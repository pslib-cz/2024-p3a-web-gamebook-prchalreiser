import React from 'react';
import styles from './Shop.module.css';
import { ShopItem } from '../../../types';

interface ShopProps {
    items: ShopItem[];
    onPurchase: (itemId: number) => void;
}

const Shop: React.FC<ShopProps> = ({ items, onPurchase }) => {
    return (
        <div className={styles.shopGrid}>
            {items.map((item) => (
                <div key={item.shopItemID} className={styles.shopItem}>
                    <div className={styles.itemName}>{item.item.name}</div>
                    <div className={styles.itemDescription}>{item.item.description}</div>
                    <div className={styles.itemPrice}>{item.price} coins</div>
                    <button
                        className={styles.buyButton}
                        onClick={() => onPurchase(item.shopItemID)}
                        disabled={item.quantity === 0}
                    >
                        {item.quantity === 0 ? 'Sold Out' : 'Buy'}
                    </button>
                </div>
            ))}
        </div>
    );
};

export default Shop; 