import { ref, reactive, useContext, inject } from '@nuxtjs/composition-api'


export default (axios) => {
     const listCategories = async () => {
          const categories = await axios.$get('/categories')
          return categories;
     }


     return { listCategories }

}