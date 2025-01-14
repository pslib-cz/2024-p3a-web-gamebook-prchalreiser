import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./StartingPage.module.css";
import Link from "../components/Link";
import RainbowSpiral from "../components/RainbowSpiral";
import PlayerInfo from "../components/PlayerInfo";
import { useAuth } from "../contexts/AuthContext";
import { API_URL } from '../config/env';

const StartingPage = () => {
    const navigate = useNavigate();
    const { token } = useAuth();
    const [loading, setLoading] = useState(false);
    const [playerData, setPlayerData] = useState<{ name: string, lastLocationID: number } | null>(null);

    useEffect(() => {
        const token = localStorage.getItem("accessToken");
        if (!token) {
            navigate("/login");
        } else {
            fetchPlayerData();
        }
    }, [navigate]);

    const fetchPlayerData = async () => {
        try {
            const response = await fetch(`${API_URL}/api/Players/current`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const data = await response.json();
                setPlayerData(data);
            }
        } catch (error) {
            console.error('Failed to fetch player data:', error);
        }
    };

    const handlePlayClick = async () => {
        setLoading(true);

        try {
            const response = await fetch(`${API_URL}/api/Locations/last-location`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const locationData = await response.json();
                if (locationData && locationData.locationID) {
                    navigate(`/scene/${locationData.locationID}`);
                } else {
                    // If no last location exists, start from the first location (420)
                    navigate('/scene/0');
                }
            } else if (response.status === 404) {
                // Handle 404 by starting from the first location
                navigate('/scene/0');
            } else {
                // Handle other errors by redirecting to start
                navigate('/scene/0');
            }
        } catch (error) {
            console.error('Failed to fetch last location:', error);
            // On error, try starting from the first location
            navigate('/scene/0');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.container}>
            {playerData && (
                <PlayerInfo 
                    playerName={playerData.name} 
                    lastLocation={playerData.lastLocationID} 
                />
            )}
            <h1 className={styles.title}>Multidimenzionální absťák</h1>
            <div className={styles.mainContent}>
                <div className={styles.linkWrapper}>
                    <Link href="#" onClick={() => { handlePlayClick() }}>
                        {loading ? "Načítání..." : "Hrát"}
                    </Link>
                    <Link href="/leaderboard">Žebříčky</Link>
                </div>
                <div className={styles.spiralWrapper}>
                    <RainbowSpiral />
                </div>
            </div>
        </div>
    );
};

export default StartingPage;
