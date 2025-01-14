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

interface SceneContextType {
    getSceneData: (sceneId: string) => Promise<{ scene: SceneData; links: Link[] }>;
    preloadNextScenes: (links: Link[]) => Promise<void>;
    clearCache: () => void;
    getShopData: (sceneId: string) => Promise<Shop | null>;
    purchaseItem: (shopItemId: number) => Promise<{ message: string; newBalance: number }>;
    getMinigameData: (sceneId: string) => Promise<Minigame | null>;
    playRPS: (minigameId: string, playerChoice: string) => Promise<RPSResult>;
}

const SceneContext = createContext<SceneContextType | null>(null);

const CACHE_DURATION = 5 * 60 * 1000; // 5 minutes

export const SceneProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [sceneCache, setSceneCache] = useState<SceneCache>({});
    const [shopCache, setShopCache] = useState<{ [key: string]: Shop }>({});
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

    const preloadNextScenes = useCallback(async (links: Link[]) => {
        const preloadPromises = links.map(link =>
            getSceneData(link.toLocation.locationID.toString())
        );
        await Promise.all(preloadPromises);
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
            const response = await fetch(`${API_URL}/api/Minigames/location/${sceneId}`, {
                headers: { 'Authorization': `Bearer ${token}` }
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
        const response = await fetch(`${API_URL}/api/Minigames/rps/play`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ minigameID: minigameId, playerChoice })
        });

        if (!response.ok) {
            throw new Error('Failed to play RPS');
        }

        return await response.json();
    }, [token]);

    return (
        <SceneContext.Provider value={{ 
            getSceneData, 
            preloadNextScenes, 
            clearCache,
            getShopData,
            purchaseItem,
            getMinigameData,
            playRPS
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