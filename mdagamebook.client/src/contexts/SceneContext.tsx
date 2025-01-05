import React, { createContext, useContext, useState, useCallback } from 'react';
import { useAuth } from './AuthContext';

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

interface SceneContextType {
    getSceneData: (sceneId: string) => Promise<{ scene: SceneData; links: Link[] }>;
    preloadNextScenes: (links: Link[]) => Promise<void>;
    clearCache: () => void;
}

const SceneContext = createContext<SceneContextType | null>(null);

const CACHE_DURATION = 5 * 60 * 1000; // 5 minutes

export const SceneProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [sceneCache, setSceneCache] = useState<SceneCache>({});
    const { token } = useAuth();

    const fetchSceneData = async (sceneId: string) => {
        const [sceneResponse, linksResponse] = await Promise.all([
            fetch(`https://localhost:7260/api/Locations/${sceneId}`, {
                headers: { 'Authorization': `Bearer ${token}` }
            }),
            fetch(`https://localhost:7260/api/Links/from/${sceneId}`, {
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

    return (
        <SceneContext.Provider value={{ getSceneData, preloadNextScenes, clearCache }}>
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