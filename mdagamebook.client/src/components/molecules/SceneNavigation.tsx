import styles from './SceneNavigation.module.css';
import nextButton from "../../assets/nextbutton.svg";
import Link from "../Link";

interface SceneLink {
  linkID: number;
  toLocation: {
    locationID: number;
    name: string;
  };
  name: string | null;
}

interface SceneNavigationProps {
  links: SceneLink[];
  onNavigate: (path: string) => void;
}

export const SceneNavigation: React.FC<SceneNavigationProps> = ({ links, onNavigate }) => {
  if (links.length === 1) {
    const link = links[0];
    return (
      <button
        className={styles.continueButton}
        onClick={() => onNavigate(`/scene/${link.toLocation.locationID}`)}
        aria-label={link.name || "Continue to next scene"}
      >
        {link.name ? (
          <span className={styles.linkName}>{link.name}</span>
        ) : (
          <img src={nextButton} alt="Next" className={styles.continueTriangle} />
        )}
      </button>
    );
  }

  return (
    <div className={styles.multipleChoices}>
      {links.map((link) => (
        <Link
          key={link.linkID}
          href={`/scene/${link.toLocation.locationID}`}
          className={styles.choiceButton}
          onClick={() => onNavigate(`/scene/${link.toLocation.locationID}`)}
        >
          {link.name || `Go to ${link.toLocation.name}`}
        </Link>
      ))}
    </div>
  );
}; 