# Movie db


## Backend (FilmApi)


    dotnet restore
  

**Configuration (appsettings.json)**
| Name | Description  | Default
|--|--|--|
| enableJsonFileWriting |set to true to save data to disk. **IMPORTANT**: for this to work you should set your folder permissions  | true
|movieFileDirectoryName |json data file will be saved in this folder | "movies-db"
 
  ## Frontend (films-db)
      
      cd films-db
      npm install && npm run dev
      
      open http://localhost:3000 in your favorite browser
