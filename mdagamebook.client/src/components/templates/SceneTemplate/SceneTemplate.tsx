import React from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './SceneTemplate.module.css';
import SceneBackground from '../../molecules/SceneBackground/SceneBackground';
import SceneContent from '../../organisms/SceneContent/SceneContent';
import Shop from '../../organisms/Shop/Shop';
import RPSGame from '../../organisms/RPSGame/RPSGame';
import NumberGuessGame from '../../organisms/NumberGuessGame/NumberGuessGame';
import HomeButton from '../../atoms/HomeButton/HomeButton';
import PlayerStats from "../../PlayerStats";
import { SceneData, Link, ShopData, Minigame } from '../../../types';

interface SceneTemplateProps {
    sceneData: SceneData;
    links: Link[];
    shop: ShopData;
    minigame: Minigame;
    isPortrait: boolean;
    isTransitioning: boolean;
    currentSceneBuffer: SceneData;
    nextSceneBuffer: SceneData;
    hasItem: boolean;
    onNavigate: (locationId: number) => void;
    onCollectItem: () => void;
    onPurchase: (itemId: number) => void;
    onPlayRPS: (minigameId: string, choice: string) => Promise<void>;
    onPlayNumberGuess: (numbers: { number1: string; number2: string }) => Promise<void>;
}

const SceneTemplate: React.FC<SceneTemplateProps> = ({
    sceneData,
    links,
    shop,
    minigame,
    isPortrait,
    isTransitioning,
    currentSceneBuffer,
    nextSceneBuffer,
    hasItem,
    onNavigate,
    onCollectItem,
    onPurchase,
    onPlayRPS,
    onPlayNumberGuess
}) => {
    const navigate = useNavigate();

    return (
        <div className={styles.container}>
            <PlayerStats />
            <HomeButton onClick={() => navigate("/")} />

            <SceneBackground
                imageUrl={sceneData.backgroundImageUrl}
                isActive={!isTransitioning}
                isTransitioning={isTransitioning}
            />

            {isTransitioning && (
                <>
                    <SceneBackground
                        imageUrl={currentSceneBuffer?.backgroundImageUrl}
                        isActive={false}
                        className={styles.transitionOut}
                    />
                    <SceneBackground
                        imageUrl={nextSceneBuffer?.backgroundImageUrl}
                        isActive={true}
                        className={styles.transitionIn}
                    />
                </>
            )}

            {shop ? (
                <Shop items={shop.shopItems} onPurchase={onPurchase} />
            ) : sceneData.description ? (
                <SceneContent
                    description={sceneData.description}
                    links={links}
                    onNavigate={onNavigate}
                    hasRequiredItem={sceneData.hasRequiredItem}
                    hasItem={hasItem}
                    onCollectItem={onCollectItem}
                    isPortrait={isPortrait}
                />
            ) : null}

            {minigame && (
                minigame.type === "RPS" ? (
                    <RPSGame
                        minigameId={minigame.minigameID}
                        description={minigame.description}
                        opponentName={minigame.opponentName}
                        playerScore={minigame.playerScore}
                        computerScore={minigame.computerScore}
                        isCompleted={minigame.isCompleted}
                        onPlay={onPlayRPS}
                    />
                ) : minigame.type === "NUMBERS" ? (
                    <NumberGuessGame
                        minigameId={minigame.minigameID}
                        description={minigame.description}
                        onSubmit={onPlayNumberGuess}
                    />
                ) : null
            )}
        </div>
    );
};

export default SceneTemplate; 