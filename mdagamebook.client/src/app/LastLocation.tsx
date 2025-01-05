import { useState, useEffect } from 'react';
import { useAuth } from "../contexts/AuthContext";
import styles from "./Scene.module.css"; // Reusing Scene styles for consistency

interface LocationData {
    locationID: number;
    name: string;
    description: string;
    backgroundImageUrl: string;
}

const LastLocation = () => {
    const [location, setLocation] = useState<LocationData | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const { token, logout } = useAuth();

    useEffect(() => {
        const fetchLastLocation = async () => {
            try {
                setLoading(true);
                const response = await fetch('https://localhost:7260/api/Locations/last-location', {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });

                if (!response.ok) {
                    if (response.status === 401) {
                        logout();
                        throw new Error("Prosím, přihlaste se znovu");
                    }
                    const errorData = await response.json();
                    throw new Error(errorData.message || "Nepodařilo se načíst poslední lokaci");
                }

                const locationData = await response.json();
                setLocation(locationData);
            } catch (err) {
                setError(err instanceof Error ? err.message : 'Nastala neočekávaná chyba');
            } finally {
                setLoading(false);
            }
        };

        fetchLastLocation();
    }, [token, logout]);

    if (loading) {
        return <div className={styles.container}>Loading...</div>;
    }

    if (error) {
        return <div className={styles.container}>{error}</div>;
    }

    if (!location) {
        return <div className={styles.container}>No last location found</div>;
    }

    return (
        <div className={styles.container}>
            <h1 className={styles.title}>Last Visited Location</h1>
            <div className={styles.mainContent}>
                <h2>{location.name} (ID: {location.locationID})</h2>
                <div className={styles.description}>
                    {location.description}
                </div>
                {location.backgroundImageUrl && (
                    <img
                        className={styles.sceneImage}
                        src={location.backgroundImageUrl}
                        alt={location.name}
                    />
                )}
            </div>
        </div>
    );
};

export default LastLocation; 