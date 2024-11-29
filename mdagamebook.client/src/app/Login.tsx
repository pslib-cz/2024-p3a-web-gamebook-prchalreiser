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
                body: JSON.stringify({ email, password }),
                mode: "cors",
            });
            if (!response.ok) {
                throw new Error("Přihlášení nebylo úspěšné");
            }
            const data = await response.json();
            localStorage.setItem("accessToken", data.accessToken);

            console.log(localStorage.getItem("accessToken"));
            navigate("/upload"); 
        } catch (error) {
            if (error instanceof Error) {
                setError(error);
            }
            else {
                setError(new Error("Registrace nebyla úspěšná"));
            }
        }
        finally {
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