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
import ResourceVI from '../../../js/ResourceVI';
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
          menuItemText: ResourceVI.LeftMenuText["dashboard"],
        },
        {
          menuFunction: "cash",
          menuItemText: ResourceVI.LeftMenuText["cash"],
        },
        {
          menuFunction: "deposit",
          menuItemText: ResourceVI.LeftMenuText["deposit"],
        },
        {
          menuFunction: "buy",
          menuItemText: ResourceVI.LeftMenuText["buy"],
        },
        {
          menuFunction: "sell",
          menuItemText: ResourceVI.LeftMenuText["sell"],
        },
        {
          menuFunction: "invoice",
          menuItemText: ResourceVI.LeftMenuText["invoice"],
        },
        {
          menuFunction: "warehouse",
          menuItemText: ResourceVI.LeftMenuText["warehouse"],
        },
        {
          menuFunction: "tool",
          menuItemText: ResourceVI.LeftMenuText["tool"],
        },
        {
          menuFunction: "assets",
          menuItemText: ResourceVI.LeftMenuText["assets"],
        },
        {
          menuFunction: "tax",
          menuItemText: ResourceVI.LeftMenuText["tax"],
        },
        {
          menuFunction: "price",
          menuItemText: ResourceVI.LeftMenuText["price"],
        },
        {
          menuFunction: "sumary",
          menuItemText: ResourceVI.LeftMenuText["sumary"],
        },
        {
          menuFunction: "budget",
          menuItemText: ResourceVI.LeftMenuText["budget"],
        },
        {
          menuFunction: "report",
          menuItemText: ResourceVI.LeftMenuText["report"],
        },
        {
          menuFunction: "analysis",
          menuItemText: ResourceVI.LeftMenuText["analysis"],
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
