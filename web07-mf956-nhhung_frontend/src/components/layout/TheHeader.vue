<template>
  <div class="page-header">
    <div class="header-left">
      <div class="content-toggle icon-24 i-three-bar"></div>
      <div class="store-selector">
        <Dropdown
          myurl=""
          itemId="StoreId"
          itemName="StoreName"
          originValue="0+''"
          defaultName="CÔNG TY CỔ PHẦN MISA"
        />
      </div>
    </div>
    <div class="header-right"></div>
    <div class="user">
      <div class="user-notification icon-24 i-bell"></div>
      <div class="reddot"></div>
      <div class="user-avatar"></div>
      <div class="user-fullname">MF956 Nguyễn Huy Hùng</div>
      <div class="user-function spr i-down-arrow"></div>
    </div>
  </div>
</template>

<script>
import Dropdown from "../base/BaseDropdown.vue";
export default {
  name: "TheHeader",
  components: {
    Dropdown,
  },
};
</script>
<style>
@import "../../css/layout/header.css";
</style>
