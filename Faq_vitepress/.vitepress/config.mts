import { defineConfig } from "vitepress";

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "My Awesome FAQs",
  description: "A FAQs Site",
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: "Home", link: "/" },
      { text: "Examples", link: "/markdown-examples" },
    ],

    sidebar: [
      {
        text: "FAQs",
        items: [
          {
            text: ".Net",
            link: "/dotnet/index",
            items: [
              { text: "C#", link: "/dotnet/index" },
              { text: ".Net Core", link: "/dotnet/index" },
              { text: "EF Core", link: "/dotnet/index" },
            ],
          },
          {
            text: "JavaScript",
            link: "/dotnet/index",
            items: [
              { text: "JavaScript", link: "/js/js" },
              { text: "Vue.js", link: "/js/vue" },
              { text: "React.js", link: "/js/react" },
            ],
          },
          { text: "Python", link: "/dotnet/index" },
          { text: "Markdown Examples", link: "/markdown-examples" },
          { text: "Runtime API Examples", link: "/api-examples" },
        ],
      },
    ],

    socialLinks: [
      { icon: "github", link: "https://github.com/vuejs/vitepress" },
    ],
  },
});
