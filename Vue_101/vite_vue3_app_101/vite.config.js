import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import { fileURLToPath, URL } from "url";
import tenantResolverPlugin from "./build/vite-plugin-tenant.js";

console.log("starting application...!");
console.log("process.argv :>> ", process.argv);
const tenant =
  process.argv.find((arg) => arg.startsWith("--tenant="))?.split("=")[1] ||
  "demo";

console.log(tenant);

// plugin configuration
/*
const outputPluginStats = () => ({
  name: "output-plugin-stats",
  configResolved(config) {
    const plugins = config.plugins.map((plugin) => plugin.name);
    console.log(`Your project has ${plugins.length} Vite plugins.`);
    console.table(plugins);
  },
});

const requestAnalytics = () => ({
  name: "request-analytics",
  configureServer(server) {
    return () => {
      server.middlewares.use((req, res, next) => {
        console.log(`${req.method.toUpperCase()} ${req.url}`);
        next();
      });
    };
  },
});

const hotUpdateReport = () => ({
  name: "hot-update-report",
  handleHotUpdate({ file, timestamp, modules }) {
    console.log(`${timestamp}: ${modules.length} module(s) updated`);
  },
});
*/
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    tenantResolverPlugin(tenant),
    // outputPluginStats(),
    // requestAnalytics(),
    // hotUpdateReport(),
  ],
  resolve: {
    alias: [
      {
        find: "@",
        replacement: fileURLToPath(new URL("./src", import.meta.url)),
      },
    ],
    //extensions: [".js", ".vue"],
  },
  build: {
    sourcemap: true,
  },
});
