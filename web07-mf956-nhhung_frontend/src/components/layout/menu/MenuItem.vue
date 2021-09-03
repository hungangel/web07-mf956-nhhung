<template>
  <div
    class="menu-item"
    :class="[{ selected: currentFunction == menuItemFunction }]"
    @click="menuItemOnClick()"
  >
    <div
      class="menu-item-icon"
      :class="[icon, { selected: currentFunction == menuItemFunction }]"
    ></div>
    <div class="menu-item-text" :class="{ minimized: contentExpanded }">
      {{ menuItemText }}
    </div>
  </div>
</template>

<script>
export default {
  name: "MenuItem",
  props: {
    contentExpanded: Boolean,
    icon: String,
    menuItemText: String,
    menuItemFunction: String,
    currentFunction: String,
  },
  data() {
    return {};
  },
  methods: {
    menuItemOnClick() {
      this.$emit("clickMenuItem", this.menuItemFunction);
    },
  },
};
</script>
