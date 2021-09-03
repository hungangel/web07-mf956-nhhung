<template>
  <button
    :class="['button', subClass, iconName]"
    @click="$emit('btnClick')"
  >
    {{ buttonText }}
  </button>
</template>
<script>
export default {
  name: "ButtonIcon",
  props: {
    iconName: String,
    buttonText: String,
    subClass: String,
  },
};
</script>
<style scoped>
@import '../../css/base/button.css';
</style>