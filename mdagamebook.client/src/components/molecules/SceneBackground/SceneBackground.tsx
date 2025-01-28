import React from 'react';
import styles from './SceneBackground.module.css';

interface SceneBackgroundProps {
  imageUrl: string;
  isActive: boolean;
  isTransitioning?: boolean;
  className?: string;
}

const SceneBackground: React.FC<SceneBackgroundProps> = ({
  imageUrl,
  isActive,
  isTransitioning,
  className = ''
}) => {
  return (
    <div className={`${styles.sceneBackground} ${className}`}>
      <img
        className={`${styles.backgroundImage} ${!isActive ? styles.backgroundImageHidden : ''}`}
        src={imageUrl}
        alt="Scene background"
        loading="eager"
        draggable={false}
      />
    </div>
  );
};

export default SceneBackground; 