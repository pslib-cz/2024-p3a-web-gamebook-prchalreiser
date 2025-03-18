import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "vite";
import plugin from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ""
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "mdagamebook.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (
        0 !==
        child_process.spawnSync(
            "dotnet",
            [
                "dev-certs",
                "https",
                "--export-path",
                certFilePath,
                "--format",
                "Pem",
                "--no-password",
            ],
            { stdio: "inherit" }
        ).status
    ) {
        throw new Error("Could not create certificate.");
    }
}

const target =
    env.REACT_APP_API_URL || env.ASPNETCORE_HTTPS_PORT
        ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
        : env.ASPNETCORE_URLS
        ? env.ASPNETCORE_URLS.split(";")[0]
        : "https://localhost:7260";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            "@": fileURLToPath(new URL("./src", import.meta.url)),
        },
    },
    server: {
        proxy: {
            "/api": {
                target,
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/api/, "/api"),
                configure: (proxy, _options) => {
                    proxy.on("error", (err, _req, _res) => {
                        console.log("proxy error", err);
                    });
                    proxy.on("proxyReq", (proxyReq, req, _res) => {
                        console.log(
                            "Sending Request to the Target: " + proxyReq.method,
                            req.method,
                            req.url
                        );
                    });
                    proxy.on("proxyRes", (proxyRes, req, _res) => {
                        console.log(
                            "Received Response from the Target:",
                            proxyRes.statusCode,
                            req.url
                        );
                    });
                },
            },
            "/swagger": {
                target,
                changeOrigin: true,
                secure: false,
            },
            "/Uploads": {
                target,
                changeOrigin: true,
                secure: false,
            },
        },
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        },
        host: true,
    },
});
