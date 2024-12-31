import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";

const SignInPage = () => {
    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const { login } = useAuth();

    const handleLogin = async (email: string, password: string) => {
        setLoading(true);
        try {
            await login(email, password);
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
                    handleLogin(email, password);
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