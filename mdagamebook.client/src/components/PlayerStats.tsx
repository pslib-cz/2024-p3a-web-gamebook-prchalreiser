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
                // Keep the old stats while loading new ones
                setIsLoading(true);
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

    // Show previous stats while loading instead of null
    if (isLoading && !stats) {
        return (
            <div className={styles.statsContainer}>
                <div className={`${styles.statBar} ${styles.loading}`}>
                    <div className={styles.stat}>
                        <div className={styles.statIcon}>❤️</div>
                        <div className={styles.statValue}>
                            <div className={styles.progressBar}>
                                <div className={styles.progressSkeleton} />
                            </div>
                        </div>
                    </div>
                    {/* Repeat for other stats */}
                </div>
            </div>
        );
    }

    // Keep showing old stats while loading new ones
    return (
        <div className={styles.statsContainer}>
            <div className={styles.statBar}>
                <div className={styles.stat}>
                    <div className={styles.statIcon}>❤️</div>
                    <div className={styles.statValue}>
                        <div className={styles.progressBar}>
                            <div
                                className={`${styles.progress} ${styles.healthBar} ${isLoading ? styles.loading : ''}`}
                                style={{ width: `${stats?.health ?? 0}%` }}
                            />
                        </div>
                        <span>{stats?.health ?? 0}</span>
                    </div>
                </div>
                <div className={styles.stat}>
                    <div className={styles.statIcon}>💊</div>
                    <div className={styles.statValue}>
                        <div className={styles.progressBar}>
                            <div
                                className={`${styles.progress} ${styles.withdrawalBar} ${isLoading ? styles.loading : ''}`}
                                style={{ width: `${stats?.withdrawal ?? 0}%` }}
                            />
                        </div>
                        <span>{stats?.withdrawal ?? 0}</span>
                    </div>
                </div>
                <div className={styles.stat}>
                    <div className={styles.statIcon}>⚡</div>
                    <div className={styles.statValue}>
                        <div className={styles.progressBar}>
                            <div
                                className={`${styles.progress} ${styles.staminaBar} ${isLoading ? styles.loading : ''}`}
                                style={{ width: `${stats?.stamina ?? 0}%` }}
                            />
                        </div>
                        <span>{stats?.stamina ?? 0}</span>
                    </div>
                </div>
                <div className={`${styles.stat} ${styles.coins}`}>
                    <div className={styles.statIcon}>💰</div>
                    <span>{stats?.coins ?? 0}</span>
                </div>
            </div>
        </div>
    );
};

export default PlayerStats; 