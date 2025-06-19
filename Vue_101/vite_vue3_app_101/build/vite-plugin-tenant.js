import { promises as fs } from "fs";
import { dirname, resolve, parse } from "pathe";

// import { createUnplugin } from "unplugin";

const extensionList = [".vue"];

export default function tenantResolverPlugin(tenant) {
  return {
    name: "vite-plugin-tenant-resolver",
    enforce: "pre",
    async resolveId(id, importer) {
      if (importer === undefined) return;
      // Check if the id matches the pattern for components
      const match = id.match(/^(.+)\.vue$/);
      if (match) {
        //const baseComponentPath = match[1];

        const standardComponentPath = resolve(dirname(importer), id);
        const standardComponent = parse(standardComponentPath);
        // Construct tenant-specific component path
        const fileExtention = standardComponent.ext || ".vue";
        const tenantComponent = `${standardComponent.name}.${tenant}${fileExtention}`;
        const tenantComponentPath = resolve(
          standardComponent.dir,
          tenantComponent
        );

        try {
          // Check if the tenant-specific component exists
          await fs.access(tenantComponentPath);
          console.log(
            `Resolved [${tenant}] tenant-specific  component: [${tenantComponent}]`
          );
          return tenantComponentPath;
        } catch (err) {
          try {
            // Fallback to the standard component
            await fs.access(standardComponentPath);
            console.log(`Resolved standard component: [${id}]`);
            return standardComponentPath; //standardComponentPath;
          } catch (err) {
            // If neither the tenant-specific nor the standard component exists, return null
            console.error(`Component not found: ${id}`);
            return null;
          }
        }
      }
      return null;
    },
  };
}
