import { useParams, useNavigate } from 'react-router-dom';
import { useState, useEffect, useRef, useCallback } from 'react';
import styles from "./Scene.module.css";
import nextButton from '../assets/nextbutton.svg';

import Link from '../components/Link'; // Adjust the import path as necessary
import { useAuth } from "../contexts/AuthContext";
import PlayerStats from '../components/PlayerStats';
import { useScene } from '../contexts/SceneContext';
import { API_URL } from '../config/env';

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

interface ShopItem {
    shopItemID: number;
    itemID: number;
    price: number;
    quantity: number;
    item: {
        name: string;
        description: string;
    };
}

interface Shop {
    shopID: string;
    locationID: number;
    shopItems: ShopItem[];
}

interface Minigame {
    minigameID: string;
    locationID: number;
    description: string;
    type: string;
    isCompleted: boolean;
    playerScore: number;
    computerScore: number;
}

interface RPSResult {
    playerChoice: string;
    computerChoice: string;
    result: string;
    playerScore: number;
    computerScore: number;
    isCompleted: boolean;
}

const truncateText = (text: string, maxLength: number) => {
    if (text.length <= maxLength) return text;
    return text.substring(0, maxLength) + '...';
};

const SceneBuffer: React.FC<SceneBufferProps> = ({ sceneData, isActive, className }) => {
    if (!sceneData) return null;

    return (
        <div className={`${styles.sceneBuffer} ${className || ''}`}>
            <img
                className={`${styles.backgroundImage} ${!isActive ? styles.backgroundImageHidden : ''}`}
                src={sceneData.backgroundImageUrl}
                alt={sceneData.name}
                loading="eager"
                draggable={false}
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

    const containerRef = useRef<HTMLDivElement>(null);

    const [isTransitioning, setIsTransitioning] = useState(false);
    const [currentSceneBuffer, setCurrentSceneBuffer] = useState<SceneData | null>(null);
    const [nextSceneBuffer, setNextSceneBuffer] = useState<SceneData | null>(null);
    const { getSceneData, preloadNextScenes, getShopData, purchaseItem, getMinigameData, playRPS } = useScene();

    const [shop, setShop] = useState<Shop | null>(null);
    const [minigame, setMinigame] = useState<Minigame | null>(null);

    const [isPortrait, setIsPortrait] = useState(window.innerHeight > window.innerWidth);

    useEffect(() => {
        const handleResize = () => {
            setIsPortrait(window.innerHeight > window.innerWidth);
        };

        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, []);

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
                const response = await fetch(`${API_URL}/api/Players/has-item/1`, {
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
            const response = await fetch(`${API_URL}/api/Locations/${sceneId}/collect-item`, {
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
                const response = await fetch(`${API_URL}/api/Locations/${link.toLocation.locationID}`, {
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

    }, [links, token]);

    useEffect(() => {
        preloadImages();
    }, [sceneData, preloadImages]);

    const handleNavigation = useCallback(async (path: string) => {
        const nextSceneId = parseInt(path.split('/').pop() || '');

        try {
            setIsTransitioning(true);

            const { scene: nextSceneData } = await getSceneData(nextSceneId.toString());

            // Pre-load the next image
            await new Promise((resolve, reject) => {
                const img = new Image();
                img.onload = resolve;
                img.onerror = reject;
                img.src = nextSceneData.backgroundImageUrl;
            });

            setCurrentSceneBuffer(sceneData);
            setNextSceneBuffer(nextSceneData);
            setSceneData(nextSceneData);

            // Eliminace bílého flashbangu při přechodu mezi scénami
            await new Promise(resolve => requestAnimationFrame(resolve));
            await new Promise(resolve => setTimeout(resolve, 500));

            navigate(path, { replace: true });

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
        const loadShopData = async () => {
            if (!sceneId) return;
            const shopData = await getShopData(sceneId);
            setShop(shopData);
        };
        loadShopData();
    }, [sceneId, getShopData]);

    useEffect(() => {
        const loadMinigameData = async () => {
            if (!sceneId) return;
            const minigameData = await getMinigameData(sceneId);
            setMinigame(minigameData);
        };
        loadMinigameData();
    }, [sceneId, getMinigameData]);

    const handlePurchase = async (shopItemId: number) => {
        try {
            const { message, newBalance } = await purchaseItem(shopItemId);
            alert(message);
            // You might want to refresh player stats here
        } catch (error) {
            alert(error instanceof Error ? error.message : 'Purchase failed');
        }
    };

    const renderDescription = () => {
        if (!sceneData) return null;

        // Only truncate on mobile landscape
        if (window.matchMedia('(max-width: 768px) and (orientation: landscape)').matches) {
            return truncateText(sceneData.description, 300);
        }
        return sceneData.description;
    };

    const RPSGame = ({ minigame }: { minigame: Minigame }) => {
        const [result, setResult] = useState<RPSResult | null>(null);
        const [loading, setLoading] = useState(false);
        const { playRPS } = useScene();

        const handleChoice = async (choice: string) => {
            try {
                setLoading(true);
                const result = await playRPS(minigame.minigameID, choice);
                setResult(result);
            } catch (error) {
                console.error('Failed to play:', error);
            } finally {
                setLoading(false);
            }
        };

        return (
            <div className={styles.minigameWrapper}>
                <div className={styles.minigameContainer}>
                    <h2>Rock Paper Scissors</h2>
                    <p>{minigame.description}</p>
                    <div className={styles.scoreBoard}>
                        <p>Player: {result?.playerScore || minigame.playerScore}</p>
                        <p>Computer: {result?.computerScore || minigame.computerScore}</p>
                    </div>
                    {result && (
                        <div className={styles.roundResult}>
                            <p>You chose: {result.playerChoice}</p>
                            <p>Computer chose: {result.computerChoice}</p>
                            <p>Result: {result.result.toUpperCase()}</p>
                        </div>
                    )}
                    {!minigame.isCompleted && !loading && (
                        <div className={styles.choices}>
                            <button onClick={() => handleChoice('rock')}>Rock</button>
                            <button onClick={() => handleChoice('paper')}>Paper</button>
                            <button onClick={() => handleChoice('scissors')}>Scissors</button>
                        </div>
                    )}
                    {minigame.isCompleted && (
                        <div className={styles.gameOver}>
                            <h3>Game Over!</h3>
                            <p>{result?.playerScore === 3 ? 'You won!' : 'Computer won!'}</p>
                        </div>
                    )}
                </div>
            </div>
        );
    };

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

            <SceneBuffer
                sceneData={sceneData}
                isActive={!isTransitioning}
                isTransitioning={isTransitioning}
            />

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
                    {renderDescription()}
                </div>
                {links.length === 1 && (
                    <div className={styles.navigation}>
                        {sceneData.hasRequiredItem && !hasItem && (
                            <button
                                className={styles.collectButton}
                                onClick={collectItem}
                            >
                                Sebrat item
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
                            Sebrat item
                        </button>
                    )}
                    {renderNavigation()}
                </div>
            )}

            {shop && (
                <div className={styles.shopWrapper}>
                    <div className={styles.shopContainer}>
                        <h2 className={styles.shopTitle}>Shop</h2>
                        <div className={styles.shopItems}>
                            {shop.shopItems.map(item => (
                                <div key={item.shopItemID} className={styles.shopItem}>
                                    <h3>{item.item.name}</h3>
                                    <p>{window.matchMedia('(max-width: 768px) and (orientation: landscape)').matches
                                        ? truncateText(item.item.description, 50)
                                        : item.item.description}</p>
                                    <p className={styles.itemPrice}>
                                        {item.price} coins
                                        {item.quantity > 0 && ` (${item.quantity})`}
                                    </p>
                                    <button
                                        onClick={() => handlePurchase(item.shopItemID)}
                                        className={styles.buyButton}
                                        disabled={item.quantity === 0}
                                    >
                                        {item.quantity === 0 ? 'Sold Out' : 'Buy'}
                                    </button>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            )}

            {minigame && minigame.type === 'RPS' && (
                <RPSGame minigame={minigame} />
            )}

            {isPortrait && (
                <div className={styles.rotateDevice}>
                    <div className={styles.rotateMessage}>
                        Please rotate your device to landscape mode for the best experience
                    </div>
                </div>
            )}
        </div>
    );
};

export default Scene;

