import styles from './SceneBuffer.module.css';

interface SceneData {
  name: string;
  backgroundImageUrl: string;
}

interface SceneBufferProps {
  sceneData: SceneData | null;
  isActive: boolean;
  isTransitioning: boolean;
  className?: string;
}

export const SceneBuffer: React.FC<SceneBufferProps> = ({
  sceneData,
  isActive,
  className,
}) => {
  if (!sceneData) return null;

  return (
    <div className={`${styles.sceneBuffer} ${className || ""}`}>
      <img
        className={`${styles.backgroundImage} ${
          !isActive ? styles.backgroundImageHidden : ""
        }`}
        src={sceneData.backgroundImageUrl}
        alt={sceneData.name}
        loading="eager"
        draggable={false}
      />
    </div>
  );
}; 