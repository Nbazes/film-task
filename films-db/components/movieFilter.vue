<template>
     <div class="filter__outer">
          <div class="form-inline form-group">
               <label for="sel1">Categories:</label>
               <select
                    class="form-control"
                    id="sel1"
                    @change="onCategoryChange"
                    v-model="filterData.selectedCategory"
               >
                    <option></option>
                    <option v-for="(category, index) in categories" :key="index">{{ category }}</option>
               </select>
          </div>
     </div>
</template>
<script>
import { ref, reactive, useContext } from '@nuxtjs/composition-api'
export default {
     props: ["categories"],
     components: {},
     setup(props, { emit }) {
          const filterData = reactive({
               selectedCategory: null
          })


          const onCategoryChange = () => {
               emit('filterChange', filterData)
          }
          return { filterData, onCategoryChange }
     },
}
</script>

<style lang="scss" scoped>
@media (max-width: 767.98px) {
     // .filter__outer {
     //      height: auto !important;
     //      padding: 10px 10px !important;
     // }
     select.form-control {
          margin-left: 0 !important;
     }
}

.filter__outer {
     background: #f2f2f2;
     border-radius: 10px;
     width: 100%;
     display: flex;
     padding: 10px 10px;
     flex-direction: column;
     justify-content: center;
}
select.form-control {
     min-width: 120px;
     margin-left: 10px;
     margin-bottom: 0;
}
.form-inline.form-group {
     margin-bottom: 0;
}
</style>