<template>
  <button
    :class="['button', subClass, iconName]"
    @click="$emit('btnClick')"
    :title="title"
  >
    {{ buttonText }}
  </button>
</template>
<script>
export default {
  name: "ButtonIcon",
  props: {
    title:String,
    iconName: String,
    buttonText: String,
    subClass: String,
  },
};
</script>
<style scoped>
@import '../../css/base/button.css';
</style>