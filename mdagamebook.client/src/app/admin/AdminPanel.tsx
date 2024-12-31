import { useState } from 'react';
import LocationsManager from './LocationsManager';
import LinksManager from './LinksManager';
import styles from './AdminPanel.module.css';

const AdminPanel = () => {
    const [activeTab, setActiveTab] = useState<'locations' | 'links'>('locations');

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