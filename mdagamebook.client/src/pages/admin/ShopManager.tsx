import { useState, useEffect } from "react";
import { useAuth } from "../../contexts/AuthContext";
import styles from "./AdminPanel.module.css";
import { API_URL } from "../../config/env";
interface Location {
  locationID: number;
  name: string;
  description: string;
  backgroundImageUrl: string;
  hasShop: boolean;
  hasMinigame: boolean;
  hasRequiredItem?: boolean;
  items?: string;
}

interface Shop {
  shopID: string;
  locationID: number;
  shopItems: ShopItem[];
}

interface ShopItem {
  shopItemID: number;
  itemID: number;
  price: number;
  quantity: number;
  item: {
    name: string;
    description: string;
    isDrinkable: boolean;
    effect: string;
  };
}

interface Item {
  itemID: number;
  name: string;
  description: string;
  isDrinkable: boolean;
  price: number;
  effect: string;
}

const ShopManager = () => {
  const { token } = useAuth();
  const [shops, setShops] = useState<Shop[]>([]);
  const [locations, setLocations] = useState<Location[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [editingShop, setEditingShop] = useState<Shop | null>(null);

  const [formData, setFormData] = useState({
    locationID: "",
    shopItems: [] as { itemID: number; price: number; quantity: number }[],
  });

  const [items, setItems] = useState<Item[]>([]);
  const [selectedItems, setSelectedItems] = useState<
    {
      itemID: number;
      price: number;
      quantity: number;
    }[]
  >([]);

  useEffect(() => {
    fetchShops();
    fetchLocations();
    fetchItems();
  }, [token]);

  const fetchShops = async () => {
    try {
      const response = await fetch(`${API_URL}/api/shops`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        const data = await response.json();
        setShops(data);
      }
    } catch (err) {
      setError("Failed to fetch shops!");
    }
  };

  const fetchLocations = async () => {
    try {
      const response = await fetch(`${API_URL}/api/locations`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        const data = await response.json();
        setLocations(data);
      }
    } catch (err) {
      setError("Failed to fetch locations!");
    }
  };

  const fetchItems = async () => {
    try {
      const response = await fetch(`${API_URL}/api/items`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        const data = await response.json();
        setItems(data);
      }
    } catch (err) {
      setError("Failed to fetch items!");
    }
  };

  const handleAddItem = () => {
    setSelectedItems([...selectedItems, { itemID: 0, price: 0, quantity: 1 }]);
  };

  const handleRemoveItem = (index: number) => {
    setSelectedItems(selectedItems.filter((_, i) => i !== index));
  };

  const handleItemChange = (
    index: number,
    field: "itemID" | "price" | "quantity",
    value: number
  ) => {
    const newItems = [...selectedItems];
    newItems[index] = { ...newItems[index], [field]: value };
    setSelectedItems(newItems);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);

    try {
      const shopData = {
        locationID: parseInt(formData.locationID),
      };

      // Create shop first
      const shopResponse = await fetch(`${API_URL}/api/shops`, {
        method: editingShop ? "PUT" : "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(shopData),
      });

      if (!shopResponse.ok) {
        const errorData = await shopResponse.json();
        throw new Error(
          errorData.title ||
            `Failed to ${editingShop ? "update" : "create"} shop`
        );
      }

      const shopResult = await shopResponse.json();
      const shopId = shopResult.shopID || editingShop?.shopID;

      // Add items to shop
      for (const item of selectedItems) {
        if (item.itemID === 0) continue;

        await fetch(`${API_URL}/api/shops/${shopId}/items`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(item),
        });
      }

      setSuccess(`Shop ${editingShop ? "updated" : "created"} successfully!`);
      fetchShops();
      resetForm();
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (shopId: string) => {
    if (!window.confirm("Are you sure you want to delete this shop?")) {
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`${API_URL}/api/shops/${shopId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to delete shop");
      }

      setSuccess("Shop deleted successfully!");
      fetchShops();
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
    } finally {
      setLoading(false);
    }
  };

  const resetForm = () => {
    setFormData({
      locationID: "",
      shopItems: [],
    });
    setSelectedItems([]);
    setEditingShop(null);
  };

  return (
    <div className={styles.section}>
      <h2>Shop Management</h2>

      {error && <div className={styles.error}>{error}</div>}
      {success && <div className={styles.success}>{success}</div>}

      <form onSubmit={handleSubmit} className={styles.form}>
        <div className={styles.formGroup}>
          <label htmlFor="location">Location:</label>
          <select
            id="location"
            value={formData.locationID}
            onChange={(e) =>
              setFormData((prev) => ({ ...prev, locationID: e.target.value }))
            }
            required
            className={styles.formSelect}
          >
            <option value="">Select location</option>
            {locations.map((location) => (
              <option key={location.locationID} value={location.locationID}>
                {location.name}
              </option>
            ))}
          </select>
        </div>

        <div className={styles.formGroup}>
          <label>Shop Items:</label>
          <button
            type="button"
            onClick={handleAddItem}
            className={`${styles.button} ${styles.secondaryButton}`}
          >
            Add Item
          </button>

          {selectedItems.map((item, index) => (
            <div key={index} className={styles.shopItemForm}>
              <select
                value={item.itemID}
                onChange={(e) =>
                  handleItemChange(index, "itemID", parseInt(e.target.value))
                }
                className={styles.formSelect}
              >
                <option value={0}>Select item</option>
                {items.map((i) => (
                  <option key={i.itemID} value={i.itemID}>
                    {i.name}
                  </option>
                ))}
              </select>
              <input
                type="number"
                value={item.price}
                onChange={(e) =>
                  handleItemChange(index, "price", parseInt(e.target.value))
                }
                placeholder="Price"
                className={styles.formInput}
                min="0"
              />
              <input
                type="number"
                value={item.quantity}
                onChange={(e) =>
                  handleItemChange(index, "quantity", parseInt(e.target.value))
                }
                placeholder="Quantity"
                className={styles.formInput}
                min="1"
              />
              <button
                type="button"
                onClick={() => handleRemoveItem(index)}
                className={`${styles.button} ${styles.deleteButton}`}
              >
                Remove
              </button>
            </div>
          ))}
        </div>

        <button type="submit" disabled={loading} className={styles.button}>
          {loading
            ? "Processing..."
            : editingShop
            ? "Update Shop"
            : "Create Shop"}
        </button>
      </form>

      <div className={styles.list}>
        <h3>Existing Shops</h3>
        {shops.map((shop) => (
          <div key={shop.shopID} className={styles.listItem}>
            <div>
              <h4>
                Location:{" "}
                {locations.find((l) => l.locationID === shop.locationID)?.name}
              </h4>
              <div className={styles.shopItems}>
                <h5>Items:</h5>
                {shop.shopItems.map((item) => (
                  <div key={item.shopItemID} className={styles.shopItem}>
                    <span>{item.item.name}</span>
                    <span>Price: {item.price}</span>
                    <span>Quantity: {item.quantity}</span>
                  </div>
                ))}
              </div>
            </div>
            <div className={styles.buttonGroup}>
              <button
                onClick={() => handleDelete(shop.shopID)}
                className={`${styles.button} ${styles.deleteButton}`}
              >
                Delete Shop
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ShopManager;
