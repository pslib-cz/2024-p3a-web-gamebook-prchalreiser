import { useParams, useNavigate } from 'react-router-dom';
import { useState, useEffect, useRef } from 'react';
import styles from "./Scene.module.css";
import nextButton from '../assets/nextbutton.svg';

import Link from '../components/Link'; // Adjust the import path as necessary
import { useAuth } from "../contexts/AuthContext";

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

    const fetchData = async () => {
        try {
            setLoading(true);
            const [sceneResponse, linksResponse] = await Promise.all([
                fetch(`https://localhost:7260/api/Locations/${sceneId}`, {
                    headers: { 'Authorization': `Bearer ${token}` }
                }),
                fetch(`https://localhost:7260/api/Links/from/${sceneId}`, {
                    headers: { 'Authorization': `Bearer ${token}` }
                })
            ]);

            if (!sceneResponse.ok) {
                const errorData = await sceneResponse.json();
                if (sceneResponse.status === 401) {
                    logout();
                    throw new Error("Prosím, přihlaste se znovu");
                }
                throw new Error(errorData.message || "Nepodařilo se načíst scénu");
            }

            if (!linksResponse.ok) {
                throw new Error("Nepodařilo se načíst odkazy");
            }

            const sceneData = await sceneResponse.json();
            const linksData = await linksResponse.json();

            setSceneData(sceneData);
            setLinks(linksData);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Nastala neočekávaná chyba');
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

    const handleNavigation = async (path: string) => {
        setIsExiting(true);
        // Wait for the fade out
        await new Promise(resolve => setTimeout(resolve, 200));
        // Navigate and reset the exit state
        navigate(path);
        setIsExiting(false);
    };

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
        <div 
            ref={containerRef}
            className={`${styles.container} ${isExiting ? styles.exit : ''}`}
        >
            {sceneData.backgroundImageUrl && (
                <img
                    className={styles.backgroundImage}
                    src={sceneData.backgroundImageUrl}
                    alt={sceneData.name}
                />
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

