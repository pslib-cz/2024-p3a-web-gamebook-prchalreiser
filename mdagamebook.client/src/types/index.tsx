// Scene Types
export interface SceneData {
    id: number;
    name: string;
    description: string;
    backgroundImageUrl: string;
    hasShop: boolean;
    hasMinigame: boolean;
    hasRequiredItem?: boolean;
}

export interface Link {
    linkID: number;
    toLocation: {
        locationID: number;
        name: string;
    };
    name: string | null;
}

// Shop Types
export interface ShopItem {
    shopItemID: number;
    itemID: number;
    price: number;
    quantity: number;
    item: {
        name: string;
        description: string;
    };
}

export interface Shop {
    shopItems: ShopItem[];
}

export interface ShopData {
    shopItems: ShopItem[];
}

// Minigame Types
export interface MinigameBase {
    minigameID: string;
    type: 'RPS' | 'NumberGuess';
    description: string;
    winLocationID: number;
    loseLocationID: number;
}

export interface RPSMinigame extends MinigameBase {
    type: 'RPS';
    opponentName: string;
    playerScore: number;
    computerScore: number;
    isCompleted: boolean;
}

export interface NumberGuessMinigame extends MinigameBase {
    type: 'NumberGuess';
}

export type Minigame = RPSMinigame | NumberGuessMinigame;

// Component Props Types
export interface ButtonProps {
    onClick?: () => void;
    children: React.ReactNode;
    disabled?: boolean;
    className?: string;
    type?: 'button' | 'submit';
    variant?: 'primary' | 'secondary' | 'collect' | 'buy';
}

export interface HomeButtonProps {
    onClick: () => void;
}

export interface LinkProps {
    href: string;
    children: React.ReactNode;
    onClick?: () => void;
    className?: string;
}

export interface NumberGuessFormProps {
    onSubmit: (numbers: { number1: string; number2: string }) => void;
    loading: boolean;
}

export interface RPSControlsProps {
    onChoice: (choice: string) => void;
    disabled: boolean;
}

export interface SceneBackgroundProps {
    imageUrl: string;
    isActive: boolean;
    isTransitioning?: boolean;
    className?: string;
}

export interface SceneContentProps {
    description: string;
    links: Link[];
    onNavigate: (locationId: number) => void;
    hasRequiredItem?: boolean;
    hasItem: boolean;
    onCollectItem: () => void;
    isPortrait: boolean;
}

export interface SceneNavigationProps {
    links: Link[];
    onNavigate: (locationId: number) => void;
    hasRequiredItem?: boolean;
    hasItem?: boolean;
    onCollectItem?: () => void;
}

export interface ShopProps {
    items: ShopItem[];
    onPurchase: (itemId: number) => void;
}

export interface ShopItemProps {
    id: number;
    name: string;
    description: string;
    price: number;
    quantity: number;
    onPurchase: (itemId: number) => void;
}

export interface PlayerInfoProps {
    playerName: string;
    lastLocation: number;
}

// Scene Component Types
export interface SceneProps {
    sceneId?: string;
}

export interface SceneTemplateProps {
    sceneData: SceneData;
    links: Link[];
    shop: Shop | null;
    minigame: Minigame | null;
    isPortrait: boolean;
    isTransitioning: boolean;
    currentSceneBuffer: SceneData | null;
    nextSceneBuffer: SceneData | null;
    hasItem: boolean;
    onNavigate: (locationId: number) => void;
    onCollectItem: () => void;
    onPurchase: (itemId: number) => void;
    onPlayRPS: (minigameId: string, choice: string) => Promise<RPSResult>;
    onPlayNumberGuess: (numbers: { number1: string; number2: string }) => Promise<void>;
}

export interface SceneState {
    sceneData: SceneData | null;
    loading: boolean;
    error: string | null;
    hasItem: boolean;
    links: Link[];
    shop: Shop | null;
    minigame: Minigame | null;
    isTransitioning: boolean;
    currentSceneBuffer: SceneData | null;
    nextSceneBuffer: SceneData | null;
    isPortrait: boolean;
}

// Game Result Types
export interface RPSGameResult {
    result: string;
    isCompleted: boolean;
    playerScore: number;
    computerScore: number;
}

export interface NumberGuessResult {
    isCorrect: boolean;
}

// Updated Scene Template Props
export interface RPSResult {
    result: string;
    isCompleted: boolean;
    playerScore: number;
    computerScore: number;
}

export interface RPSGameProps {
    minigameId: string;
    description: string;
    opponentName: string;
    playerScore: number;
    computerScore: number;
    isCompleted: boolean;
    onPlay: (minigameId: string, choice: string) => Promise<RPSResult>;
}

export interface NumberGuessGameProps {
    minigameId: string;
    description: string;
    onSubmit: (numbers: { number1: string; number2: string }) => Promise<void>;
}
