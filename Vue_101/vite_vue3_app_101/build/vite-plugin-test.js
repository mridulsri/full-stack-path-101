function testPlugin() {
  return {
    name: "test",
    enforce: "pre",
    config() {
      console.log("🚀 ~ file: vite.config.ts:9 ~ config ~ config:");
    },
    configResolved() {
      console.log(
        "🚀 ~ file: vite.config.ts:12 ~ configResolved ~ configResolved:"
      );
    },
    options() {
      console.log("🚀 ~ file: vite.config.ts:15 ~ options ~ options:");
    },
    configureServer() {
      console.log(
        "🚀 ~ file: vite.config.ts:20 ~ configureServer ~ configureServer:"
      );
    },
    buildStart() {
      console.log("🚀 ~ file: vite.config.ts:22 ~ buildStart ~ buildStart:");
    },
    transformIndexHtml() {
      console.log(
        "🚀 ~ file: vite.config.ts:26 ~ transformIndexHtml ~ transformIndexHtml:"
      );
    },
    resolveId() {
      console.log("🚀 ~ file: vite.config.ts:27 ~ resolveId ~ source:");
    },
    load() {
      console.log("🚀 ~ file: vite.config.ts:34 ~ load ~ load:");
    },
    transform() {
      console.log("🚀 ~ file: vite.config.ts:38 ~ transform ~ transform:");
    },
  };
}

// const config: UserConfig = {
//   plugins: [Vue(), testPlugin()],
//   build: {
//     sourcemap: true,
//   },
// };

// export default config;
