<template>
  <div class="mt-3">
    <movie-filter @filterChange="onFilter" :categories="categories"></movie-filter>
    <div class="row justify-content-center mt-3">
      <div v-for="(movie, index) in movies" :key="index" class="col-md-8 col-lg-4 col-sm-12 mt-3">
        <movie :movie="movie"></movie>
      </div>
    </div>
  </div>
</template>
<script>
import { ref, reactive, useContext, onMounted } from '@nuxtjs/composition-api'
import useCategories from '~/services/useCategories'
import useMovies from '~/services/useMovies'
export default {
  props: [],
  components: {},
  setup(props, { root: { $axios } }) {

    const { listCategories } = useCategories($axios)
    const { listMovies } = useMovies($axios)
    const categories = ref([])
    const filters = reactive({ selectedCategory: null })
    const movies = ref([])

    onMounted(async () => {
      categories.value = await listCategories();
      await setMovies();
    })

    const onFilter = async (filterData) => {
      filters.selectedCategory = filterData.selectedCategory
      await setMovies();
    }

    const setMovies = async () => {
      movies.value = await listMovies(filters.selectedCategory)
    }

    return {
      categories,
      onFilter,
      movies
    }
  },
}
</script>

<style lang="scss" scoped >
.row::after {
  content: "";
  flex: auto;
}
</style>