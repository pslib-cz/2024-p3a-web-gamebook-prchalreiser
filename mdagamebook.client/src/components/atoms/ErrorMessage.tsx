import styles from './ErrorMessage.module.css';

interface ErrorMessageProps {
  message: string;
}

export const ErrorMessage: React.FC<ErrorMessageProps> = ({ message }) => (
  <div className={styles.error}>{message}</div>
); 