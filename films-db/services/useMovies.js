export default (axios) => {

     const listMovies = async (category, limit = 10) => {
          let params = {
               category,
               limit
          }

          if (!category) {
               delete params.category
          }

          const movies = await axios.$get(`/movies`, { params })
          return movies;
     }

     return {
          listMovies
     }
}