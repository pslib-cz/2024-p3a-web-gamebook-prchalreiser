import React, { useState } from 'react';
import Button from '../../atoms/Button/Button';
import styles from './NumberGuessForm.module.css';

interface NumberGuessFormProps {
  onSubmit: (numbers: { number1: string; number2: string }) => void;
  loading: boolean;
}

const NumberGuessForm: React.FC<NumberGuessFormProps> = ({ onSubmit, loading }) => {
  const [numbers, setNumbers] = useState({ number1: '', number2: '' });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(numbers);
  };

  return (
    <form onSubmit={handleSubmit} className={styles.numberGuessForm}>
      <div className={styles.numberInputs}>
        <input
          type="number"
          value={numbers.number1}
          onChange={(e) => setNumbers(prev => ({ ...prev, number1: e.target.value }))}
          placeholder="Číslo 1"
          required
        />
        <input
          type="number"
          value={numbers.number2}
          onChange={(e) => setNumbers(prev => ({ ...prev, number2: e.target.value }))}
          placeholder="Číslo 2"
          required
        />
      </div>
      <Button type="submit" disabled={loading}>
        {loading ? 'Potvrzuji...' : 'Potvrdit'}
      </Button>
    </form>
  );
};

export default NumberGuessForm; 