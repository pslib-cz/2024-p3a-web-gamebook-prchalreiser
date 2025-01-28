import { useState, useEffect } from 'react';
import styles from './PlayerStats.module.css';
import { useScene } from "../contexts/SceneContext";

interface PlayerStats {
    health: number;
    withdrawal: number;
    stamina: number;
    coins: number;
}

const PlayerStats = () => {
    const [stats, setStats] = useState<PlayerStats | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const { getPlayerStats, playerStatsVersion } = useScene();

    useEffect(() => {
        const loadStats = async () => {
            try {
                const playerStats = await getPlayerStats();
                setStats(playerStats);
            } catch (err) {
                console.error('Failed to fetch player stats:', err);
            } finally {
                setIsLoading(false);
            }
        };

        loadStats();
    }, [getPlayerStats, playerStatsVersion]);

    // Add a loading state that maintains the same dimensions
    if (isLoading && !stats) {
        return (
            <div className={styles.statsContainer}>
                <div className={`${styles.statBar} ${styles.loading}`}>
                    {/* Skeleton loading state that matches the layout */}
                    <div className={styles.stat}>
                        <div className={styles.statIcon}>❤️</div>
                        <div className={styles.statValue}>
                            <div className={styles.progressBar}>
                                <div className={styles.progressSkeleton} />
                            </div>
                        </div>
                    </div>
                    {/* Repeat for other stats... */}
                </div>
            </div>
        );
    }

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