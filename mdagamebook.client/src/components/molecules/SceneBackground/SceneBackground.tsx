import React from 'react';
import styles from './SceneBackground.module.css';

const SceneBackground: React.FC<{ imageUrl: string; isTransitioning: boolean }> = ({
    imageUrl,
    isTransitioning
}) => (
    <div className={styles.background}>
        <img
            className={`${styles.image} ${isTransitioning ? styles.transitioning : ''}`}
            src={imageUrl}
            alt=""
            draggable={false}
        />
    </div>
);

export default SceneBackground; 