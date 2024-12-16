import { useParams } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

interface SceneData {
    id: number;
    title: string;
    description: string;
    // Add other properties based on your API response
}

const Scene = () => {
    const navigate = useNavigate();
    const { sceneId } = useParams<{ sceneId: string }>();
    const [sceneData, setSceneData] = useState<SceneData | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchSceneData = async () => {
            try {
                setLoading(true);
                const response = await fetch(`https://localhost:7260/api/Locations/${sceneId}`);

                if (!response.ok) {
                    // if response is 404, throw that scene may not exist
                    if (response.status === 404) {
                        throw new Error("Scéna neexistuje");
                    }
                    // if response is 401, redirect to /login
                    else if (response.status === 401) {
                        navigate("/login");
                    }
                    else {
                        throw new Error(`HTTP error! status: ${response.status}`);
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

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (!sceneData) {
        return <div>No scene data found</div>;
    }

    return (
        <div className="scene-container">
            <h1>{sceneData.name}</h1>
            <div className="scene-description">
                {sceneData.description}
            </div>
            {/* Add more UI elements based on your sceneData structure */}
        </div>
    );
};

export default Scene;