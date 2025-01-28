import React from 'react';
import ShopItem from '../../molecules/ShopItem/ShopItem';
import styles from './Shop.module.css';

interface ShopItemType {
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
  items: ShopItemType[];
  onPurchase: (itemId: number) => void;
}

const Shop: React.FC<ShopProps> = ({ items, onPurchase }) => {
  return (
    <div className={styles.shopWrapper}>
      <div className={styles.shopContainer}>
        <h2 className={styles.shopTitle}>Shop</h2>
        <div className={styles.shopItems}>
          {items.map((item) => (
            <ShopItem
              key={item.shopItemID}
              id={item.shopItemID}
              name={item.item.name}
              description={item.item.description}
              price={item.price}
              quantity={item.quantity}
              onPurchase={onPurchase}
            />
          ))}
        </div>
      </div>
    </div>
  );
};

export default Shop; 