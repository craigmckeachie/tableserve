import { defineConfig } from "vite";
import handlebars from "vite-plugin-handlebars";
import { resolve } from "path";

export default defineConfig({
  plugins: [
    handlebars({
      partialDirectory: resolve(__dirname, "partials"),
    }),
  ],
  build: {
    rollupOptions: {
      input: {
        index: resolve(__dirname, "index.html"),
        signin: resolve(__dirname, "signin.html"),
        orders: resolve(__dirname, "orders.html"),
        orderCreate: resolve(__dirname, "order-create.html"),
        orderEdit: resolve(__dirname, "order-edit.html"),
        orderDetail: resolve(__dirname, "order-detail.html"),
        orderitemCreate: resolve(__dirname, "orderitem-create.html"),
        orderitemEdit: resolve(__dirname, "orderitem-edit.html"),
        menuitems: resolve(__dirname, "menuitems.html"),
        menuitemCreate: resolve(__dirname, "menuitem-create.html"),
        menuitemEdit: resolve(__dirname, "menuitem-edit.html"),
        categories: resolve(__dirname, "categories.html"),
        categoryCreate: resolve(__dirname, "category-create.html"),
        categoryEdit: resolve(__dirname, "category-edit.html"),
        staff: resolve(__dirname, "staff.html"),
        staffCreate: resolve(__dirname, "staff-create.html"),
        staffEdit: resolve(__dirname, "staff-edit.html"),
      },
    },
  },
});
