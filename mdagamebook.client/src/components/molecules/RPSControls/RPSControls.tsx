import React from 'react';
import Button from '../../atoms/Button/Button';
import styles from './RPSControls.module.css';

interface RPSControlsProps {
  onChoice: (choice: string) => void;
  disabled: boolean;
}

const RPSControls: React.FC<RPSControlsProps> = ({ onChoice, disabled }) => {
  return (
    <div className={styles.choices}>
      <Button onClick={() => onChoice('rock')} disabled={disabled}>
        Rock
      </Button>
      <Button onClick={() => onChoice('paper')} disabled={disabled}>
        Paper
      </Button>
      <Button onClick={() => onChoice('scissors')} disabled={disabled}>
        Scissors
      </Button>
    </div>
  );
};

export default RPSControls; 