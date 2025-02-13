import { useParams, useNavigate } from "react-router-dom";
import { useState, useEffect, useCallback, useTransition } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useScene } from "../contexts/SceneContext";
import { API_URL } from "../config/env";
import SceneTemplate from "../components/templates/SceneTemplate/SceneTemplate";
import { SceneData, Link, ShopData, Minigame } from '../types';

const Scene = () => {
  const { sceneId } = useParams<{ sceneId: string }>();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [hasItem, setHasItem] = useState(false);
  const [shop, setShop] = useState<ShopData | null>(null);
  const [minigame, setMinigame] = useState<Minigame | null>(null);
  const [isTransitioning, setIsTransitioning] = useState(false);
  const [isPortrait, setIsPortrait] = useState(window.innerHeight > window.innerWidth);

  const { token } = useAuth();
  const navigate = useNavigate();
  const {
    getShopData,
    getMinigameData,
    playRPS,
    getPlayerStats,
    refreshPlayerStats,
    currentScene,
    loadInitialScene,
    switchToScene,
  } = useScene();

  // Handle device orientation
  useEffect(() => {
    const handleResize = () => {
      setIsPortrait(window.innerHeight > window.innerWidth);
    };
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  // Load initial scene
  useEffect(() => {
    if (!sceneId) return;

    const loadScene = async () => {
      try {
        setLoading(true);
        setError(null);
        await loadInitialScene(sceneId);
      } catch (err) {
        console.error('Scene loading error:', err);
        setError(err instanceof Error ? err.message : "Failed to load scene");
      } finally {
        setLoading(false);
      }
    };

    loadScene();
  }, [sceneId, loadInitialScene]);

  // Check for items
  useEffect(() => {
    if (!token || !sceneId) return;

    const checkForItem = async () => {
      try {
        const response = await fetch(`${API_URL}/api/Players/has-item/1`, {
          headers: { Authorization: `Bearer ${token}` }
        });
        if (response.ok) {
          const hasItem = await response.json();
          setHasItem(hasItem);
        }
      } catch (err) {
        console.error("Failed to check for item:", err);
      }
    };

    checkForItem();
  }, [sceneId, token]);

  // Load shop data
  useEffect(() => {
    if (!sceneId || !currentScene?.scene.hasShop) return;

    const loadShopData = async () => {
      const shopData = await getShopData(sceneId);
      setShop(shopData);
    };

    loadShopData();
  }, [sceneId, currentScene, getShopData]);

  // Load minigame data
  useEffect(() => {
    if (!sceneId || !currentScene?.scene.hasMinigame) return;

    const loadMinigameData = async () => {
      const minigameData = await getMinigameData(sceneId);
      setMinigame(minigameData);
    };

    loadMinigameData();
  }, [sceneId, currentScene, getMinigameData]);

  const handleNavigation = useCallback(async (locationId: number) => {
    try {
      // Increase withdrawal by 1 when moving to a new scene
      await fetch(`${API_URL}/api/Players/update-stats`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
          withdrawalChange: 1
        })
      });

      setIsTransitioning(true);
      setShop(null);
      switchToScene(locationId.toString());
      navigate(`/scene/${locationId}`, { replace: true });
      await Promise.all([
        new Promise(resolve => setTimeout(resolve, 300)),
        refreshPlayerStats() // Refresh stats to show new withdrawal value
      ]);
      setIsTransitioning(false);
    } catch (error) {
      console.error('Failed to update withdrawal:', error);
    }
  }, [navigate, switchToScene, token, refreshPlayerStats]);

  const handleCollectItem = async () => {
    try {
      const response = await fetch(
        `${API_URL}/api/Locations/${sceneId}/collect-item`,
        {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Failed to collect item");
      }

      setHasItem(true);
      alert(data.message);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to collect item");
    }
  };

  const handlePurchase = async (shopItemId: number) => {
    try {
      const response = await fetch(`${API_URL}/api/shops/buy`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({ shopItemId })
      });

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || 'Purchase failed');
      }

      alert(data.message);
      
      const [updatedShop] = await Promise.all([
        getShopData(sceneId!),
        getPlayerStats()
      ]);
      
      setShop(updatedShop);

    } catch (error) {
      alert(error instanceof Error ? error.message : 'Failed to purchase item');
    }
  };

  const handlePlayRPS = async (minigameId: string, choice: string) => {
    const result = await playRPS(minigameId, choice);
    setMinigame(prevMinigame => ({
        ...prevMinigame!,
        playerScore: result.playerScore,
        computerScore: result.computerScore,
        isCompleted: result.isCompleted
    }));
    
    if (result.isCompleted) {
        // Decrease stamina by 20 when minigame is completed
        await fetch(`${API_URL}/api/Players/update-stats`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({
                staminaChange: -20
            })
        });
        
        await refreshPlayerStats(); // Refresh stats to show new stamina value
        const targetLocationId = result.playerScore >= 3 ? minigame.winLocationID : minigame.loseLocationID;
        navigate(`/scene/${targetLocationId}`);
    }
    return result;
  };

  const handlePlayNumberGuess = async (numbers: { number1: string; number2: string }) => {
    try {
      const response = await fetch(`${API_URL}/api/Minigames/${minigame.minigameID}/play-numbers`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
          number1: parseInt(numbers.number1),
          number2: parseInt(numbers.number2)
        })
      });

      const result = await response.json();

      // Decrease stamina by 20 after playing number guess game
      await fetch(`${API_URL}/api/Players/update-stats`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({
          staminaChange: -20
        })
      });

      await refreshPlayerStats(); // Refresh stats to show new stamina value
      const targetLocationId = result.isCorrect ? minigame.winLocationID : minigame.loseLocationID;
      navigate(`/scene/${targetLocationId}`);
    } catch (error) {
      console.error('Failed to play:', error);
    }
  };

  if (loading) return (
    <div style={{ 
        position: 'fixed', 
        top: 0, 
        left: 0, 
        width: '100vw', 
        height: '100vh',
        background: '#1a1a1a',
        color: 'white',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center'
    }}>
        Loading your adventure...
    </div>
  );

  if (error) return (
    <div style={{ 
        position: 'fixed', 
        top: 0, 
        left: 0, 
        width: '100vw', 
        height: '100vh',
        background: '#1a1a1a',
        color: 'white',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flexDirection: 'column',
        padding: '20px'
    }}>
        <div>Error: {error}</div>
        <button 
            onClick={() => window.location.reload()} 
            style={{ 
                marginTop: '20px',
                padding: '10px 20px',
                background: '#333',
                border: 'none',
                color: 'white',
                cursor: 'pointer'
            }}
        >
            Retry
        </button>
    </div>
  );

  if (!currentScene) return null;

  return (
    <SceneTemplate
      sceneData={currentScene.scene}
      links={currentScene.links}
      shop={shop}
      minigame={minigame}
      isPortrait={isPortrait}
      isTransitioning={isTransitioning}
      hasItem={hasItem}
      onNavigate={handleNavigation}
      onCollectItem={handleCollectItem}
      onPurchase={handlePurchase}
      onPlayRPS={handlePlayRPS}
      onPlayNumberGuess={handlePlayNumberGuess}
    />
  );
};

export default Scene;
