import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import LocationsManager from './LocationsManager';
import LinksManager from './LinksManager';
import styles from './AdminPanel.module.css';
import { useAuth } from '../../contexts/AuthContext';
import { API_URL } from '../../config/env';

const AdminPanel = () => {
    const [activeTab, setActiveTab] = useState<'locations' | 'links'>('locations');
    const [isAdmin, setIsAdmin] = useState<boolean | null>(null);
    const [error, setError] = useState<string | null>(null);
    const { token } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const checkAdminStatus = async () => {
            try {
                // Try to fetch locations - this endpoint requires admin role
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
                </div>
            </div>

            {activeTab === 'locations' ? <LocationsManager /> : <LinksManager />}
        </div>
    );
};

export default AdminPanel; 