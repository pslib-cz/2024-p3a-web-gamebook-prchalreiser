import styles from './CollectButton.module.css';

interface CollectButtonProps {
  onClick: () => void;
}

export const CollectButton: React.FC<CollectButtonProps> = ({ onClick }) => (
  <button className={styles.collectButton} onClick={onClick}>
    Sebrat item
  </button>
); 