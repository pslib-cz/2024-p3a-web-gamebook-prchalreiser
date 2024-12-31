import { useState, useEffect } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import styles from './AdminPanel.module.css';

interface Location {
    locationID: number;
    name: string;
    description: string;
    backgroundImageUrl: string;
    hasRequiredItem: boolean;
    items: string;
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
        items: '[]'
    });

    const fetchLocations = async () => {
        try {
            const response = await fetch('https://localhost:7260/api/Locations', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (response.ok) {
                const data = await response.json();
                setLocations(data);
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
            const response = await fetch('https://localhost:7260/api/file/upload', {
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
                return `https://localhost:7260/Uploads/${fileInfo.id}${fileExtension}`;
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
                ? `https://localhost:7260/api/Locations/${editingLocation.locationID}`
                : 'https://localhost:7260/api/Locations';

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

            setSuccess(`Location ${editingLocation ? 'updated' : 'created'} successfully!`);
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
            items: location.items
        });
    };

    const resetForm = () => {
        setFormData({
            name: '',
            description: '',
            hasRequiredItem: false,
            items: '[]'
        });
        setFile(null);
        setEditingLocation(null);
    };

    const handleDelete = async (locationId: number) => {
        if (!window.confirm('Are you sure you want to delete this location? This will also delete all links to and from this location.')) {
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`https://localhost:7260/api/Locations/${locationId}`, {
                method: 'DELETE',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Failed to delete location');
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
                        required
                    />
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="description">Description:</label>
                    <textarea
                        id="description"
                        value={formData.description}
                        onChange={(e) => setFormData(prev => ({ ...prev, description: e.target.value }))}
                        required
                    />
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="image">Background Image:</label>
                    <input
                        type="file"
                        id="image"
                        accept="image/*"
                        onChange={(e) => setFile(e.target.files?.[0] || null)}
                        required={!editingLocation}
                    />
                </div>

                <div className={styles.formGroup}>
                    <label>
                        <input
                            type="checkbox"
                            checked={formData.hasRequiredItem}
                            onChange={(e) => setFormData(prev => ({ ...prev, hasRequiredItem: e.target.checked }))}
                        />
                        Has Required Item
                    </label>
                </div>

                <button type="submit" disabled={loading}>
                    {loading ? 'Processing...' : (editingLocation ? 'Update Location' : 'Create Location')}
                </button>
                {editingLocation && (
                    <button type="button" onClick={resetForm}>
                        Cancel Edit
                    </button>
                )}
            </form>

            <div className={styles.list}>
                <h3>Existing Locations</h3>
                {locations.map(location => (
                    <div key={location.locationID} className={styles.listItem}>
                        <div className={styles.locationHeader}>
                            <h4>{location.name}</h4>
                            <div className={styles.locationMeta}>
                                {location.hasRequiredItem && (
                                    <span className={styles.badge}>Requires Item</span>
                                )}
                            </div>
                        </div>
                        <div className={styles.locationContent}>
                            <div className={styles.locationInfo}>
                                <p>{location.description}</p>
                                <div className={styles.buttonGroup}>
                                    <button onClick={() => handleEdit(location)}>
                                        <span className={styles.buttonIcon}>✏️</span> Edit
                                    </button>
                                    <button
                                        onClick={() => handleDelete(location.locationID)}
                                        className={styles.deleteButton}
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