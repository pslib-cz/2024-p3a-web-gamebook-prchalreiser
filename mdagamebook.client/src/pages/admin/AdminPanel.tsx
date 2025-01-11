import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import LocationsManager from './LocationsManager';
import LinksManager from './LinksManager';
import ShopManager from './ShopManager';
import ItemManager from './ItemManager';
import PlayerManager from './PlayerManager';
import styles from './AdminPanel.module.css';
import { useAuth } from '../../contexts/AuthContext';
import { API_URL } from '../../config/env';

type TabType = 'locations' | 'links' | 'shops' | 'items' | 'players';

const AdminPanel = () => {
    const [activeTab, setActiveTab] = useState<TabType>('locations');
    const [isAdmin, setIsAdmin] = useState<boolean | null>(null);
    const [error, setError] = useState<string | null>(null);
    const { token } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const checkAdminStatus = async () => {
            try {
                const response = await fetch(`${API_URL}/api/locations`, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });

                if (response.ok) {
                    setIsAdmin(true);
                    setError(null);
                } else {
                    console.error('Admin check failed: Not authorized');
                    setIsAdmin(false);
                    setError('Not authorized');
                    navigate('/', { replace: true });
                }
            } catch (error) {
                console.error('Admin check error:', error);
                setIsAdmin(false);
                setError('Failed to check admin status');
                navigate('/', { replace: true });
            }
        };

        checkAdminStatus();
    }, [token, navigate]);

    if (isAdmin === null) {
        return <div className={styles.loading}>Loading...</div>;
    }

    if (!isAdmin) {
        return <div className={styles.error}>{error}</div>;
    }

    const renderActiveTab = () => {
        switch (activeTab) {
            case 'locations':
                return <LocationsManager />;
            case 'links':
                return <LinksManager />;
            case 'shops':
                return <ShopManager />;
            case 'items':
                return <ItemManager />;
            case 'players':
                return <PlayerManager />;
            default:
                return null;
        }
    };

    return (
        <div className={styles['admin-panel']}>
            <div className={styles.header}>
                <h1>Game Management</h1>
                <div className={styles.tabs}>
                    <button
                        className={`${styles.tab} ${activeTab === 'locations' ? styles.active : ''}`}
                        onClick={() => setActiveTab('locations')}
                    >
                        Locations
                    </button>
                    <button
                        className={`${styles.tab} ${activeTab === 'links' ? styles.active : ''}`}
                        onClick={() => setActiveTab('links')}
                    >
                        Links
                    </button>
                    <button
                        className={`${styles.tab} ${activeTab === 'shops' ? styles.active : ''}`}
                        onClick={() => setActiveTab('shops')}
                    >
                        Shops
                    </button>
                    <button
                        className={`${styles.tab} ${activeTab === 'items' ? styles.active : ''}`}
                        onClick={() => setActiveTab('items')}
                    >
                        Items
                    </button>
                    <button
                        className={`${styles.tab} ${activeTab === 'players' ? styles.active : ''}`}
                        onClick={() => setActiveTab('players')}
                    >
                        Players
                    </button>
                </div>
            </div>

            {renderActiveTab()}
        </div>
    );
};

export default AdminPanel; 