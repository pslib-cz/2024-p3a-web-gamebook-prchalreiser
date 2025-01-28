import React from 'react';
import styles from './HomeButton.module.css';
import homeButton from '../../../assets/homebutton.svg';

interface HomeButtonProps {
  onClick: () => void;
}

const HomeButton: React.FC<HomeButtonProps> = ({ onClick }) => {
  return (
    <button
      className={styles.homeButton}
      onClick={onClick}
      aria-label="Return to home"
    >
      <img src={homeButton} alt="Home" />
    </button>
  );
};

export default HomeButton; 