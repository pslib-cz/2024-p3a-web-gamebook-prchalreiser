import React, { useState } from 'react';
import RPSControls from '../../molecules/RPSControls/RPSControls';
import styles from './RPSGame.module.css';

interface RPSGameProps {
  minigameId: string;
  description: string;
  opponentName: string;
  playerScore: number;
  computerScore: number;
  isCompleted: boolean;
  onPlay: (minigameId: string, choice: string) => Promise<any>;
}

const RPSGame: React.FC<RPSGameProps> = ({
  minigameId,
  description,
  opponentName,
  playerScore,
  computerScore,
  isCompleted,
  onPlay
}) => {
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState<string | null>(null);

  const handleChoice = async (choice: string) => {
    if (loading || isCompleted) return;

    try {
      setLoading(true);
      const gameResult = await onPlay(minigameId, choice);
      setResult(gameResult.result);
    } catch (error) {
      console.error('Failed to play:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className={styles.minigameWrapper}>
      <div className={styles.minigameContainer}>
        <p className={styles.minigameDescription}>{description}</p>
        <div className={styles.scoreBoard}>
          <div>
            <p>You</p>
            <p>{playerScore}</p>
          </div>
          <div>
            <p>{opponentName}</p>
            <p>{computerScore}</p>
          </div>
        </div>
        {result && !isCompleted && (
          <div className={styles.roundResult}>
            <p>Round {result.toUpperCase()}</p>
          </div>
        )}
        {!isCompleted && (
          <RPSControls onChoice={handleChoice} disabled={loading} />
        )}
      </div>
    </div>
  );
};

export default RPSGame; 