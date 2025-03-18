import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import styles from "./Login.module.css";

const EyeIcon = () => (
    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#666" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
        <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z" />
        <circle cx="12" cy="12" r="3" />
    </svg>
);

const EyeOffIcon = () => (
    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#666" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
        <path d="M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24" />
        <line x1="1" y1="1" x2="23" y2="23" />
    </svg>
);

const SignInPage = () => {
    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [showPassword, setShowPassword] = useState<boolean>(false);
    const [isRegistering, setIsRegistering] = useState<boolean>(false);
    const { login, register } = useAuth();

    const handleSubmit = async (email: string, password: string, playerName: string) => {
        setLoading(true);
        try {
            if (isRegistering) {
                await register(email, password, playerName);
            } else {
                await login(email, password);
            }
        } catch (error) {
            setError(new Error(isRegistering ?
                "Registrace se nezdařila! " + error :
                "Nesprávné přihlašovací údaje! " + error
            ));
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.pageContainer}>
            <div className={styles.loginContainer}>
                <div className={styles.glowEffect} />
                <h1 className={styles.title}>{isRegistering ? 'Registrace' : 'Přihlášení'}</h1>
                {error && <div className={styles.error}>{error.message}</div>}
                <form
                    onSubmit={(event) => {
                        event.preventDefault();
                        const form = event.target as HTMLFormElement;
                        const email = form.email.value;
                        const password = form.password.value;
                        const playerName = form.playerName?.value || '';
                        handleSubmit(email, password, playerName);
                    }}
                >
                    {isRegistering && (
                        <div className={styles.formGroup}>
                            <label htmlFor="playerName" className={styles.label}>
                                Jméno Postavy
                            </label>
                            <div className={styles.inputWrapper}>
                                <input
                                    type="text"
                                    id="playerName"
                                    name="playerName"
                                    className={styles.input}
                                    placeholder="Zadejte jméno postavy"
                                    required
                                />
                            </div>
                        </div>
                    )}
                    <div className={styles.formGroup}>
                        <label htmlFor="email" className={styles.label}>
                            Email
                        </label>
                        <div className={styles.inputWrapper}>
                            <input
                                type="email"
                                id="email"
                                name="email"
                                className={styles.input}
                                placeholder="vas@email.cz"
                                required
                            />
                        </div>
                    </div>
                    <div className={styles.formGroup}>
                        <label htmlFor="password" className={styles.label}>
                            Heslo
                        </label>
                        <div className={styles.inputWrapper}>
                            <input
                                type={showPassword ? "text" : "password"}
                                id="password"
                                name="password"
                                className={styles.input}
                                placeholder="••••••••"
                                required
                            />
                            <button
                                type="button"
                                className={styles.togglePassword}
                                onClick={() => setShowPassword(!showPassword)}
                                aria-label={showPassword ? "Skrýt heslo" : "Zobrazit heslo"}
                            >
                                {showPassword ? <EyeOffIcon /> : <EyeIcon />}
                            </button>
                        </div>
                    </div>
                    <button
                        type="submit"
                        className={styles.submitButton}
                        disabled={loading}
                    >
                        {loading ? "Načítání..." : (isRegistering ? "Registrovat" : "Přihlásit")}
                    </button>

                    <button
                        type="button"
                        className={styles.switchButton}
                        onClick={() => setIsRegistering(!isRegistering)}
                    >
                        {isRegistering ? "Již máte účet? Přihlaste se" : "Nemáte účet? Zaregistrujte se"}
                    </button>
                </form>
            </div>
        </div>
    );
};

export default SignInPage;