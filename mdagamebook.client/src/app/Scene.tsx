import { useParams } from 'react-router-dom';
import { useState, useEffect } from 'react';
import styles from "./Scene.module.css";

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

interface ApiError {
    message: string;
}

const Scene = () => {
    const { sceneId } = useParams<{ sceneId: string }>();
    const [sceneData, setSceneData] = useState<SceneData | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [hasItem, setHasItem] = useState(false);
    const { token, logout } = useAuth();
    const [links, setLinks] = useState<Link[]>([]);

    useEffect(() => {
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

        if (sceneId) {
            fetchData();
        }
    }, [sceneId, token, logout]);

    useEffect(() => {
        const checkForItem = async () => {
            if (sceneId === "420") {
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
            }
        };

        checkForItem();
    }, [sceneId, token]);

    const collectItem = async () => {
        try {
            const response = await fetch(`https://localhost:7260/api/Locations/${sceneId}/collect-item`, {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (!response.ok) {
                const errorData: ApiError = await response.json();
                throw new Error(errorData.message);
            }

            const data = await response.json();
            setHasItem(true);
            alert(data.message);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to collect item');
        }
    };

    if (loading) {
        return <div className={styles.container}>Loading...</div>;
    }

    if (error) {
        return <div className={styles.container}>{error}</div>;
    }

    if (!sceneData) {
        return <div className={styles.container}>No scene data found</div>;
    }

    return (
        <div className={styles.container}>
            <h1 className={styles.title}>{sceneData.name}</h1>
            <div className={styles.mainContent}>
                <div className={styles.description}>
                    {sceneData.description}
                </div>
                {sceneData.backgroundImageUrl && (
                    <img
                        className={styles.sceneImage}
                        src={sceneData.backgroundImageUrl}
                        alt={sceneData.name}
                    />
                )}
                <div className={styles.navigation}>
                    {sceneData.hasRequiredItem && !hasItem && (
                        <button
                            className={styles.collectButton}
                            onClick={collectItem}
                        >
                            Collect Required Item
                        </button>
                    )}
                    {links.map(link => (
                        <Link
                            key={link.linkID}
                            href={`/scene/${link.toLocation.locationID}`}
                        >
                            Go to {link.toLocation.name} ({link.toLocation.locationID})
                        </Link>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default Scene;

