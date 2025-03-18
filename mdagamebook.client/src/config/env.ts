const DEFAULT_PROTOCOL = "https";
const DEFAULT_PORT = "443";
const DEFAULT_HOST = "localhost";

export const API_PROTOCOL =
    import.meta.env.VITE_API_PROTOCOL || DEFAULT_PROTOCOL;
export const API_PORT = import.meta.env.VITE_API_PORT || DEFAULT_PORT;
export const API_HOST = import.meta.env.VITE_API_HOST || DEFAULT_HOST;

// Allow a complete override of the API URL for Docker/container environments
export const API_URL = "";

// If you want to specify an absolute URL (not recommended for development)
// export const API_URL = 'https://localhost:7260';
