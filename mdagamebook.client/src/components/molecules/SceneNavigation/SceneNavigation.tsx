import React from 'react';
import Button from '../../atoms/Button/Button';
import styles from './SceneNavigation.module.css';
import nextButton from '../../../assets/nextbutton.svg';

interface Link {
  linkID: number;
  toLocation: {
    locationID: number;
    name: string;
  };
  name: string | null;
}

interface SceneNavigationProps {
  links: Link[];
  onNavigate: (locationId: number) => void;
  hasRequiredItem?: boolean;
  hasItem?: boolean;
  onCollectItem?: () => void;
}

const SceneNavigation: React.FC<SceneNavigationProps> = ({
  links,
  onNavigate,
  hasRequiredItem,
  hasItem,
  onCollectItem
}) => {
  const isSingleLink = links.length === 1;
  
  return (
    <div className={isSingleLink ? styles.navigation : styles.navigationMultiple}>
      {hasRequiredItem && !hasItem && (
        <Button variant="collect" onClick={onCollectItem}>
          Collect Item
        </Button>
      )}
      <div className={isSingleLink ? styles.singleLink : styles.multipleLinks}>
        {isSingleLink ? (
          <button
            className={styles.continueButton}
            onClick={() => onNavigate(links[0].toLocation.locationID)}
            aria-label="Continue"
          >
            <img 
              src={nextButton} 
              alt="Continue" 
              className={styles.continueTriangle}
            />
          </button>
        ) : (
          links.map((link) => (
            <Button
              key={link.linkID}
              onClick={() => onNavigate(link.toLocation.locationID)}
            >
              {link.name || `Go to ${link.toLocation.name}`}
            </Button>
          ))
        )}
      </div>
    </div>
  );
};

export default SceneNavigation; 