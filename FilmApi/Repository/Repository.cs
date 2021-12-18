using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilmApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FilmApi.Repository
{
     public abstract class Repository<T> : IRepository<T> where T : IModel, new()
     {
          protected List<T> _items;
          private readonly IConfiguration _config;
          private readonly ILogger _logger;
          public Repository(List<T> items, IConfiguration config, ILogger<T> logger)
          {
               _logger = logger;
               _config = config;
               _items = items;


               LoadFile();

          }

          public void Delete(int id)
          {
               _items.RemoveAll(i => i.Id == id);
               WriteChanges();

          }

          public virtual T Find(Func<T, bool> find)
          {
               T item = _items.FirstOrDefault(i => find(i));

               return item;
          }

          public virtual T Get(int id)
          {
               T item = _items.SingleOrDefault(item => item.Id == id);
               return item;
          }

          public virtual T Insert(T item)
          {
               int max = _items.Max(i => i.Id) ?? -1;
               item.Id = max + 1;
               _items.Add(item);

               WriteChanges();


               return item;
          }

          public virtual IEnumerable<T> List()
          {
               return _items;
          }

          public virtual void Update(int id, T item)
          {
               int index = _items.FindIndex(0, i => i.Id == id);

               if (index > -1)
               {
                    T existingItem = _items[index];
                    item.Id = existingItem.Id;
                    _items[index] = item;
               }

               WriteChanges();
          }

          protected void WriteChanges()
          {

               bool saveStateEnabled = _config.GetValue<bool>("enableJsonFileWriting");

               if (saveStateEnabled == false)
               {
                    _logger.LogWarning("Writing json file is disabled - check appsettings.json");
                    return;
               }

               string json = JsonConvert.SerializeObject(_items, Formatting.Indented,
                   new JsonSerializerSettings
                   {
                        TypeNameHandling = TypeNameHandling.All,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                   });

               string directoryName = _config.GetValue<string>("movieFileDirectoryName");

               if (string.IsNullOrWhiteSpace(directoryName))
               {
                    throw new Exception("check configurations - no json db file directory specified");
               }
               string path = Path.Combine(Environment.CurrentDirectory, directoryName);

               if (!Directory.Exists(path))
               {
                    Directory.CreateDirectory(path);
               }

               File.WriteAllText(path, json);
          }

          private void LoadFile()
          {
               if (_items.Count > 0)
               {
                    return;
               }


               bool saveStateEnabled = _config.GetValue<bool>("enableJsonFileWriting");

               if (saveStateEnabled == false)
               {
                    _logger.LogWarning("Writing json file is disabled - check appsettings.json");
                    return;
               }

               string directoryName = _config.GetValue<string>("movieFileDirectoryName");

               if (string.IsNullOrWhiteSpace(directoryName))
               {
                    throw new Exception("check configurations - no json db file directory specified");
               }


               string path = Path.Combine(Environment.CurrentDirectory, directoryName);

               string data = File.ReadAllText(path);

               _items = JsonConvert.DeserializeObject<List<T>>(data,
                                  new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All });

          }


     }
}