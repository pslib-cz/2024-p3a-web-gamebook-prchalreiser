import styles from "./StartingPage.module.css";
import Link from "../components/Link";
import RainbowSpiral from "../components/RainbowSpiral";

const StartingPage = () => {
    return (
        <div className={styles.container}>
            <h1 className={styles.title}>Multidimenzionální absťák</h1>
            <div className={styles.mainContent}>
                <div className={styles.linkWrapper}>
                    <Link href="/start">Hrát</Link>
                    <Link href="/leaderboard">Žebříčky</Link>
                </div>
                <div className={styles.spiralWrapper}>
                    <RainbowSpiral />
                </div>
            </div>
        </div>
    )
}

export default StartingPage;
