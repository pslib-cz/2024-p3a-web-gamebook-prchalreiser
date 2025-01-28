import React, { useState } from 'react';
import NumberGuessForm from '../../molecules/NumberGuessForm/NumberGuessForm';
import styles from './NumberGuessGame.module.css';

interface NumberGuessGameProps {
  minigameId: string;
  description: string;
  onSubmit: (numbers: { number1: string; number2: string }) => Promise<void>;
}

const NumberGuessGame: React.FC<NumberGuessGameProps> = ({
  description,
  onSubmit
}) => {
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (numbers: { number1: string; number2: string }) => {
    try {
      setLoading(true);
      await onSubmit(numbers);
    } catch (error) {
      console.error('Failed to submit numbers:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className={styles.minigameWrapper}>
      <div className={styles.minigameContainer}>
        <p className={styles.minigameDescription}>{description}</p>
        <NumberGuessForm onSubmit={handleSubmit} loading={loading} />
      </div>
    </div>
  );
};

export default NumberGuessGame; 