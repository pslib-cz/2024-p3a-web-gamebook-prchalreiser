import { useState, useEffect } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import styles from './AdminPanel.module.css';
import { API_URL } from '../../config/env';

interface Link {
    linkID: number;
    fromLocationID: number;
    toLocationID: number;
    requiredItemId: number | null;
    condition: boolean | null;
    toLocation: {
        name: string;
    };
}

interface Location {
    locationID: number;
    name: string;
}

const LinksManager = () => {
    const { token } = useAuth();
    const [links, setLinks] = useState<Link[]>([]);
    const [locations, setLocations] = useState<Location[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<string | null>(null);
    const [editingLink, setEditingLink] = useState<Link | null>(null);

    const [formData, setFormData] = useState({
        fromLocationID: '',
        toLocationID: '',
        requiredItemId: null as number | null,
        condition: null as boolean | null
    });

    useEffect(() => {
        fetchLinks();
        fetchLocations();
    }, [token]);

    const fetchLinks = async () => {
        try {
            const response = await fetch(`${API_URL}/api/Links`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (response.ok) {
                const data = await response.json();
                setLinks(data);
            }
        } catch (err) {
            setError('Failed to fetch links! ' + err);
        }
    };

    const fetchLocations = async () => {
        try {
            const response = await fetch(`${API_URL}/api/Locations`, {
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

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        setSuccess(null);

        try {
            const linkData = {
                ...formData,
                fromLocationID: parseInt(formData.fromLocationID),
                toLocationID: parseInt(formData.toLocationID),
            };

            const response = await fetch(`${API_URL}/api/Links`, {
                method: editingLink ? 'PUT' : 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(editingLink ?
                    { ...linkData, linkID: editingLink.linkID } :
                    linkData
                ),
            });

            if (!response.ok) {
                throw new Error(`Failed to ${editingLink ? 'update' : 'create'} link`);
            }

            setSuccess(`Link ${editingLink ? 'updated' : 'created'} successfully!`);
            fetchLinks();
            resetForm();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    const handleEdit = (link: Link) => {
        setEditingLink(link);
        setFormData({
            fromLocationID: link.fromLocationID.toString(),
            toLocationID: link.toLocationID.toString(),
            requiredItemId: link.requiredItemId,
            condition: link.condition
        });
    };

    const resetForm = () => {
        setFormData({
            fromLocationID: '',
            toLocationID: '',
            requiredItemId: null,
            condition: null
        });
        setEditingLink(null);
    };

    const handleDelete = async (linkId: number) => {
        if (!window.confirm('Are you sure you want to delete this link?')) {
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_URL}/api/Links/${linkId}`, {
                method: 'DELETE',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Failed to delete link');
            }

            setSuccess('Link deleted successfully!');
            fetchLinks();
        } catch (err) {
            setError(err instanceof Error ? err.message : 'An error occurred');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.section}>
            <h2>Link Management</h2>

            {error && <div className={styles.error}>{error}</div>}
            {success && <div className={styles.success}>{success}</div>}

            <form onSubmit={handleSubmit} className={styles.form}>
                <div className={styles.formGroup}>
                    <label htmlFor="fromLocation">From Location:</label>
                    <select
                        id="fromLocation"
                        value={formData.fromLocationID}
                        onChange={(e) => setFormData(prev => ({ ...prev, fromLocationID: e.target.value }))}
                        required
                        className={styles.formSelect}
                    >
                        <option value="">Select location</option>
                        {locations.map(location => (
                            <option key={location.locationID} value={location.locationID}>
                                {location.name}
                            </option>
                        ))}
                    </select>
                </div>

                <div className={styles.formGroup}>
                    <label htmlFor="toLocation">To Location:</label>
                    <select
                        id="toLocation"
                        value={formData.toLocationID}
                        onChange={(e) => setFormData(prev => ({ ...prev, toLocationID: e.target.value }))}
                        required
                        className={styles.formSelect}
                    >
                        <option value="">Select location</option>
                        {locations.map(location => (
                            <option key={location.locationID} value={location.locationID}>
                                {location.name}
                            </option>
                        ))}
                    </select>
                </div>

                <button type="submit" disabled={loading} className={`${styles.button} ${styles.primaryButton} ${loading ? styles.buttonDisabled : ''}`}>
                    {loading ? 'Processing...' : (editingLink ? 'Update Link' : 'Create Link')}
                </button>
                {editingLink && (
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
                <h3>Existing Links</h3>
                {links.map(link => (
                    <div key={link.linkID} className={styles.listItem}>
                        <div className={styles.linkContent}>
                            <div className={styles.linkPath}>
                                <span className={styles.locationName}>
                                    {locations.find(l => l.locationID === link.fromLocationID)?.name}
                                </span>
                                <span className={styles.linkArrow}>→</span>
                                <span className={styles.locationName}>
                                    {link.toLocation.name}
                                </span>
                            </div>
                            {link.requiredItemId && (
                                <span className={styles.badge}>Required Item: {link.requiredItemId}</span>
                            )}
                        </div>
                        <div className={styles.buttonGroup}>
                            <button
                                onClick={() => handleEdit(link)}
                                className={`${styles.button} ${styles.editButton}`}
                            >
                                <span className={styles.buttonIcon}>✏️</span> Edit
                            </button>
                            <button
                                onClick={() => handleDelete(link.linkID)}
                                className={`${styles.button} ${styles.deleteButton}`}
                            >
                                <span className={styles.buttonIcon}>🗑️</span> Delete
                            </button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default LinksManager; 