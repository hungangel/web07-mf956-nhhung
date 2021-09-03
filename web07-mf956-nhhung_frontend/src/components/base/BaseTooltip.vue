<template>
  <!-- <div class="tooltip-box flex jc-center"> -->
    <div class="tooltiptext">
      <div>{{ errorMessage }}</div>
    </div>
  <!-- </div> -->
</template>
<script>
export default {
  name: "Tooltip",
  props: {
    errorMessage: String,
  },
};
</script>
<style scoped>
@import '../../css/base/tooltip.css'
</style>
