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

  const fetchData = async () => {
    try {
      setLoading(true);
      const { scene, links } = await getSceneData(sceneId!);
      setSceneData(scene);
      setLinks(links);
      preloadNextScenes(links);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Unexpected error");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (sceneId) {
      fetchData();
    }
  }, [sceneId]);

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

        const [{ scene: nextSceneData }, playerStats] = await Promise.all([
          getSceneData(locationId.toString()),
          getPlayerStats(),
          new Promise((resolve, reject) => {
            const img = new Image();
            img.onload = resolve;
            img.onerror = reject;
            img.src = nextSceneData.backgroundImageUrl;
          })
        ]);

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
    [navigate, sceneData, getSceneData, getPlayerStats]
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
      const updatedShop = await getShopData(sceneId!);
      setShop(updatedShop);

    } catch (error) {
      alert(error instanceof Error ? error.message : 'Failed to purchase item');
    }
  };

  const handlePlayRPS = async (minigameId: string, choice: string) => {
    const result = await playRPS(minigameId, choice);
    if (result.isCompleted) {
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
