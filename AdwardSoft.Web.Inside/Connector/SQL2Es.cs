using AdwardSoft.Web.Inside.Models;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Connector.Elastic
{
    public class SQL2Es
    {
        private readonly ElasticClient _client;

        public SQL2Es(ElasticClientProvider provider)
        {
            _client = provider.Client;
        }
        #region Initial
        public async Task<bool> Initial(List<FoodSearchViewModel> foods, List<FoodLocationSearchViewModel> locations)
        {
            _client.DeleteIndex(Indices.Index("food"));
            var indexSettings = _client.IndexExists("food");
            if (!indexSettings.Exists)
            {
                

                var response = _client.CreateIndex("food", c => c
                                .Mappings(m => m
                                    .Map<FoodSearch>(mm => mm
                                        .AutoMap()
                                        .Properties(p => p
                                        .GeoPoint(k => k
                                            .Name(n => n.Location)
                                            )
                                        )
                                    )
                                )
                            );

            }


            _client.DeleteIndex(Indices.Index("location"));
            var indexSettings2 = _client.IndexExists("location");
            if (!indexSettings2.Exists)
            {

                var response = _client.CreateIndex("location", c => c
                                .Mappings(m => m
                                    .Map<FoodLocationSearch>(mm => mm
                                        .AutoMap()
                                        .Properties(p => p
                                        .GeoPoint(k => k
                                            .Name(n => n.Location)
                                            )
                                        )
                                    )
                                )
                            );
            }

            var map = await CreateMappings(foods, locations);
            return map;
        }



        #region class
        public class FoodSearch
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal PriceOld { get; set; }
            public decimal Price { get; set; }          
            public string Image { get; set; }      
            public int FoodLocationId { get; set; }          
            public string Description { get; set; }      
            public string FoodLocationName { get; set; }
            public GeoLocation Location { get; set; }
            public string Tag { get; set; }
            public string Category { get; set; }
            public string CategoryLocation { get; set; }
        }


        public class FoodLocationSearch
        {
            public int Id { get; set; }
            public string Image { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
            public string Address { get; set; }
            public string Tel { get; set; }
            public string Note { get; set; }
            public GeoLocation Location { get; set; }
            public string Tag { get; set; }
            public string Category { get; set; }
        }
        #endregion

        public async Task<bool> CreateMappings(List<FoodSearchViewModel> foods, List<FoodLocationSearchViewModel> locations)
        {
            try
            {
                var foodsS = new List<FoodSearch>();
                foreach (var item in foods)
                {
                    foodsS.Add(new FoodSearch
                        {
                            Id = item.Id,
                            Description = item.Description,
                            FoodLocationId = item.FoodLocationId,
                            FoodLocationName = item.FoodLocationName,
                            Image = item.Image,
                            Name = item.Name,
                            Price = item.Price,
                            PriceOld = item.PriceOld,
                            Location = new GeoLocation(item.Latitude, item.Longitude),
                            Category = item.Category,
                            Tag = " " + item.Name.ToLower() + " " + convertToUnSign3(item.Name.ToLower()) + " ",
                            CategoryLocation = item.CategoryLocation
                        }
                    );
                }
                var resBulk1 = _client.Bulk(b => b
                            .CreateMany(foodsS)
                            .Index("food")
                            .Type("foodsearch")
                            .Refresh(Refresh.WaitFor));


                var locationsS = new List<FoodLocationSearch>();
                foreach (var item in locations)
                {
                    locationsS.Add(new FoodLocationSearch
                        {
                            Id = item.Id,
                            Address = item.Address,                    
                            Note = item.Note,
                            Image = item.Image,
                            Name = item.Name,
                            Tel = item.Tel,
                            Title = item.Title,
                            Location = new GeoLocation(item.Latitude, item.Longitude),
                            Category = item.Category,
                            Tag = " " + item.Name.ToLower() + " " + convertToUnSign3(item.Name.ToLower()) + " "
                        }
                    );
                }
                var resBulk2 = _client.Bulk(b => b
                            .CreateMany(locationsS)
                            .Index("location")
                            .Type("foodlocationsearch")
                            .Refresh(Refresh.WaitFor));

            }
            catch (Exception ex)
            {
                return false;
            }


            return true;
        }
        #endregion

        #region Index (Insert/Update)
        public async Task<IIndexResponse> Index(FoodSearchViewModel item)
        {
            var model = new FoodSearch
            {
                Id = item.Id,
                Description = item.Description,
                FoodLocationId = item.FoodLocationId,
                FoodLocationName = item.FoodLocationName,
                Image = item.Image,
                Name = item.Name,
                Price = item.Price,
                PriceOld = item.PriceOld,
                Location = new GeoLocation(item.Latitude, item.Longitude),
                Category = item.Category,
                Tag = " " + item.Name.ToLower() + " " + convertToUnSign3(item.Name.ToLower()) + " ",
                CategoryLocation = item.CategoryLocation
            };

            return await _client.IndexAsync<FoodSearch>(model, i => i
                                               .Index("food")
                                               .Type("foodsearch")
                                               .Id(model.Id)
                                               .Refresh(Elasticsearch.Net.Refresh.True));
            
        }



        public async Task<IBulkResponse> Update(List<FoodSearchViewModel> foods)
        {
            var model = new List<FoodSearch>();
            foreach (var item in foods)
            {
                model.Add(new FoodSearch
                {
                    Id = item.Id,
                    Description = item.Description,
                    FoodLocationId = item.FoodLocationId,
                    FoodLocationName = item.FoodLocationName,
                    Image = item.Image,
                    Name = item.Name,
                    Price = item.Price,
                    PriceOld = item.PriceOld,
                    Location = new GeoLocation(item.Latitude, item.Longitude),
                    Category = item.Category,
                    Tag = " " + item.Name.ToLower() + " " + convertToUnSign3(item.Name.ToLower()) + " ",
                    CategoryLocation = item.CategoryLocation
                }
                );
            }

            return _client.Bulk(b => b
                          .UpdateMany(model, (d, document) => d
                           .Upsert(document)
                           .Doc(document)
                           .Id(document.Id))
                          .Index("food")
                          .Type("foodsearch")
                          .Refresh(Refresh.WaitFor));
          
        }
        public async Task<IIndexResponse> Index(FoodLocationSearchViewModel item)
        {
            var model = new FoodLocationSearch
            {
                Id = item.Id,
                Address = item.Address,
                Note = item.Note,
                Image = item.Image,
                Name = item.Name,
                Tel = item.Tel,
                Title = item.Title,
                Location = new GeoLocation(item.Latitude, item.Longitude),
                Category = item.Category,
                Tag = " " + item.Name.ToLower() + " " + convertToUnSign3(item.Name.ToLower()) + " "
            };
            
            return await _client.IndexAsync<FoodLocationSearch>(model, i => i
                                               .Index("location")
                                               .Type("foodlocationsearch")
                                               .Id(model.Id)
                                               .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public async Task<IBulkResponse> Update(List<FoodLocationSearchViewModel> locations)
        {

            var model = new List<FoodLocationSearch>();
            foreach (var item in locations)
            {
                model.Add(new FoodLocationSearch
                {
                    Id = item.Id,
                    Address = item.Address,
                    Note = item.Note,
                    Image = item.Image,
                    Name = item.Name,
                    Tel = item.Tel,
                    Title = item.Title,
                    Location = new GeoLocation(item.Latitude, item.Longitude),
                    Category = item.Category,
                    Tag = " " + item.Name.ToLower() + " " + convertToUnSign3(item.Name.ToLower()) + " " 
                }
                );
            }

            return _client.Bulk(b => b
                           .UpdateMany(model, (d, document) => d
                            .Upsert(document)
                            .Doc(document)
                            .Id(document.Id))
                           .Index("location")
                           .Type("foodlocationsearch")
                           .Refresh(Refresh.WaitFor));
           
        }
        #endregion

        #region Delete
        public async Task<IDeleteResponse> Delete(FoodSearchViewModel model)
        {
            return await _client.DeleteAsync<FoodSearch>(model.Id, d => d
                                               .Index("food")
                                               .Type("foodsearch"));
        }
        public async Task<IDeleteResponse> Delete(FoodLocationSearchViewModel model)
        {
            return await _client.DeleteAsync<FoodLocationSearch>(model.Id, d => d
                                               .Index("location")
                                               .Type("foodlocationsearch"));
        }
        #endregion

        #region Count
        public async Task<long> Count()
        {
            var food = await _client.CountAsync<FoodSearchViewModel>(d => d
                                              .Index("food")
                                              .Type("foodsearch"));
            var location = await _client.CountAsync<FoodLocationSearchViewModel>(d => d
                                             .Index("location")
                                             .Type("foodlocationsearch"));
            return food.Count + location.Count;
        }

        #endregion


        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
