import React from "react";
import { useNavigate } from "react-router-dom";
import styles from "./SceneTemplate.module.css";
import SceneBackground from "../../molecules/SceneBackground/SceneBackground";
import SceneContent from "../../organisms/SceneContent/SceneContent";
import Shop from "../../organisms/Shop/Shop";
import RPSGame from "../../organisms/RPSGame/RPSGame";
import NumberGuessGame from "../../organisms/NumberGuessGame/NumberGuessGame";
import HomeButton from "../../atoms/HomeButton/HomeButton";
import PlayerStats from "../../PlayerStats";
import { SceneData, Link, ShopData, Minigame } from "../../../types";

interface SceneTemplateProps {
  sceneData: SceneData;
  links: Link[];
  shop: ShopData | null;
  minigame: Minigame | null;
  isPortrait: boolean;
  isTransitioning: boolean;
  hasItem: boolean;
  onNavigate: (locationId: number) => void;
  onCollectItem: () => void;
  onPurchase: (itemId: number) => void;
  onPlayRPS: (minigameId: string, choice: string) => Promise<void>;
  onPlayNumberGuess: (numbers: {
    number1: string;
    number2: string;
  }) => Promise<void>;
}

const SceneTemplate: React.FC<SceneTemplateProps> = ({
  sceneData,
  links,
  shop,
  minigame,
  isPortrait,
  isTransitioning,
  hasItem,
  onNavigate,
  onCollectItem,
  onPurchase,
  onPlayRPS,
  onPlayNumberGuess,
}) => {
  const navigate = useNavigate();

  return (
    <div className={styles.container}>
      <PlayerStats />
      <HomeButton onClick={() => navigate("/")} />

      <SceneBackground
        imageUrl={sceneData.backgroundImageUrl}
        isTransitioning={isTransitioning}
      />

      <SceneContent
        description={sceneData.description}
        links={links}
        onNavigate={onNavigate}
        hasRequiredItem={sceneData.hasRequiredItem ?? false}
        hasItem={hasItem}
        onCollectItem={onCollectItem}
        isPortrait={isPortrait}
      />

      {shop && (
        <div className={styles.shopContainer}>
          <div className={styles.shopTitle}>Shop</div>
          <Shop items={shop.shopItems} onPurchase={onPurchase} />
        </div>
      )}

      {minigame &&
        (minigame.type === "RPS" ? (
          <RPSGame
            minigameId={minigame.minigameID}
            description={minigame.description}
            opponentName={minigame.opponentName}
            playerScore={minigame.playerScore}
            computerScore={minigame.computerScore}
            isCompleted={minigame.isCompleted}
            onPlay={onPlayRPS}
          />
        ) : minigame.type === "NumberGuess" ? (
          <NumberGuessGame
            minigameId={minigame.minigameID}
            description={minigame.description}
            onSubmit={onPlayNumberGuess}
          />
        ) : null)}
    </div>
  );
};

export default SceneTemplate;
