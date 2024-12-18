import { useState } from "react";
import { useNavigate } from "react-router-dom";

const SignInPage = () => {
    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    const navigate = useNavigate();

    const loginUser = async (email: string, password: string) => {
        setLoading(true);
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
            
            // Store the token - using accessToken as that's what the API returns
            localStorage.setItem("accessToken", data.accessToken);

            // Create a new player if one doesn't exist
            const createPlayerResponse = await fetch("https://localhost:7260/api/Players", {
                method: "POST",
                headers: {
                    "Authorization": `Bearer ${data.accessToken}`,
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

            // If player creation fails but not because of conflict (409), throw error
            if (!createPlayerResponse.ok && createPlayerResponse.status !== 409) {
                throw new Error("Failed to create player");
            }

            // Now verify the token
            const verifyResponse = await fetch("https://localhost:7260/api/user/manage/info", {
                method: "GET",
                headers: {
                    "Authorization": `Bearer ${data.accessToken}`
                }
            });

            if (!verifyResponse.ok) {
                throw new Error("Token verification failed");
            }

            navigate("/scene/420"); // Navigate to the starting scene
        } catch (error) {
            if (error instanceof Error) {
                setError(error);
            } else {
                setError(new Error("Přihlášení nebylo úspěšné"));
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h1>Přihlášení</h1>
            {error && <h1>{error.message}</h1>}
            <form
                onSubmit={(event) => {
                    event.preventDefault();
                    const form = event.target as HTMLFormElement;
                    const email = form.email.value;
                    const password = form.password.value;
                    loginUser(email, password);
                }}
            >
                <div>
                    <label htmlFor="email">Email</label>
                    <input type="email" id="email" name="email" required />
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input type="password" id="password" name="password" required />
                </div>
                <button type="submit" disabled={loading}>
                    Přihlásit
                </button>
            </form>
        </div>
    );
}

export default SignInPage;