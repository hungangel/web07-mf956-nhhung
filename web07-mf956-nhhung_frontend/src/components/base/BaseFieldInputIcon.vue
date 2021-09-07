<template>
  <div class="inputbox">
    <div class="field-input-icon">
      <input
        type="text"
        :class="[subClass]"
        :id="id"
        :placeholder="placeHolder"
        :required="required == 'true' ? true : false"
        @input="onInput($event.target.value)"
        v-model="modelData"
      />
      <div class="input-icon icon-16" :class="[iconName]"></div>
    </div>
  </div>
</template>
<script>
import _ from "lodash";

export default {
  name: "FieldInputIcon",
  data() {
    return {
      modelData: "",
      isEmpty: true,
    };
  },
  props: {
    subClass: String,
    iconName: String,
    id: String,
    value: String,
    fieldName: String,
    tabIndex: String,
    originValue: String,
    dataType: String,
    placeHolder: String,
    required: String,
  },
  methods: {
    /**
     * Khi input, emit giá trị trong ô nhập sau 1s
     * CreatedBy: NHHung(29/08)
     */
    onInput: _.debounce(function(value) {
      this.$emit("input", value);
      if (this.modelData.length > 0) this.isEmpty = false;
    }, 1500),
  },
  watch: {
    originValue: function() {
      this.modelData = this.originValue;
    },
  },
};
</script>
