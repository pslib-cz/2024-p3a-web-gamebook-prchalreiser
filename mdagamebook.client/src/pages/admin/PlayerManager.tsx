import { useState, useEffect } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import styles from './AdminPanel.module.css';
import { API_URL } from '../../config/env';

interface Player {
    playerID: string;
    name: string;
    health: number;
    withdrawal: number;
    stamina: number;
    coins: number;
    lastLocationID: number;
    inventory: Item[];
}

interface Item {
    itemID: number;
    name: string;
    description: string;
}

const PlayerManager = () => {
    const { token } = useAuth();
    const [players, setPlayers] = useState<Player[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<string | null>(null);

    useEffect(() => {
        fetchPlayers();
    }, [token]);

    const fetchPlayers = async () => {
        try {
            const response = await fetch(`${API_URL}/api/players`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (response.ok) {
                const data = await response.json();
                setPlayers(data);
            }
        } catch (err) {
            setError('Failed to fetch players!');
        }
    };

    const handleResetStats = async (playerId: string) => {
        if (!window.confirm('Are you sure you want to reset this player\'s stats?')) {
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/players/${playerId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({
                    playerID: playerId,
                    health: 100,
                    withdrawal: 0,
                    stamina: 100,
                    coins: 0
                }),
            });

            if (!response.ok) {
                throw new Error('Failed to reset player stats');
            }

            setSuccess('Player stats reset successfully!');
            fetchPlayers();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    const handleDeletePlayer = async (playerId: string) => {
        if (!window.confirm('Are you sure you want to delete this player? This action cannot be undone.')) {
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/players/${playerId}`, {
                method: 'DELETE',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Failed to delete player');
            }

            setSuccess('Player deleted successfully!');
            fetchPlayers();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.section}>
            <h2>Player Management</h2>

            {error && <div className={styles.error}>{error}</div>}
            {success && <div className={styles.success}>{success}</div>}

            <div className={styles.list}>
                <h3>Active Players</h3>
                {players.map(player => (
                    <div key={player.playerID} className={styles.listItem}>
                        <div className={styles.playerHeader}>
                            <div className={styles.playerTitleGroup}>
                                <h4>{player.name || 'Unnamed Player'}</h4>
                                <span className={styles.playerId}>ID: {player.playerID}</span>
                            </div>
                            <div className={styles.playerStats}>
                                <span className={`${styles.statBadge} ${styles.health}`}>
                                    ❤️ {player.health}
                                </span>
                                <span className={`${styles.statBadge} ${styles.stamina}`}>
                                    ⚡ {player.stamina}
                                </span>
                                <span className={`${styles.statBadge} ${styles.withdrawal}`}>
                                    🌀 {player.withdrawal}
                                </span>
                                <span className={`${styles.statBadge} ${styles.coins}`}>
                                    💰 {player.coins}
                                </span>
                                <span className={`${styles.statBadge} ${styles.location}`}>
                                    🗺️ Location {player.lastLocationID}
                                </span>
                            </div>
                        </div>

                        <div className={styles.playerContent}>
                            <div className={styles.inventorySection}>
                                <h5>Inventory</h5>
                                <div className={styles.inventoryGrid}>
                                    {player.inventory?.map(item => (
                                        <div key={item.itemID} className={styles.inventoryItem}>
                                            <span className={styles.itemName}>{item.name}</span>
                                            <span className={styles.itemDescription}>{item.description}</span>
                                        </div>
                                    ))}
                                    {(!player.inventory || player.inventory.length === 0) && (
                                        <span className={styles.emptyInventory}>No items</span>
                                    )}
                                </div>
                            </div>

                            <div className={styles.buttonGroup}>
                                <button
                                    onClick={() => handleResetStats(player.playerID)}
                                    className={`${styles.button} ${styles.warningButton}`}
                                    disabled={loading}
                                >
                                    <span className={styles.buttonIcon}>🔄</span> Reset Stats
                                </button>
                                <button
                                    onClick={() => handleDeletePlayer(player.playerID)}
                                    className={`${styles.button} ${styles.deleteButton}`}
                                    disabled={loading}
                                >
                                    <span className={styles.buttonIcon}>🗑️</span> Delete Player
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
                {players.length === 0 && (
                    <div className={styles.emptyState}>
                        No players found
                    </div>
                )}
            </div>
        </div>
    );
};

export default PlayerManager; 