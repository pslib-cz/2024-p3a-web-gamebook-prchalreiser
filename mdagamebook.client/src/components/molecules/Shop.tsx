import styles from './Shop.module.css';
import { truncateText } from '../../utils/textUtils';

interface ShopItem {
  shopItemID: number;
  itemID: number;
  price: number;
  quantity: number;
  item: {
    name: string;
    description: string;
  };
}

interface ShopProps {
  shopItems: ShopItem[];
  onPurchase: (shopItemId: number) => void;
}

export const Shop: React.FC<ShopProps> = ({ shopItems, onPurchase }) => (
  <div className={styles.shopWrapper}>
    <div className={styles.shopContainer}>
      <h2 className={styles.shopTitle}>Shop</h2>
      <div className={styles.shopItems}>
        {shopItems.map((item) => (
          <div key={item.shopItemID} className={styles.shopItem}>
            <h3>{item.item.name}</h3>
            <p>
              {window.matchMedia("(max-width: 768px) and (orientation: landscape)").matches
                ? truncateText(item.item.description, 50)
                : item.item.description}
            </p>
            <p className={styles.itemPrice}>
              {item.price} coins
              {item.quantity > 0 && ` (${item.quantity})`}
            </p>
            <button
              onClick={() => onPurchase(item.shopItemID)}
              className={styles.buyButton}
              disabled={item.quantity === 0}
            >
              {item.quantity === 0 ? "Sold Out" : "Buy"}
            </button>
          </div>
        ))}
      </div>
    </div>
  </div>
); 