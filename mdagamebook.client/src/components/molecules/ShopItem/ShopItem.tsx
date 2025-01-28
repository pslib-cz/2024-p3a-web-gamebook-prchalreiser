import React from 'react';
import Button from '../../atoms/Button/Button';
import styles from './ShopItem.module.css';

interface ShopItemProps {
  id: number;
  name: string;
  description: string;
  price: number;
  quantity: number;
  onPurchase: (itemId: number) => void;
}

const ShopItem: React.FC<ShopItemProps> = ({
  id,
  name,
  description,
  price,
  quantity,
  onPurchase
}) => {
  const truncatedDescription = window.matchMedia("(max-width: 768px) and (orientation: landscape)").matches
    ? description.length > 50 ? `${description.substring(0, 50)}...` : description
    : description;

  return (
    <div className={styles.shopItem}>
      <h3>{name}</h3>
      <p>{truncatedDescription}</p>
      <p className={styles.itemPrice}>
        {price} coins
        {quantity > 0 && ` (${quantity})`}
      </p>
      <Button
        variant="buy"
        onClick={() => onPurchase(id)}
        disabled={quantity === 0}
      >
        {quantity === 0 ? "Sold Out" : "Buy"}
      </Button>
    </div>
  );
};

export default ShopItem; 