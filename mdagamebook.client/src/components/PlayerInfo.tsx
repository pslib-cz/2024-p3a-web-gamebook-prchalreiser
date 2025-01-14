import { useState, useEffect, useRef } from 'react';
import styles from './PlayerInfo.module.css';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';

interface PlayerInfoProps {
    playerName: string;
    lastLocation: number;
}

const PlayerInfo = ({ playerName, lastLocation }: PlayerInfoProps) => {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const { logout } = useAuth();
    const navigate = useNavigate();
    const menuRef = useRef<HTMLDivElement>(null);

    // Close menu when clicking outside
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
                setIsMenuOpen(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    const handleLogout = () => {
        setIsMenuOpen(false);
        logout();
    };

    const handleChangeAccount = () => {
        setIsMenuOpen(false);
        navigate('/login');
    };

    return (
        <div className={styles.container} ref={menuRef}>
            <div 
                className={styles.playerButton} 
                onClick={() => setIsMenuOpen(!isMenuOpen)}
                role="button"
                tabIndex={0}
            >
                <span>{playerName}</span>
                <span className={styles.location}>Lokace: {lastLocation}</span>
            </div>
            
            {isMenuOpen && (
                <div className={styles.menu}>
                    <button onClick={handleChangeAccount}>
                        Změnit účet
                    </button>
                    <button onClick={handleLogout}>
                        Odhlásit se
                    </button>
                </div>
            )}
        </div>
    );
};

export default PlayerInfo; 