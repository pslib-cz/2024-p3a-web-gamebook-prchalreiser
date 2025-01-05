import { useParams, useNavigate } from 'react-router-dom';
import { useState, useEffect, useRef, useCallback, useMemo } from 'react';
import styles from "./Scene.module.css";
import nextButton from '../assets/nextbutton.svg';

import Link from '../components/Link'; // Adjust the import path as necessary
import { useAuth } from "../contexts/AuthContext";
import PlayerStats from '../components/PlayerStats';
import { useScene } from '../contexts/SceneContext';

interface SceneData {
    id: number;
    name: string;
    description: string;
    items: string;
    backgroundImageUrl: string;
    hasRequiredItem: boolean;
}

interface Link {
    linkID: number;
    fromLocationID: number;
    toLocationID: number;
    requiredItemId: number | null;
    toLocation: {
        locationID: number;
        name: string;
    };
}

interface PreloadedImages {
    [key: number]: HTMLImageElement;
}

interface SceneBufferProps {
    sceneData: SceneData | null;
    isActive: boolean;
    isTransitioning: boolean;
    className?: string;
}

const SceneBuffer: React.FC<SceneBufferProps> = ({ sceneData, isActive, isTransitioning, className }) => {
    if (!sceneData) return null;

    return (
        <div className={`${styles.sceneBuffer} ${className || ''}`}>
            <img
                className={`${styles.backgroundImage} ${!isActive ? styles.backgroundImageHidden : ''}`}
                src={sceneData.backgroundImageUrl}
                alt={sceneData.name}
                loading="eager"
            />
        </div>
    );
};

const Scene = () => {
    const { sceneId } = useParams<{ sceneId: string }>();
    const [sceneData, setSceneData] = useState<SceneData | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [hasItem, setHasItem] = useState(false);
    const { token, logout } = useAuth();
    const [links, setLinks] = useState<Link[]>([]);
    const navigate = useNavigate();
    const [isExiting, setIsExiting] = useState(false);
    const containerRef = useRef<HTMLDivElement>(null);
    const [preloadedImages, setPreloadedImages] = useState<PreloadedImages>({});
    const [currentImage, setCurrentImage] = useState<string | null>(null);
    const [nextImage, setNextImage] = useState<string | null>(null);
    const [isTransitioning, setIsTransitioning] = useState(false);
    const [currentSceneBuffer, setCurrentSceneBuffer] = useState<SceneData | null>(null);
    const [nextSceneBuffer, setNextSceneBuffer] = useState<SceneData | null>(null);
    const { getSceneData, preloadNextScenes } = useScene();

    const fetchData = async () => {
        try {
            setLoading(true);
            const { scene, links } = await getSceneData(sceneId!);
            setSceneData(scene);
            setLinks(links);
            // Preload next scenes immediately
            preloadNextScenes(links);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Unexpected error');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (sceneId) {
            fetchData();
        }
    }, [sceneId, token, logout]);

    useEffect(() => {
        const checkForItem = async () => {
            try {
                const response = await fetch('https://localhost:7260/api/Players/has-item/1', {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });

                if (response.ok) {
                    const hasItem = await response.json();
                    setHasItem(hasItem);
                }
            } catch (err) {
                console.error('Failed to check for item:', err);
            }
        };

        checkForItem();
    }, [sceneId, token]);

    const collectItem = async () => {
        try {
            const response = await fetch(`https://localhost:7260/api/Locations/${sceneId}/collect-item`, {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.message || 'Failed to collect item');
            }

            setHasItem(true);
            alert(data.message);

            // Refresh the scene data after collecting the item
            fetchData();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to collect item');
        }
    };

    const preloadImages = useCallback(async () => {
        if (!links || !links.length) return;

        const newPreloadedImages: PreloadedImages = {};

        for (const link of links) {
            try {
                const response = await fetch(`https://localhost:7260/api/Locations/${link.toLocation.locationID}`, {
                    headers: { 'Authorization': `Bearer ${token}` }
                });
                const sceneData = await response.json();

                if (sceneData.backgroundImageUrl) {
                    const img = new Image();
                    img.src = sceneData.backgroundImageUrl;
                    await new Promise((resolve, reject) => {
                        img.onload = resolve;
                        img.onerror = reject;
                    });
                    newPreloadedImages[link.toLocation.locationID] = img;
                }
            } catch (error) {
                console.error('Failed to preload image:', error);
            }
        }

        setPreloadedImages(newPreloadedImages);
    }, [links, token]);

    useEffect(() => {
        if (sceneData?.backgroundImageUrl) {
            setCurrentImage(sceneData.backgroundImageUrl);
        }
        preloadImages();
    }, [sceneData, preloadImages]);

    const handleNavigation = useCallback(async (path: string) => {
        const nextSceneId = parseInt(path.split('/').pop() || '');

        try {
            setIsTransitioning(true);

            // Get the next scene data from cache (it should be preloaded)
            const { scene: nextSceneData } = await getSceneData(nextSceneId.toString());

            // Pre-load the next image
            await new Promise((resolve, reject) => {
                const img = new Image();
                img.onload = resolve;
                img.onerror = reject;
                img.src = nextSceneData.backgroundImageUrl;
            });

            // Set up the buffers
            setCurrentSceneBuffer(sceneData);
            setNextSceneBuffer(nextSceneData);

            // Update the scene data immediately but keep the old background
            setSceneData(nextSceneData);

            // Wait for a frame to ensure React has updated the DOM
            await new Promise(resolve => requestAnimationFrame(resolve));
            await new Promise(resolve => setTimeout(resolve, 500));

            navigate(path, { replace: true }); // Use replace to prevent history stack buildup

        } catch (error) {
            console.error('Navigation failed:', error);
            navigate(path);
        } finally {
            setIsTransitioning(false);
            setCurrentSceneBuffer(null);
            setNextSceneBuffer(null);
        }
    }, [navigate, sceneData, getSceneData]);

    useEffect(() => {
        // Reset exit state when scene changes
        setIsExiting(false);
    }, [sceneId]);

    if (loading) {
        return <div className={styles.loading}>Loading your adventure...</div>;
    }

    if (error) {
        return <div className={styles.error}>{error}</div>;
    }

    if (!sceneData) {
        return <div className={styles.error}>No scene data found</div>;
    }

    const renderNavigation = () => {
        if (links.length === 1) {
            return (
                <button
                    className={styles.continueButton}
                    onClick={() => handleNavigation(`/scene/${links[0].toLocation.locationID}`)}
                    aria-label="Continue to next scene"
                >
                    <img
                        src={nextButton}
                        alt="Next"
                        className={styles.continueTriangle}
                    />
                </button>
            );
        }

        return (
            <div className={styles.multipleChoices}>
                {links.map(link => (
                    <Link
                        key={link.linkID}
                        href={`/scene/${link.toLocation.locationID}`}
                        className={styles.choiceButton}
                        onClick={() => handleNavigation(`/scene/${link.toLocation.locationID}`)}
                    >
                        {link.toLocation.name}
                    </Link>
                ))}
            </div>
        );
    };

    return (
        <div ref={containerRef} className={styles.container}>
            <PlayerStats />

            {/* Main scene buffer */}
            <SceneBuffer
                sceneData={sceneData}
                isActive={!isTransitioning}
                isTransitioning={isTransitioning}
            />

            {/* Transition buffers */}
            {isTransitioning && (
                <>
                    <SceneBuffer
                        sceneData={currentSceneBuffer}
                        isActive={false}
                        isTransitioning={true}
                        className={styles.transitionOut}
                    />
                    <SceneBuffer
                        sceneData={nextSceneBuffer}
                        isActive={true}
                        isTransitioning={true}
                        className={styles.transitionIn}
                    />
                </>
            )}

            <div className={links.length === 1 ? styles.textBoxSingle : styles.textBoxMultiple}>
                <div className={styles.description}>
                    {sceneData.description}
                </div>
                {links.length === 1 && (
                    <div className={styles.navigation}>
                        {sceneData.hasRequiredItem && !hasItem && (
                            <button
                                className={styles.collectButton}
                                onClick={collectItem}
                            >
                                Collect Required Item
                            </button>
                        )}
                        {renderNavigation()}
                    </div>
                )}
            </div>
            {links.length > 1 && (
                <div className={styles.navigationMultiple}>
                    {sceneData.hasRequiredItem && !hasItem && (
                        <button
                            className={styles.collectButton}
                            onClick={collectItem}
                        >
                            Collect Required Item
                        </button>
                    )}
                    {renderNavigation()}
                </div>
            )}
        </div>
    );
};

export default Scene;

