import { useState, useEffect } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import styles from './AdminPanel.module.css';
import { API_URL } from '../../config/env';

interface Minigame {
    minigameID: string;
    locationID: number;
    description: string;
    type: string;
    opponentName: string;
    winLocationID: number;
    loseLocationID: number;
    number1?: string;
    number2?: string;
}

interface Location {
    locationID: number;
    name: string;
    description: string;
    backgroundImageUrl: string;
    hasRequiredItem: boolean;
    hasShop: boolean;
    hasMinigame: boolean;
    items: string;
    minigame?: Minigame;
}

const LocationsManager = () => {
    const { token } = useAuth();
    const [file, setFile] = useState<File | null>(null);
    const [locations, setLocations] = useState<Location[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<string | null>(null);
    const [editingLocation, setEditingLocation] = useState<Location | null>(null);

    const [formData, setFormData] = useState({
        name: '',
        description: '',
        hasRequiredItem: false,
        hasShop: false,
        hasMinigame: false,
        items: '[]'
    });

    const [minigameData, setMinigameData] = useState({
        description: '',
        opponentName: '',
        winLocationID: 0,
        loseLocationID: 0,
        type: 'RPS',
        number1: '',
        number2: ''
    });

    const fetchLocations = async () => {
        try {
            const response = await fetch(`${API_URL}/api/Locations`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (response.ok) {
                const data = await response.json();
                const locationsWithMinigames = await Promise.all(
                    data.map(async (location: Location) => {
                        if (location.hasMinigame) {
                            const minigameResponse = await fetch(`${API_URL}/api/Minigames/${location.locationID}`, {
                                headers: {
                                    Authorization: `Bearer ${token}`
                                }
                            });
                            if (minigameResponse.ok) {
                                const minigameData = await minigameResponse.json();
                                return { ...location, minigame: minigameData };
                            }
                        }
                        return location;
                    })
                );
                setLocations(locationsWithMinigames);
            } else if (response.status === 403) {
                // Handle unauthorized access
                setError('Access denied: Admin privileges required');
            }
        } catch (err) {
            setError('Failed to fetch locations! ' + err);
        }
    };

    useEffect(() => {
        fetchLocations();
    }, [token]);

    const handleFileUpload = async () => {
        if (!file) return null;

        const formData = new FormData();
        formData.append('Files', file);

        try {
            const response = await fetch(`${API_URL}/api/file/upload`, {
                method: 'POST',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
                body: formData,
            });

            if (!response.ok) {
                throw new Error('Failed to upload file');
            }

            const data = await response.json();
            if (data && data.length > 0) {
                const fileInfo = data[0];
                const fileExtension = fileInfo.fileName.substring(fileInfo.fileName.lastIndexOf('.'));
                return `/Uploads/${fileInfo.id}${fileExtension}`;
            }
            throw new Error('Invalid response from server');
        } catch (error) {
            console.error('File upload error:', error);
            throw new Error('Failed to upload image');
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        setSuccess(null);

        try {
            const imageUrl = await handleFileUpload();
            if (!imageUrl) {
                throw new Error('Failed to upload image');
            }

            const locationData = {
                ...formData,
                backgroundImageUrl: imageUrl,
            };

            const url = editingLocation
                ? `${API_URL}/api/Locations/${editingLocation.locationID}`
                : `${API_URL}/api/Locations`;

            const response = await fetch(url, {
                method: editingLocation ? 'PUT' : 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(editingLocation ?
                    { ...locationData, locationID: editingLocation.locationID } :
                    locationData
                ),
            });

            if (!response.ok) {
                throw new Error(`Failed to ${editingLocation ? 'update' : 'create'} location`);
            }

            // Get the location data from the response
            const locationResult = editingLocation ?
                { locationID: editingLocation.locationID } :
                await response.json();

            if (formData.hasMinigame) {
                const minigameResponse = await fetch(
                    `${API_URL}/api/Minigames/${locationResult.locationID}`,
                    {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            Authorization: `Bearer ${token}`,
                        },
                        body: JSON.stringify({
                            ...minigameData,
                            locationID: locationResult.locationID,
                            type: minigameData.type,
                            number1: minigameData.number1,
                            number2: minigameData.number2
                        }),
                    }
                );

                if (!minigameResponse.ok) {
                    throw new Error('Failed to update minigame settings');
                }
            }

            setSuccess('Location and minigame settings updated successfully!');
            fetchLocations();
            resetForm();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    const handleEdit = (location: Location) => {
        setEditingLocation(location);
        setFormData({
            name: location.name,
            description: location.description,
            hasRequiredItem: location.hasRequiredItem,
            hasShop: location.hasShop,
            hasMinigame: location.hasMinigame,
            items: location.items
        });

        if (location.minigame) {
            setMinigameData({
                description: location.minigame.description || '',
                opponentName: location.minigame.opponentName || '',
                winLocationID: location.minigame.winLocationID || 0,
                loseLocationID: location.minigame.loseLocationID || 0,
                type: 'RPS',
                number1: location.minigame.number1 || '',
                number2: location.minigame.number2 || ''
            });
        } else {
            // Reset minigame data when editing a location without minigame
            setMinigameData({
                description: '',
                opponentName: '',
                winLocationID: 0,
                loseLocationID: 0,
                type: 'RPS',
                number1: '',
                number2: ''
            });
        }
    };

    const resetForm = () => {
        setFormData({
            name: '',
            description: '',
            hasRequiredItem: false,
            hasShop: false,
            hasMinigame: false,
            items: '[]'
        });
        setFile(null);
        setEditingLocation(null);
        setMinigameData({
            description: '',
            opponentName: '',
            winLocationID: 0,
            loseLocationID: 0,
            type: 'RPS',
            number1: '',
            number2: ''
        });
    };

    const handleDelete = async (locationId: number) => {
        if (!window.confirm('Are you sure you want to delete this location?')) {
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/Locations/${locationId}`, {
                method: 'DELETE',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Failed to delete location. Try to delete all links to and from this location first.');
            }

            setSuccess('Location deleted successfully!');
            fetchLocations();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.section}>
            <h2>Location Management</h2>

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
                        className={styles.formInput}
                        required
                    />
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="description">Description:</label>
                    <textarea
                        id="description"
                        value={formData.description}
                        onChange={(e) => setFormData(prev => ({ ...prev, description: e.target.value }))}
                        className={`${styles.formInput} ${styles.formTextarea}`}
                    />
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="image">Background Image:</label>
                    <input
                        type="file"
                        id="image"
                        accept="image/*"
                        onChange={(e) => setFile(e.target.files?.[0] || null)}
                        className={styles.fileInput}
                        required={!editingLocation}
                    />
                </div>

                <div className={styles.formGroup}>
                    <label>
                        <input
                            type="checkbox"
                            checked={formData.hasRequiredItem}
                            onChange={(e) => setFormData(prev => ({ ...prev, hasRequiredItem: e.target.checked }))}
                            className={styles.checkbox}
                        />
                        <label className={styles.checkboxLabel}>
                            Has Required Item
                        </label>
                    </label>
                </div>

                <div className={styles.formGroup}>
                    <label>
                        <input
                            type="checkbox"
                            checked={formData.hasShop}
                            onChange={(e) => setFormData(prev => ({ ...prev, hasShop: e.target.checked }))}
                            className={styles.checkbox}
                        />
                        <label className={styles.checkboxLabel}>
                            Has Shop
                        </label>
                    </label>
                </div>

                <div className={styles.formGroup}>
                    <label>
                        <input
                            type="checkbox"
                            checked={formData.hasMinigame}
                            onChange={(e) => setFormData(prev => ({ ...prev, hasMinigame: e.target.checked }))}
                            className={styles.checkbox}
                        />
                        <label className={styles.checkboxLabel}>
                            Has Minigame
                        </label>
                    </label>
                </div>

                {formData.hasMinigame && (
                    <div className={styles.minigameSettings}>
                        <h3>Minigame Settings</h3>

                        <div className={styles.formGroup}>
                            <label htmlFor="minigameType">Game Type:</label>
                            <select
                                id="minigameType"
                                value={minigameData.type}
                                onChange={(e) => setMinigameData(prev => ({
                                    ...prev,
                                    type: e.target.value
                                }))}
                                className={styles.formSelect}
                            >
                                <option value="RPS">Rock Paper Scissors</option>
                                <option value="NUMBERS">Number Guess</option>
                            </select>
                        </div>

                        <div className={styles.formGroup}>
                            <label htmlFor="minigameDescription">Game Description:</label>
                            <textarea
                                id="minigameDescription"
                                value={minigameData.description}
                                onChange={(e) => setMinigameData(prev => ({
                                    ...prev,
                                    description: e.target.value
                                }))}
                                className={`${styles.formInput} ${styles.formTextarea}`}
                            />
                        </div>

                        <div className={styles.formGroup}>
                            <label htmlFor="opponentName">Opponent Name:</label>
                            <input
                                type="text"
                                id="opponentName"
                                value={minigameData.opponentName}
                                onChange={(e) => setMinigameData(prev => ({
                                    ...prev,
                                    opponentName: e.target.value
                                }))}
                                className={styles.formInput}
                            />
                        </div>

                        <div className={styles.formGroup}>
                            <label htmlFor="winLocationID">Win Location ID:</label>
                            <input
                                type="number"
                                id="winLocationID"
                                value={minigameData.winLocationID}
                                onChange={(e) => setMinigameData(prev => ({
                                    ...prev,
                                    winLocationID: parseInt(e.target.value)
                                }))}
                                className={styles.formInput}
                            />
                        </div>

                        <div className={styles.formGroup}>
                            <label htmlFor="loseLocationID">Lose Location ID:</label>
                            <input
                                type="number"
                                id="loseLocationID"
                                value={minigameData.loseLocationID}
                                onChange={(e) => setMinigameData(prev => ({
                                    ...prev,
                                    loseLocationID: parseInt(e.target.value)
                                }))}
                                className={styles.formInput}
                            />
                        </div>

                        {minigameData.type === 'NUMBERS' && (
                            <>
                                <div className={styles.formGroup}>
                                    <label htmlFor="number1">First Number:</label>
                                    <input
                                        type="text"
                                        id="number1"
                                        value={minigameData.number1}
                                        onChange={(e) => setMinigameData(prev => ({
                                            ...prev,
                                            number1: e.target.value
                                        }))}
                                        className={styles.formInput}
                                    />
                                </div>

                                <div className={styles.formGroup}>
                                    <label htmlFor="number2">Second Number:</label>
                                    <input
                                        type="text"
                                        id="number2"
                                        value={minigameData.number2}
                                        onChange={(e) => setMinigameData(prev => ({
                                            ...prev,
                                            number2: e.target.value
                                        }))}
                                        className={styles.formInput}
                                    />
                                </div>
                            </>
                        )}
                    </div>
                )}

                <button
                    type="submit"
                    disabled={loading}
                    className={`${styles.button} ${styles.primaryButton} ${loading ? styles.buttonDisabled : ''}`}
                >
                    {loading ? 'Processing...' : (editingLocation ? 'Update Location' : 'Create Location')}
                </button>
                {editingLocation && (
                    <button
                        type="button"
                        onClick={resetForm}
                        className={`${styles.button} ${styles.secondaryButton}`}
                    >
                        Cancel Edit
                    </button>
                )}
            </form>

            <div className={styles.list}>
                <h3>Existing Locations</h3>
                {locations.map(location => (
                    <div key={location.locationID} className={styles.listItem}>
                        <div className={styles.locationHeader}>
                            <div className={styles.locationTitleGroup}>
                                <h4>{location.name}</h4>
                                <span className={styles.locationId}>ID: {location.locationID}</span>
                            </div>
                            <div className={styles.locationMeta}>
                                {location.hasRequiredItem && (
                                    <span className={styles.badge}>Requires Item</span>
                                )}
                                {location.hasShop && (
                                    <span className={styles.badge}>Has Shop</span>
                                )}
                                {location.hasMinigame && (
                                    <span className={styles.badge}>Has Minigame</span>
                                )}
                            </div>
                        </div>
                        <div className={styles.locationContent}>
                            <div className={styles.locationInfo}>
                                <p>{location.description}</p>
                                <div className={styles.buttonGroup}>
                                    <button
                                        onClick={() => handleEdit(location)}
                                        className={`${styles.button} ${styles.editButton}`}
                                    >
                                        <span className={styles.buttonIcon}>✏️</span> Edit
                                    </button>
                                    <button
                                        onClick={() => handleDelete(location.locationID)}
                                        className={`${styles.button} ${styles.deleteButton}`}
                                    >
                                        <span className={styles.buttonIcon}>🗑️</span> Delete
                                    </button>
                                </div>
                            </div>
                            <img
                                src={location.backgroundImageUrl}
                                alt={location.name}
                                className={styles.thumbnail}
                            />
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default LocationsManager; 