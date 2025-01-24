import styles from './HomeButton.module.css';
import { TiHome } from 'react-icons/ti';  // Using Ionicons home icon

interface HomeButtonProps {
  onClick: () => void;
}

export const HomeButton: React.FC<HomeButtonProps> = ({ onClick }) => (
  <button
    className={styles.homeButton}
    onClick={onClick}
    aria-label="Return to home"
  >
    <TiHome className={styles.icon} />
  </button>
); 