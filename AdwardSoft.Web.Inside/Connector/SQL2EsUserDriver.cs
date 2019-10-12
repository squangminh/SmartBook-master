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
    public class SQLEsUserDriver
    {
        private readonly ElasticClient _client;

        public SQLEsUserDriver(ElasticClientProvider provider)
        {
            _client = provider.Client;
        }
        #region Initial
        public async Task<bool> Initial(List<UserDriverSearchViewModel> userDrivers)
        {
            _client.DeleteIndex(Indices.Index("userdriver"));
            var indexDriver = _client.IndexExists("userdriver");
            if (!indexDriver.Exists)
            {


                var response = _client.CreateIndex("userdriver", c => c
                                .Mappings(m => m
                                    .Map<UserDriverSearch>(mm => mm
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


            var map = await CreateMappings(userDrivers);
            return map;
        }



        #region class
        public class UserDriverSearch
        {
            public long Id { get; set; }
            public string UserName { get; set; }
            public string PhoneNumber { get; set; }
            public string Avatar { get; set; }
            public string LicensePlates { get; set; }
            public bool isActive { get; set; }
            public GeoLocation Location { get; set; }
            public long ConnectUser { get; set; }
            public int DriverActivity { get; set; }
            public bool isConnect { get; set; }          
            public string FullName { get; set; }
            public short Type { get; set; }
        }
        #endregion

        public async Task<bool> CreateMappings(List<UserDriverSearchViewModel> userDrivers)
        {
            try
            {
                var userDriverSearches = new List<UserDriverSearch>();
                foreach (var item in userDrivers)
                {
                    userDriverSearches.Add(new UserDriverSearch
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                        PhoneNumber = item.PhoneNumber,
                        Avatar = item.Avatar,
                        LicensePlates = item.LicensePlates,
                        isActive = item.isActive,
                        Location = new GeoLocation(item.Latitude, item.Longitude),
                        ConnectUser = item.ConnectUser,
                        DriverActivity = item.DriverActivity,
                        isConnect = false,
                        FullName = item.FullName,
                        Type = item.Type
                    }
                    );
                }
                var resBulk1 = _client.Bulk(b => b
                            .CreateMany(userDriverSearches)
                            .Index("userdriver")
                            .Type("userdriversearch")
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
        public async Task<IIndexResponse> Index(UserDriverSearchViewModel item)
        {
            var model = new UserDriverSearch
            {
                Id = item.Id,
                UserName = item.UserName,
                PhoneNumber = item.PhoneNumber,
                Avatar = item.Avatar,
                LicensePlates = item.LicensePlates,
                isActive = item.isActive,
                Location = new GeoLocation(item.Latitude, item.Longitude),
                ConnectUser = item.ConnectUser,
                DriverActivity = item.DriverActivity,
                isConnect = false,
                FullName = item.FullName,
                Type = item.Type
            };

            return await _client.IndexAsync<UserDriverSearch>(model, i => i
                                               .Index("userdriver")
                                               .Type("userdriversearch")
                                               .Id(model.Id)
                                               .Refresh(Elasticsearch.Net.Refresh.True));

        }



        public async Task<IBulkResponse> Update(List<UserDriverSearchViewModel> userDrivers)
        {
            var model = new List<UserDriverSearch>();
            foreach (var item in userDrivers)
            {
                model.Add(new UserDriverSearch
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    PhoneNumber = item.PhoneNumber,
                    Avatar = item.Avatar,
                    LicensePlates = item.LicensePlates,
                    isActive = item.isActive,
                    Location = new GeoLocation(item.Latitude, item.Longitude),
                    ConnectUser = item.ConnectUser,
                    DriverActivity = item.DriverActivity,
                    isConnect = false,
                    FullName = item.FullName,
                    Type = item.Type
                }
                );
            }

            return _client.Bulk(b => b
                          .UpdateMany(model, (d, document) => d
                           .Upsert(document)
                           .Doc(document)
                           .Id(document.Id))
                          .Index("userdriver")
                          .Type("userdriversearch")
                          .Refresh(Refresh.WaitFor));

        }
        
        #endregion

        #region Delete
        public async Task<IDeleteResponse> Delete(UserDriverSearchViewModel model)
        {
            return await _client.DeleteAsync<UserDriverSearch>(model.Id, d => d
                                               .Index("userdriver")
                                               .Type("userdriversearch"));
        }
        
        #endregion

        #region Count
        public async Task<long> Count()
        {
            var userDrivers = await _client.CountAsync<UserDriverSearchViewModel>(d => d
                                              .Index("userdriver")
                                              .Type("userdriversearch"));
            return userDrivers.Count;
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

