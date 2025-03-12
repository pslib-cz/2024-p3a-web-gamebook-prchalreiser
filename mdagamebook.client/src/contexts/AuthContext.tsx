import { createContext, useContext, useState, ReactNode } from "react";
import { useNavigate } from "react-router-dom";
import { API_URL } from "../config/env";

interface AuthContextType {
  isAuthenticated: boolean;
  token: string | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  register: (
    email: string,
    password: string,
    playerName: string
  ) => Promise<void>;
}

interface AuthProviderProps {
  children: ReactNode;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: AuthProviderProps) {
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("accessToken")
  );
  const navigate = useNavigate();

  const login = async (email: string, password: string) => {
    try {
      const response = await fetch(`${API_URL}/api/user/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });

      if (!response.ok) {
        throw new Error("Přihlášení nebylo úspěšné");
      }

      const data = await response.json();
      localStorage.setItem("accessToken", data.accessToken);
      setToken(data.accessToken);

      // Create a new player if one doesn't exist
      await createPlayer(data.accessToken);

      // Verify the token
      await verifyToken(data.accessToken);

      // Fetch last location and redirect
      try {
        const locationResponse = await fetch(
          `${API_URL}/api/Locations/last-location`,
          {
            headers: {
              Authorization: `Bearer ${data.accessToken}`,
            },
          }
        );

        if (locationResponse.ok) {
          const locationData = await locationResponse.json();
          // Redirect to the last location if it exists
          if (locationData && locationData.locationID) {
            navigate(`/scene/${locationData.locationID}`);
          } else {
            // If no last location, redirect to default scene
            navigate("/");
          }
        } else {
          // If can't fetch last location, redirect to default scene
          navigate("/");
        }
      } catch (error) {
        console.error("Failed to fetch last location:", error);
        navigate("/"); // Fallback to default scene
      }
    } catch (error) {
      localStorage.removeItem("accessToken");
      setToken(null);
      throw error;
    }
  };

  const logout = () => {
    localStorage.removeItem("accessToken");
    setToken(null);
    navigate("/login");
  };

  const register = async (
    email: string,
    password: string,
    playerName: string
  ) => {
    try {
      // First register the user
      const registerResponse = await fetch(`${API_URL}/api/user/register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email,
          password,
          secured: "abcXYZ", // This is required by the backend
        }),
      });

      if (!registerResponse.ok) {
        throw new Error("Registration failed");
      }

      // Then login to get the token
      await login(email, password);

      // Now create the player with the name (this will use the token from login)
      const createPlayerResponse = await fetch(`${API_URL}/api/Players`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          playerID: crypto.randomUUID(),
          name: playerName,
          health: 100,
          withdrawal: 0,
          stamina: 100,
          coins: 0,
          inventory: [],
        }),
      });

      if (!createPlayerResponse.ok) {
        throw new Error("Failed to create player");
      }
    } catch (error) {
      throw error;
    }
  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated: !!token,
        token,
        login,
        logout,
        register,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

async function createPlayer(token: string) {
  try {
    const response = await fetch(`${API_URL}/api/Players/current`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    // Only create a new player if one doesn't exist
    if (!response.ok) {
      console.log("No player found for this user");
    }
  } catch (error) {
    console.error("Error checking for existing player:", error);
  }
}

async function verifyToken(token: string) {
  const response = await fetch(`${API_URL}/api/user/manage/info`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Token verification failed");
  }
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}
