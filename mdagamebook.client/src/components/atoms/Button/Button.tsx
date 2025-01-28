import React from 'react';
import styles from './Button.module.css';
import Link from '../Link';

interface ButtonProps {
  onClick?: () => void;
  children: React.ReactNode;
  disabled?: boolean;
  className?: string;
  type?: 'button' | 'submit';
  variant?: 'primary' | 'secondary' | 'collect' | 'buy';
  href?: string;
}

const Button: React.FC<ButtonProps> = ({
  onClick,
  children,
  disabled = false,
  className = '',
  type = 'button',
  variant = 'primary',
  href
}) => {
  // If href is provided, render as Link
  if (href) {
    return (
      <Link
        href={href}
        onClick={onClick}
        className={`${styles.button} ${styles[variant]} ${className}`}
      >
        {children}
      </Link>
    );
  }

  // Otherwise render as button
  return (
    <button
      type={type}
      onClick={onClick}
      disabled={disabled}
      className={`${styles.button} ${styles[variant]} ${className}`}
    >
      {children}
    </button>
  );
};

export default Button; 