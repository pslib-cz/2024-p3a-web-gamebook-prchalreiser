import { useParams, useNavigate } from "react-router-dom";
import { useState, useEffect, useCallback } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useScene } from "../contexts/SceneContext";
import { API_URL } from "../config/env";
import SceneTemplate from "../components/templates/SceneTemplate/SceneTemplate";
import { SceneData, Link, ShopData, Minigame } from '../types';

const Scene = () => {
  const { sceneId } = useParams<{ sceneId: string }>();
  const [sceneData, setSceneData] = useState<SceneData | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [hasItem, setHasItem] = useState(false);
  const [links, setLinks] = useState<Link[]>([]);
  const [shop, setShop] = useState<ShopData | null>(null);
  const [minigame, setMinigame] = useState<Minigame | null>(null);
  const [isTransitioning, setIsTransitioning] = useState(false);
  const [currentSceneBuffer, setCurrentSceneBuffer] = useState<SceneData | null>(null);
  const [nextSceneBuffer, setNextSceneBuffer] = useState<SceneData | null>(null);
  const [isPortrait, setIsPortrait] = useState(window.innerHeight > window.innerWidth);
  const [sceneTransitions, setSceneTransitions] = useState(0);

  const { token } = useAuth();
  const navigate = useNavigate();
  const {
    getSceneData,
    preloadNextScenes,
    getShopData,
    getMinigameData,
    playRPS,
    getPlayerStats,
  } = useScene();

  useEffect(() => {
    const handleResize = () => {
      setIsPortrait(window.innerHeight > window.innerWidth);
    };

    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  const preloadImage = (url: string): Promise<void> => {
    return new Promise((resolve, reject) => {
      const img = new Image();
      img.onload = () => resolve();
      img.onerror = reject;
      img.src = url;
    });
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const { scene, links } = await getSceneData(sceneId!);
        
        // Preload current scene image
        await preloadImage(scene.backgroundImageUrl);
        
        setSceneData(scene);
        setLinks(links);
        
        // Preload all linked scenes' images in the background
        links.forEach(link => {
          getSceneData(link.toLocation.locationID.toString())
            .then(({ scene }) => preloadImage(scene.backgroundImageUrl))
            .catch(console.error);
        });
      } catch (err) {
        setError(err instanceof Error ? err.message : "Unexpected error");
      } finally {
        setLoading(false);
      }
    };

    if (sceneId) {
      fetchData();
    }
  }, [sceneId, getSceneData]);

  useEffect(() => {
    const checkForItem = async () => {
      try {
        const response = await fetch(`${API_URL}/api/Players/has-item/1`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
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
      fetchData();
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to collect item");
    }
  };

  const handleNavigation = useCallback(
    async (locationId: number) => {
      try {
        setIsTransitioning(true);

        // Get and ensure next scene data and image are fully loaded before proceeding
        const { scene: nextSceneData } = await getSceneData(locationId.toString());

        // Handle withdrawal logic
        const newTransitions = sceneTransitions + 1;
        setSceneTransitions(newTransitions);
        
        if (newTransitions % 2 === 0) {
          const response = await fetch(`${API_URL}/api/Players/update-withdrawal`, {
            method: 'POST',
            headers: {
              'Authorization': `Bearer ${token}`,
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({ increase: 5 })
          });

          if (!response.ok) {
            console.error('Failed to update withdrawal');
          } else {
            await getPlayerStats();
          }
        }

        setCurrentSceneBuffer(sceneData);
        setNextSceneBuffer(nextSceneData);
        setSceneData(nextSceneData);

        await new Promise((resolve) => requestAnimationFrame(resolve));
        await new Promise((resolve) => setTimeout(resolve, 500));

        navigate(`/scene/${locationId}`, { replace: true });
      } catch (error) {
        console.error("Navigation failed:", error);
        navigate(`/scene/${locationId}`);
      } finally {
        setIsTransitioning(false);
        setCurrentSceneBuffer(null);
        setNextSceneBuffer(null);
      }
    },
    [navigate, sceneData, getSceneData, getPlayerStats, sceneTransitions, token]
  );

  useEffect(() => {
    const loadShopData = async () => {
      if (!sceneId || !sceneData?.hasShop) return;
      const shopData = await getShopData(sceneId);
      setShop(shopData);
    };
    loadShopData();
  }, [sceneId, getShopData, sceneData?.hasShop]);

  useEffect(() => {
    const loadMinigameData = async () => {
      if (!sceneId || !sceneData?.hasMinigame) return;
      const minigameData = await getMinigameData(sceneId);
      setMinigame(minigameData);
    };
    loadMinigameData();
  }, [sceneId, getMinigameData, sceneData?.hasMinigame]);

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
      
      // Refresh both shop and player stats
      const [updatedShop] = await Promise.all([
        getShopData(sceneId!),
        getPlayerStats() // This will trigger PlayerStats component update
      ]);
      
      setShop(updatedShop);

    } catch (error) {
      alert(error instanceof Error ? error.message : 'Failed to purchase item');
    }
  };

  const handlePlayRPS = async (minigameId: string, choice: string) => {
    const result = await playRPS(minigameId, choice);
    if (result.isCompleted && minigame) {
      const targetLocationId = result.playerScore >= 3 ? minigame.winLocationID : minigame.loseLocationID;
      navigate(`/scene/${targetLocationId}`);
    }
    return result;
  };

  const handlePlayNumberGuess = async (numbers: { number1: string; number2: string }) => {
    try {
      if (!minigame) return;
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
      const targetLocationId = result.isCorrect ? minigame.winLocationID : minigame.loseLocationID;
      navigate(`/scene/${targetLocationId}`);
    } catch (error) {
      console.error('Failed to play:', error);
    }
  };

  if (loading) {
    return <div>Loading your adventure...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  if (!sceneData) {
    return <div>No scene data found</div>;
  }

  return (
    <SceneTemplate
      sceneData={sceneData}
      links={links}
      shop={shop}
      minigame={minigame}
      isPortrait={isPortrait}
      isTransitioning={isTransitioning}
      currentSceneBuffer={currentSceneBuffer}
      nextSceneBuffer={nextSceneBuffer}
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
