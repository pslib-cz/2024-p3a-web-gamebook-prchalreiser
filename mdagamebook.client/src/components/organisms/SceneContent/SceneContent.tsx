import React from 'react';
import SceneNavigation from '../../molecules/SceneNavigation/SceneNavigation';
import styles from './SceneContent.module.css';
import { Link } from '../../../types';

interface SceneContentProps {
    description: string;
    links: Link[];
    onNavigate: (locationId: number) => void;
    hasRequiredItem: boolean;
    hasItem: boolean;
    onCollectItem: () => void;
    isPortrait: boolean;
}

const SceneContent: React.FC<SceneContentProps> = ({
    description,
    links,
    onNavigate,
    hasRequiredItem,
    hasItem,
    onCollectItem,
    isPortrait
}) => {
    const renderDescription = () => {
        if (!description) return null;

        if (window.matchMedia("(max-width: 768px) and (orientation: landscape)").matches) {
            return description.length > 300 ? `${description.substring(0, 300)}...` : description;
        }
        return description;
    };

    return (
        <div className={styles.sceneContentWrapper}>
            <div className={styles.textBoxMultiple}>
                <div className={styles.description}>{renderDescription()}</div>
            </div>
            <SceneNavigation
                links={links}
                onNavigate={onNavigate}
                hasRequiredItem={hasRequiredItem}
                hasItem={hasItem}
                onCollectItem={onCollectItem}
            />
            {isPortrait && (
                <div className={styles.rotateDevice}>
                    <div className={styles.rotateMessage}>
                        Please rotate your device to landscape mode for the best experience
                    </div>
                </div>
            )}
        </div>
    );
};

export default SceneContent; 