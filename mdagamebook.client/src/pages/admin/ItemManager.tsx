import { useState, useEffect } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import styles from './AdminPanel.module.css';
import { API_URL } from '../../config/env';

interface Item {
    itemID: number;
    name: string;
    description: string;
    isDrinkable: boolean;
    price: number;
    effect: string;
}

const ItemManager = () => {
    const { token } = useAuth();
    const [items, setItems] = useState<Item[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<string | null>(null);
    const [editingItem, setEditingItem] = useState<Item | null>(null);

    const [formData, setFormData] = useState({
        name: '',
        description: '',
        isDrinkable: false,
        price: 0,
        effect: '{}'
    });

    useEffect(() => {
        fetchItems();
    }, [token]);

    const fetchItems = async () => {
        try {
            const response = await fetch(`${API_URL}/api/items`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (response.ok) {
                const data = await response.json();
                setItems(data);
            }
        } catch (err) {
            setError('Failed to fetch items!');
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        setSuccess(null);

        try {
            const response = await fetch(`${API_URL}/api/items${editingItem ? `/${editingItem.itemID}` : ''}`, {
                method: editingItem ? 'PUT' : 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(formData),
            });

            if (!response.ok) {
                throw new Error(`Failed to ${editingItem ? 'update' : 'create'} item`);
            }

            setSuccess(`Item ${editingItem ? 'updated' : 'created'} successfully!`);
            fetchItems();
            resetForm();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    const handleEdit = (item: Item) => {
        setEditingItem(item);
        setFormData({
            name: item.name,
            description: item.description,
            isDrinkable: item.isDrinkable,
            price: item.price,
            effect: item.effect
        });
    };

    const handleDelete = async (itemId: number) => {
        if (!window.confirm('Are you sure you want to delete this item?')) {
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/items/${itemId}`, {
                method: 'DELETE',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Failed to delete item');
            }

            setSuccess('Item deleted successfully!');
            fetchItems();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    const resetForm = () => {
        setFormData({
            name: '',
            description: '',
            isDrinkable: false,
            price: 0,
            effect: '{}'
        });
        setEditingItem(null);
    };

    return (
        <div className={styles.section}>
            <h2>Item Management</h2>

            {error && <div className={styles.error}>{error}</div>}
            {success && <div className={styles.success}>{success}</div>}

            <form onSubmit={handleSubmit} className={styles.form}>
                <div className={styles.formGroup}>
                    <label htmlFor="name">Name:</label>
                    <input
                        type="text"
                        id="name"
                        value={formData.name}
                        onChange={(e) => setFormData(prev => ({ ...prev, name: e.target.value }))}
                        required
                        className={styles.formInput}
                    />
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="description">Description:</label>
                    <textarea
                        id="description"
                        value={formData.description}
                        onChange={(e) => setFormData(prev => ({ ...prev, description: e.target.value }))}
                        required
                        className={styles.formTextarea}
                    />
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="price">Price:</label>
                    <input
                        type="number"
                        id="price"
                        value={formData.price}
                        onChange={(e) => setFormData(prev => ({ ...prev, price: parseInt(e.target.value) }))}
                        required
                        min="0"
                        className={styles.formInput}
                    />
                </div>

                <div className={styles.formGroup}>
                    <label>
                        <input
                            type="checkbox"
                            checked={formData.isDrinkable}
                            onChange={(e) => setFormData(prev => ({ ...prev, isDrinkable: e.target.checked }))}
                            className={styles.formCheckbox}
                        />
                        Is Drinkable
                    </label>
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="effect">Effect (JSON):</label>
                    <textarea
                        id="effect"
                        value={formData.effect}
                        onChange={(e) => setFormData(prev => ({ ...prev, effect: e.target.value }))}
                        className={styles.formTextarea}
                    />
                </div>

                <div className={styles.buttonGroup}>
                    <button type="submit" disabled={loading} className={styles.button}>
                        {loading ? 'Processing...' : (editingItem ? 'Update Item' : 'Create Item')}
                    </button>
                    {editingItem && (
                        <button
                            type="button"
                            onClick={resetForm}
                            className={`${styles.button} ${styles.secondaryButton}`}
                        >
                            Cancel Edit
                        </button>
                    )}
                </div>
            </form>

            <div className={styles.list}>
                <h3>Existing Items</h3>
                {items.map(item => (
                    <div key={item.itemID} className={styles.listItem}>
                        <div className={styles.itemInfo}>
                            <h4>{item.name}</h4>
                            <p>{item.description}</p>
                            <div className={styles.itemDetails}>
                                <span>Price: {item.price}</span>
                                <span>Drinkable: {item.isDrinkable ? 'Yes' : 'No'}</span>
                                <span>Effect: {item.effect}</span>
                            </div>
                        </div>
                        <div className={styles.buttonGroup}>
                            <button
                                onClick={() => handleEdit(item)}
                                className={`${styles.button} ${styles.editButton}`}
                            >
                                Edit
                            </button>
                            <button
                                onClick={() => handleDelete(item.itemID)}
                                className={`${styles.button} ${styles.deleteButton}`}
                            >
                                Delete
                            </button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ItemManager; 