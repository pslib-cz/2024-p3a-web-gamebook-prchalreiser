import { useState, useEffect } from "react";
import "./Upload.module.css";
import { API_URL } from '../config/env';

const FileUpload = () => {
  const [file, setFile] = useState<File | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<Error | null>(null);
  const [success, setSuccess] = useState(false);
  const handleUpload = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!file) return;

    const token = localStorage.getItem("accessToken");
    alert(token);

    setLoading(true);
    setError(null);
    setSuccess(false);

    const formData = new FormData();
    formData.append("Files", file);

    try {
      if (file.size > 16 * 1024 * 1024) {
        throw new Error("Soubor je příliš velký. Maximální velikost je 16 MB.");
      }

      const response = await fetch(`${API_URL}/api/file/upload`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
        },
        body: formData,
        mode: "cors",
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Nahrávání selhalo");
      } else {
        console.log(response.json());
        console.log(response);
      }

      setSuccess(true);
      setFile(null);
      (event.target as HTMLFormElement).reset();
    } catch (err) {
      setError(err instanceof Error ? err : new Error("Nahrávání selhalo"));
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("accessToken");
    if (!token) {
      console.log("Nejste přihlášen");
    } else {
      fetch(`${API_URL}/api/user/manage/info`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
        mode: "cors",
      })
        .then((res) => res.json())
        .then((data) => console.log(data));
    }
  }, []);

  return (
    <div className="upload-container">
      <h1>Nahrát soubor</h1>

      {error && <div className="error-message">{error.message}</div>}
      {success && (
        <div className="success-message">Soubor byl úspěšně nahrán!</div>
      )}

      <form onSubmit={handleUpload} className="upload-form">
        <div className="file-input-container">
          <input
            type="file"
            accept=".jpg, .jpeg, .png, .gif, .webp, .avif"
            onChange={(e) => setFile(e.target.files?.[0] || null)}
            disabled={loading}
            required
            multiple
          />
        </div>

        <button
          type="submit"
          disabled={loading || !file}
          className="upload-button"
        >
          {loading ? "Nahrávání..." : "Nahrát"}
        </button>
      </form>
    </div>
  );
};

export default FileUpload;
