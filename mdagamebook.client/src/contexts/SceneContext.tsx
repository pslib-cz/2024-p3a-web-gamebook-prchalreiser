import React, { createContext, useContext, useState, useCallback } from 'react';
import { useAuth } from './AuthContext';
import { API_URL } from '../config/env';

interface SceneData {
    id: number;
    name: string;
    description: string;
    items: string;
    backgroundImageUrl: string;
    hasRequiredItem: boolean;
    hasShop: boolean;
    hasMinigame: boolean;
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

interface SceneCache {
    [key: string]: {
        scene: SceneData;
        links: Link[];
        timestamp: number;
    };
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

interface PlayerStats {
    health: number;
    withdrawal: number;
    stamina: number;
    coins: number;
}

interface SceneContextType {
    getSceneData: (sceneId: string) => Promise<{ scene: SceneData; links: Link[] }>;
    preloadNextScenes: (links: Link[]) => Promise<void>;
    clearCache: () => void;
    getShopData: (sceneId: string) => Promise<Shop | null>;
    purchaseItem: (shopItemId: number) => Promise<{ message: string; newBalance: number }>;
    getMinigameData: (sceneId: string) => Promise<Minigame | null>;
    playRPS: (minigameId: string, playerChoice: string) => Promise<RPSResult>;
    getPlayerStats: () => Promise<PlayerStats>;
    playerStatsVersion: number;
    refreshPlayerStats: () => Promise<void>;
    currentScene: { scene: SceneData; links: Link[] } | null;
    preloadedScenes: Map<string, { scene: SceneData; links: Link[] }>;
    switchToScene: (sceneId: string) => void;
    loadInitialScene: (sceneId: string) => Promise<void>;
}

const SceneContext = createContext<SceneContextType | null>(null);

const CACHE_DURATION = 5 * 60 * 1000; // 5 minutes

export const SceneProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [sceneCache, setSceneCache] = useState<SceneCache>({});
    const [shopCache, setShopCache] = useState<{ [key: string]: Shop }>({});
    const [playerStatsCache, setPlayerStatsCache] = useState<PlayerStats | null>(null);
    const [playerStatsVersion, setPlayerStatsVersion] = useState(0);
    const [currentScene, setCurrentScene] = useState<{ scene: SceneData; links: Link[] } | null>(null);
    const [preloadedScenes] = useState<Map<string, { scene: SceneData; links: Link[] }>>(new Map());
    const { token } = useAuth();

    const fetchSceneData = async (sceneId: string) => {
        const [sceneResponse, linksResponse] = await Promise.all([
            fetch(`${API_URL}/api/Locations/${sceneId}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            }),
            fetch(`${API_URL}/api/Links/from/${sceneId}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            })
        ]);

        if (!sceneResponse.ok || !linksResponse.ok) {
            throw new Error('Failed to fetch scene data');
        }

        const scene = await sceneResponse.json();
        const links = await linksResponse.json();

        return { scene, links };
    };

    const getSceneData = useCallback(async (sceneId: string) => {
        const now = Date.now();
        const cached = sceneCache[sceneId];

        if (cached && now - cached.timestamp < CACHE_DURATION) {
            return { scene: cached.scene, links: cached.links };
        }

        const { scene, links } = await fetchSceneData(sceneId);

        setSceneCache(prev => ({
            ...prev,
            [sceneId]: { scene, links, timestamp: now }
        }));

        return { scene, links };
    }, [token, sceneCache]);

    const preloadScene = useCallback(async (sceneId: string) => {
        if (preloadedScenes.has(sceneId)) return;

        try {
            const [sceneResponse, linksResponse] = await Promise.all([
                fetch(`${API_URL}/api/Locations/${sceneId}`, {
                    headers: { 'Authorization': `Bearer ${token}` }
                }),
                fetch(`${API_URL}/api/Links/from/${sceneId}`, {
                    headers: { 'Authorization': `Bearer ${token}` }
                })
            ]);

            if (!sceneResponse.ok || !linksResponse.ok) {
                throw new Error('Failed to preload scene');
            }

            const [scene, links] = await Promise.all([
                sceneResponse.json(),
                linksResponse.json()
            ]);

            // Preload the image
            await new Promise((resolve, reject) => {
                const img = new Image();
                img.onload = resolve;
                img.onerror = reject;
                img.src = scene.backgroundImageUrl;
            });

            preloadedScenes.set(sceneId, { scene, links });
        } catch (error) {
            console.error('Failed to preload scene:', error);
        }
    }, [token, preloadedScenes]);

    const switchToScene = useCallback((sceneId: string) => {
        const nextScene = preloadedScenes.get(sceneId);
        if (nextScene) {
            setCurrentScene(nextScene);
            // Start preloading the next possible scenes
            nextScene.links.forEach(link => {
                preloadScene(link.toLocation.locationID.toString());
            });
        }
    }, [preloadedScenes, preloadScene]);

    const loadInitialScene = useCallback(async (sceneId: string) => {
        try {
            // First fetch the scene data directly
            const [sceneResponse, linksResponse] = await Promise.all([
                fetch(`${API_URL}/api/Locations/${sceneId}`, {
                    headers: { 'Authorization': `Bearer ${token}` }
                }),
                fetch(`${API_URL}/api/Links/from/${sceneId}`, {
                    headers: { 'Authorization': `Bearer ${token}` }
                })
            ]);

            if (!sceneResponse.ok || !linksResponse.ok) {
                throw new Error(`Failed to load scene: ${sceneResponse.statusText}`);
            }

            // Check if responses have content
            const sceneText = await sceneResponse.text();
            const linksText = await linksResponse.text();

            if (!sceneText || !linksText) {
                throw new Error('Empty response from server');
            }

            const scene = JSON.parse(sceneText);
            const links = JSON.parse(linksText);

            // Set as current scene immediately
            setCurrentScene({ scene, links });

            // Then preload the image and linked scenes in the background
            const img = new Image();
            img.src = scene.backgroundImageUrl;
            
            // Store in preloaded scenes
            preloadedScenes.set(sceneId, { scene, links });

            // Start preloading linked scenes
            links.forEach((link: Link) => {
                preloadScene(link.toLocation.locationID.toString());
            });
        } catch (error) {
            console.error('Failed to load initial scene:', error);
            throw error;
        }
    }, [token, preloadedScenes, preloadScene]);

    const preloadNextScenes = useCallback(async (links: Link[]) => {
        const preloadPromises = links.map(async (link) => {
            const { scene } = await getSceneData(link.toLocation.locationID.toString());
            
            // Preload the image
            return new Promise((resolve, reject) => {
                const img = new Image();
                img.onload = resolve;
                img.onerror = reject;
                img.src = scene.backgroundImageUrl;
            });
        });
        
        try {
            await Promise.all(preloadPromises);
        } catch (error) {
            console.error('Failed to preload some scenes:', error);
        }
    }, [getSceneData]);

    const clearCache = useCallback(() => {
        setSceneCache({});
    }, []);

    const getShopData = useCallback(async (sceneId: string) => {
        try {
            const response = await fetch(`${API_URL}/api/Shops/location/${sceneId}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            });

            if (response.ok) {
                const shopData = await response.json();
                setShopCache(prev => ({
                    ...prev,
                    [sceneId]: shopData
                }));
                return shopData;
            } else if (response.status !== 404) {
                console.error('Failed to fetch shop data');
            }
            return null;
        } catch (error) {
            console.error('Failed to fetch shop:', error);
            return null;
        }
    }, [token]);

    const purchaseItem = useCallback(async (shopItemId: number) => {
        const response = await fetch(`${API_URL}/api/Shops/buy`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ shopItemId })
        });

        const data = await response.json();
        if (!response.ok) {
            throw new Error(data.message || 'Purchase failed');
        }

        return data;
    }, [token]);

    const getMinigameData = useCallback(async (sceneId: string) => {
        try {
            const response = await fetch(`${API_URL}/api/Minigames/play/${sceneId}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            if (response.ok) {
                return await response.json();
            } else if (response.status !== 404) {
                console.error('Failed to fetch minigame data');
            }
            return null;
        } catch (error) {
            console.error('Failed to fetch minigame:', error);
            return null;
        }
    }, [token]);

    const playRPS = useCallback(async (minigameId: string, playerChoice: string) => {
        const response = await fetch(`${API_URL}/api/Minigames/${minigameId}/play`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ playerChoice })
        });

        if (!response.ok) {
            throw new Error('Failed to play RPS');
        }

        return await response.json();
    }, [token]);

    const getPlayerStats = useCallback(async () => {
        if (playerStatsCache) {
            return playerStatsCache;
        }

        try {
            const response = await fetch(`${API_URL}/api/Players/current`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const stats = await response.json();
                setPlayerStatsCache(stats);
                setPlayerStatsVersion(v => v + 1);
                return stats;
            }
            throw new Error('Failed to fetch player stats');
        } catch (error) {
            console.error('Failed to fetch player stats:', error);
            throw error;
        }
    }, [token, playerStatsCache]);

    const refreshPlayerStats = useCallback(async () => {
        try {
            const response = await fetch(`${API_URL}/api/Players/current`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const stats = await response.json();
                setPlayerStatsCache(stats);
                setPlayerStatsVersion(v => v + 1);
            } else {
                throw new Error('Failed to fetch player stats');
            }
        } catch (error) {
            console.error('Failed to fetch player stats:', error);
            throw error;
        }
    }, [token]);

    return (
        <SceneContext.Provider value={{
            getSceneData,
            preloadNextScenes,
            clearCache,
            getShopData,
            purchaseItem,
            getMinigameData,
            playRPS,
            getPlayerStats,
            playerStatsVersion,
            refreshPlayerStats,
            currentScene,
            preloadedScenes,
            switchToScene,
            loadInitialScene,
        }}>
            {children}
        </SceneContext.Provider>
    );
};

export const useScene = () => {
    const context = useContext(SceneContext);
    if (!context) {
        throw new Error('useScene must be used within a SceneProvider');
    }
    return context;
}; 