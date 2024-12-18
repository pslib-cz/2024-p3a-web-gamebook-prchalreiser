import { useParams } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from "./Scene.module.css";

// Assuming Link is your styled button component
import Link from '../components/Link'; // Adjust the import path as necessary

interface SceneData {
    id: number;
    name: string;
    description: string;
    items: string;
    backgroundImageUrl: string;
    // Add other properties based on your API response
}

interface ApiError {
    message: string;
}

const Scene = () => {
    const navigate = useNavigate();
    const { sceneId } = useParams<{ sceneId: string }>();
    const [sceneData, setSceneData] = useState<SceneData | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [hasCollectedItem, setHasCollectedItem] = useState(false);

    useEffect(() => {
        const fetchSceneData = async () => {
            try {
                setLoading(true);
                const token = localStorage.getItem('accessToken');
                const response = await fetch(`https://localhost:7260/api/Locations/${sceneId}`, {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });

                if (!response.ok) {
                    const errorData: ApiError = await response.json();
                    if (response.status === 404) {
                        throw new Error("Scéna neexistuje");
                    } else if (response.status === 401) {
                        navigate("/login");
                    } else if (response.status === 400) {
                        throw new Error(errorData.message);
                    } else {
                        throw new Error(`${response.statusText}`);
                    }
                }

                const data = await response.json();
                setSceneData(data);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'An error occurred');
            } finally {
                setLoading(false);
            }
        };

        if (sceneId) {
            fetchSceneData();
        }
    }, [sceneId]);

    const collectItem = async () => {
        try {
            const token = localStorage.getItem('accessToken');
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
            setHasCollectedItem(true);
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
                    {sceneId === "420" && !hasCollectedItem && (
                        <button 
                            className={styles.collectButton} 
                            onClick={collectItem}
                        >
                            Collect Required Item
                        </button>
                    )}
                    {sceneId === "420" && (
                        <Link href={`/scene/421`}>
                            Go to Outside (421)
                        </Link>
                    )}
                    {sceneId === "421" && (
                        <Link href={`/scene/420`}>
                            Go to Hotbox (420)
                        </Link>
                    )}
                </div>
            </div>
        </div>
    );
};

export default Scene;
