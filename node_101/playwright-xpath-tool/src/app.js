const { chromium } = require("playwright");

(async () => {
  const browser = await chromium.launch({ headless: false, devtools: true });
  const context = await browser.newContext();
  const page = await context.newPage();

  await page.goto("https://google.com");

  // Inject CSS for hover styles
  await page.addStyleTag({
    content: `
            .playwright-hover {
                outline: 2px solid red !important;
                background-color: rgba(255, 0, 0, 0.1) !important;
            }
        `,
  });

  // Listen for dialogs (alerts)
  page.on("dialog", async (dialog) => {
    console.log(`Dialog message: ${dialog.message()}`);
    await dialog.accept(); // Close the alert
  });

  // Inject JavaScript to handle hover and right-click events
  await page.evaluate(() => {
    // Function to generate XPath for an element
    function getXPath(element) {
      if (element.id !== "") {
        return `//*[@id='${element.id}']`;
      }
      if (element === document.body) {
        return element.tagName.toLowerCase();
      }

      let ix = 0;
      const siblings = element.parentNode.childNodes;
      for (let i = 0; i < siblings.length; i++) {
        const sibling = siblings[i];
        if (sibling === element) {
          return `${getXPath(
            element.parentNode
          )}/${element.tagName.toLowerCase()}[${ix + 1}]`;
        }
        if (sibling.nodeType === 1 && sibling.tagName === element.tagName) {
          ix++;
        }
      }
    }

    // Add hover effect
    document.addEventListener("mouseover", (event) => {
      const element = event.target;
      element.classList.add("playwright-hover");
    });

    // Remove hover effect
    document.addEventListener("mouseout", (event) => {
      const element = event.target;
      element.classList.remove("playwright-hover");
    });

    document.addEventListener("contextmenu", (event) => {
      event.preventDefault();
      const element = event.target;
      const xpath = getXPath(element);
      console.log(`XPath: ${xpath}`);
      alert(`XPath: ${xpath}`);
    });
  });

  console.log("Right-click on any element to get its XPath.");
})();
