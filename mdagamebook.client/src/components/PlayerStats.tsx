import { useState, useEffect } from 'react';
import styles from './PlayerStats.module.css';
import { useAuth } from "../contexts/AuthContext";
import { API_URL } from '../config/env';

interface PlayerStats {
    health: number;
    withdrawal: number;
    stamina: number;
    coins: number;
}

const PlayerStats = () => {
    const [stats, setStats] = useState<PlayerStats | null>(null);
    const { token } = useAuth();

    useEffect(() => {
        const fetchPlayerStats = async () => {
            try {
                const response = await fetch(`${API_URL}/api/Players/current`, {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });

                if (response.ok) {
                    const data = await response.json();
                    setStats(data); // Now using the current player's data directly
                }
            } catch (err) {
                console.error('Failed to fetch player stats:', err);
            }
        };

        fetchPlayerStats();
    }, [token]);

    if (!stats) return null;

    return (
        <div className={styles.statsContainer}>
            <div className={styles.statBar}>
                <div className={styles.stat}>
                    <div className={styles.statIcon}>❤️</div>
                    <div className={styles.statValue}>
                        <div className={styles.progressBar}>
                            <div
                                className={`${styles.progress} ${styles.healthBar}`}
                                style={{ width: `${stats.health}%` }}
                            />
                        </div>
                        <span>{stats.health}</span>
                    </div>
                </div>
                <div className={styles.stat}>
                    <div className={styles.statIcon}>💊</div>
                    <div className={styles.statValue}>
                        <div className={styles.progressBar}>
                            <div
                                className={`${styles.progress} ${styles.withdrawalBar}`}
                                style={{ width: `${stats.withdrawal}%` }}
                            />
                        </div>
                        <span>{stats.withdrawal}</span>
                    </div>
                </div>
                <div className={styles.stat}>
                    <div className={styles.statIcon}>⚡</div>
                    <div className={styles.statValue}>
                        <div className={styles.progressBar}>
                            <div
                                className={`${styles.progress} ${styles.staminaBar}`}
                                style={{ width: `${stats.stamina}%` }}
                            />
                        </div>
                        <span>{stats.stamina}</span>
                    </div>
                </div>
                <div className={`${styles.stat} ${styles.coins}`}>
                    <div className={styles.statIcon}>💰</div>
                    <span>{stats.coins}</span>
                </div>
            </div>
        </div>
    );
};

export default PlayerStats; 