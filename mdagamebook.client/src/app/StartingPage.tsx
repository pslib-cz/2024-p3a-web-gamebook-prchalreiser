import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "./StartingPage.module.css";
import Link from "../components/Link";
import RainbowSpiral from "../components/RainbowSpiral";
import { useAuth } from "../contexts/AuthContext";

const StartingPage = () => {
    const navigate = useNavigate();
    const { token } = useAuth();
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const token = localStorage.getItem("accessToken");
        if (!token) {
            navigate("/login");
        }
    }, [navigate]);

    const handlePlayClick = async (e: React.MouseEvent) => {
        e.preventDefault();
        setLoading(true);

        try {
            const response = await fetch('https://localhost:7260/api/Locations/last-location', {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const locationData = await response.json();
                // Redirect to the last location if it exists
                if (locationData && locationData.locationID) {
                    navigate(`/scene/${locationData.locationID}`);
                } else {
                    // If no last location, redirect to default scene
                    navigate('/scene/420');
                }
            } else {
                // If can't fetch last location, redirect to default scene
                navigate('/scene/420');
            }
        } catch (error) {
            console.error('Failed to fetch last location:', error);
            navigate('/scene/420'); // Fallback to default scene
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.container}>
            <h1 className={styles.title}>Multidimenzionální absťák</h1>
            <div className={styles.mainContent}>
                <div className={styles.linkWrapper}>
                    <Link href="#" onClick={handlePlayClick}>
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
