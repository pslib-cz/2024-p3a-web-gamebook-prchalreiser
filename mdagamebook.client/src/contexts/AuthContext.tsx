import { createContext, useContext, useState, ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';

interface AuthContextType {
    isAuthenticated: boolean;
    token: string | null;
    login: (email: string, password: string) => Promise<void>;
    logout: () => void;
    register: (email: string, password: string) => Promise<void>;
}

interface AuthProviderProps {
    children: ReactNode;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: AuthProviderProps) {
    const [token, setToken] = useState<string | null>(localStorage.getItem('accessToken'));
    const navigate = useNavigate();

    const login = async (email: string, password: string) => {
        try {
            const response = await fetch("https://localhost:7260/api/user/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ email, password })
            });

            if (!response.ok) {
                throw new Error("Přihlášení nebylo úspěšné");
            }

            const data = await response.json();
            localStorage.setItem("accessToken", data.accessToken);
            setToken(data.accessToken);

            // Create a new player if one doesn't exist
            await createPlayer(data.accessToken, email);

            // Verify the token
            await verifyToken(data.accessToken);

            // Fetch last location and redirect
            try {
                const locationResponse = await fetch('https://localhost:7260/api/Locations/last-location', {
                    headers: {
                        'Authorization': `Bearer ${data.accessToken}`
                    }
                });

                if (locationResponse.ok) {
                    const locationData = await locationResponse.json();
                    // Redirect to the last location if it exists
                    if (locationData && locationData.locationID) {
                        navigate(`/scene/${locationData.locationID}`);
                    } else {
                        // If no last location, redirect to default scene
                        navigate('/');
                    }
                } else {
                    // If can't fetch last location, redirect to default scene
                    navigate('/');
                }
            } catch (error) {
                console.error('Failed to fetch last location:', error);
                navigate('/'); // Fallback to default scene
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

    const register = async (email: string, password: string) => {
        try {
            const response = await fetch("https://localhost:7260/api/user/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ 
                    email, 
                    password,
                    secured: "abcXYZ" // This is required by the backend
                })
            });

            if (!response.ok) {
                throw new Error("Registrace se nezdařila");
            }

            // After successful registration, log the user in
            await login(email, password);
        } catch (error) {
            throw error;
        }
    };

    return (
        <AuthContext.Provider value={{
            isAuthenticated: !!token,
            token,
            login,
            logout,
            register
        }}>
            {children}
        </AuthContext.Provider>
    );
}

async function createPlayer(token: string, email: string) {
    const response = await fetch("https://localhost:7260/api/Players", {
        method: "POST",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            playerID: crypto.randomUUID(),
            name: email,
            health: 100,
            withdrawal: 0,
            stamina: 100,
            coins: 0,
            inventory: []
        })
    });

    if (!response.ok && response.status !== 409) {
        throw new Error("Failed to create player");
    }
}

async function verifyToken(token: string) {
    const response = await fetch("https://localhost:7260/api/user/manage/info", {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    if (!response.ok) {
        throw new Error("Token verification failed");
    }
}

export function useAuth() {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
} 