const DEFAULT_PROTOCOL = "https";
const DEFAULT_PORT = "443";
const DEFAULT_HOST = "localhost";

export const API_PROTOCOL =
    import.meta.env.VITE_API_PROTOCOL || DEFAULT_PROTOCOL;
export const API_PORT = import.meta.env.VITE_API_PORT || DEFAULT_PORT;
export const API_HOST = import.meta.env.VITE_API_HOST || DEFAULT_HOST;

// Allow a complete override of the API URL for Docker/container environments
export const API_URL =
    import.meta.env.VITE_API_URL ||
    `${API_PROTOCOL}://${API_HOST}${API_PORT === "443" ? "" : `:${API_PORT}`}`;
