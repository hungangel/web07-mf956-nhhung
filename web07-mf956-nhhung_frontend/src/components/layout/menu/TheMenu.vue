<template>
  <div>
    <div class="menu-left left" :class="{ minimized: contentExpanded }">
      <div class="menu-left-header">
        <div class="icon-24 menu-toggle i-toggle"></div>
        <img
          class="logo i-logo"
          src="https://actappg1.misacdn.net/img/Logo_Module_TiengViet_White.66947422.svg"
        />
      </div>
      <div v-for="(menuItem, itemIndex) in menuItems" :key="itemIndex">
        <MenuItem
          :icon="'i-' + menuItem.menuFunction"
          :contentExpanded="contentExpanded"
          :menuItemFunction="menuItem.menuFunction"
          :routerPath="menuItem.routerPath ? menuItem.routerPath : 'notready'"
          :currentFunction="currentFunction"
          @clickMenuItem="onClickMenuItem"
          :menuItemText="menuItem.menuItemText"
        />
      </div>
    </div>
  </div>
</template>

<script>
import "../../../css/layout/menu.css";
import ResourceVI from "../../../scripts/resource";
import MenuItem from "./MenuItem.vue";
export default {
  name: "TheMenu",
  components: {
    MenuItem,
  },
  data() {
    return {
      contentExpanded: false,
      currentFunction: "dashboard",
      menuItems: [
        {
          menuFunction: "dashboard",
          menuItemText: ResourceVI.LeftMenuText["Dashboard"],
          routerPath:'/dict/employee'
        },
        {
          menuFunction: "cash",
          menuItemText: ResourceVI.LeftMenuText["Cash"],
        },
        {
          menuFunction: "deposit",
          menuItemText: ResourceVI.LeftMenuText["Deposit"],
        },
        {
          menuFunction: "buy",
          menuItemText: ResourceVI.LeftMenuText["Buy"],
        },
        {
          menuFunction: "sell",
          menuItemText: ResourceVI.LeftMenuText["Sell"],
        },
        {
          menuFunction: "invoice",
          menuItemText: ResourceVI.LeftMenuText["Invoice"],
        },
        {
          menuFunction: "warehouse",
          menuItemText: ResourceVI.LeftMenuText["Warehouse"],
        },
        {
          menuFunction: "tool",
          menuItemText: ResourceVI.LeftMenuText["Tool"],
        },
        {
          menuFunction: "assets",
          menuItemText: ResourceVI.LeftMenuText["Assets"],
        },
        {
          menuFunction: "tax",
          menuItemText: ResourceVI.LeftMenuText["Tax"],
        },
        {
          menuFunction: "price",
          menuItemText: ResourceVI.LeftMenuText["Price"],
        },
        {
          menuFunction: "sumary",
          menuItemText: ResourceVI.LeftMenuText["Sumary"],
        },
        {
          menuFunction: "budget",
          menuItemText: ResourceVI.LeftMenuText["Budget"],
        },
        {
          menuFunction: "report",
          menuItemText: ResourceVI.LeftMenuText["Report"],
        },
        {
          menuFunction: "analysis",
          menuItemText: ResourceVI.LeftMenuText["Analysis"],
        },
      ],
    };
  },
  methods: {
    toggleMenuLeft() {
      this.contentExpanded = !this.contentExpanded;
      this.$emit("toggleMenuLeft", this.contentExpanded);
    },
    onClickMenuItem(itemFunction) {
      this.currentFunction = itemFunction;
    },
  },
};
</script>
<style scoped>
@import "../../../css/layout/menu.css";
</style>
