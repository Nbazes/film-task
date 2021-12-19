<template>
     <div class="card">
          <div class="poster__header" :style="headerStyle">
               <div class="poster__header__inner">
                    <h1>{{movie.title}}</h1>
               </div>
          </div>
          <div class="card-body d-flex flex-column align-items-center">
               <client-only>
                    <star-rating :rating="movie.rating" :star-size="20" />
               </client-only>
              <p class="lead mt-3"> {{movie.category}}</p>
          </div>
     </div>
</template>
<script>
import { ref, reactive, useContext, computed } from '@nuxtjs/composition-api'

export default {
     props: ['movie'],
     components: {   },
     setup(props) {

          const headerStyle = computed(() => {
               return {
                    'background-image': `url(${props.movie.backgroundUrl})`
               }
          })
          return { headerStyle }
     },
}
</script>


<style lang="scss" scoped>
.poster__header {
     height: 300px;
     background-size: cover;
     background-repeat: no-repeat;
     background-position: top center;
     // background: url("https://www.google.com/url?sa=i&url=http%3A%2F%2Ft3.gstatic.com%2Fimages%3Fq%3Dtbn%3AANd9GcQzBPeJBL1nrbE44py9eA0PFWzRQjQlW4NwjIBKuOMjVi4ou8UR&psig=AOvVaw0pwq-yrVtZm3tTlrtosUSB&ust=1639988201198000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCMjum6O27_QCFQAAAAAdAAAAABAD");
     position: relative;
}
.poster__header__inner {
     display: flex;
     color: #fff;
     flex-direction: column;
     width: 100%;
     height: 100%;
     text-align: center;
     justify-content: center;
     backdrop-filter: grayscale(10%)
}

h1 {
     font-weight: 700;
     text-transform: uppercase;
}
</style>