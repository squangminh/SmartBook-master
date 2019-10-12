//using AdwardSoft.API.Mobile.Model;
//using AdwardSoft.Core.Pattern;
//using AdwardSoft.DTO.Presentation.CMS;
//using AdwardSoft.Utilities.Helper;
//using AdwardSoft.Web.Inside.Connector.Elastic;
//using Elasticsearch.Net;
//using Microsoft.AspNetCore.SignalR;
//using Nest;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AdwardSoft.API.Mobile.Connector.SingalR
//{
//    public class UserDriverHub : Hub
//    {
//        private readonly IUserConnectionManager _userConnectionManager;
//        private IGenericRepository _generic;
//        private SQL2Es _SQL2Es;
//        private readonly ElasticClient _client;

//        public UserDriverHub(SQL2Es SQL2Es, IUserConnectionManager userConnectionManager
//            , IGenericRepository generic, ElasticClientProvider provider)
//        {
//            _userConnectionManager = userConnectionManager;
//            _generic = generic;
//            _SQL2Es = SQL2Es;
//            _client = provider.Client;
//        }

//        public async override Task OnConnectedAsync()
//        {
//            var httpContext = this.Context.GetHttpContext();

//            var userId = httpContext.Request.Query["userId"];
//            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);
//            //var value = await Task.FromResult(0);
//            await Clients.Client(Context.ConnectionId).SendAsync("confirmConnect", "You have connected");
//            //return "You have connected";
//            var driver = _client.Search<Model.UserDriverSearch>(s => s
//                .Index("userdriver")
//                .Type("userdriversearch")
//                .Query(q => q
//                    .Term(f => f.Id, Int64.Parse(userId))
//                   )
//                 );
//            if (driver.Documents.Count > 0)
//            {
//                var driverInfor = driver.Documents.FirstOrDefault();

//                driverInfor.isConnect = true;

//                await _client.IndexAsync<Model.UserDriverSearch>(driverInfor, i => i
//                                           .Index("userdriver")
//                                           .Type("userdriversearch")
//                                           .Id(driverInfor.Id)
//                                           .Refresh(Elasticsearch.Net.Refresh.True));
//            }
            
//        }

//        public async override Task OnDisconnectedAsync(Exception exception)
//        {
//            //get the connectionId
//            var connectionId = Context.ConnectionId;
//            var userId = _userConnectionManager.GetUserId(connectionId);

//            _userConnectionManager.RemoveUserConnection(connectionId);

//            //Count connect User
//            var connectionsUser = _userConnectionManager.GetUserConnections(userId);
//            //var value = await Task.FromResult(0);

//            if (connectionsUser.Count == 0)
//            {
//                var driver = _client.Search<Model.UserDriverSearch>(s => s
//                .Index("userdriver")
//                .Type("userdriversearch")
//                .Query(q => q
//                    .Term(f => f.Id, Int64.Parse(userId))
//                   )
//                 );
//                if (driver.Documents.Count > 0)
//                {
//                    var driverInfor = driver.Documents.FirstOrDefault();

//                    driverInfor.isConnect = false;

//                    await _client.IndexAsync<Model.UserDriverSearch>(driverInfor, i => i
//                                               .Index("userdriver")
//                                               .Type("userdriversearch")
//                                               .Id(driverInfor.Id)
//                                               .Refresh(Elasticsearch.Net.Refresh.True));
//                }
//            }
                     

//        }

//        public async Task UpdateLocation(double latitude, double longitude)
//        {
//            var userId = _userConnectionManager.GetUserId(Context.ConnectionId);
//            var driver = _client.Search<Model.UserDriverSearch>(s => s
//                .Index("userdriver")
//                .Type("userdriversearch")
//                .Query(q => q
//                    .Term(f => f.Id, Int64.Parse(userId))
//                   )
//                 );

//            var driverInfor = driver.Documents.FirstOrDefault();

//            driverInfor.Location = new GeoLocation(latitude, longitude);

//            await _client.IndexAsync<Model.UserDriverSearch>(driverInfor, i => i
//                                       .Index("userdriver")
//                                       .Type("userdriversearch")
//                                       .Id(driverInfor.Id)
//                                       .Refresh(Elasticsearch.Net.Refresh.True));
//        }
      
//        // order
//        public async Task CustomerOrder(string order, string customerInfor, int RestaurantId, string orderFood, string orderSend)
//        {
//            var resScuccess = new Response();
//            var resError = new ResponseError();

//            bool flag = true;

//            //parse json to object
//            var Order = JsonConvert.DeserializeObject<Order>(order);
//            Order.Note = (Order.Note == null ? " " : Order.Note);

//            var OrderFood = JsonConvert.DeserializeObject<List<OrderFood>>(orderFood);
//            var OrderSend = JsonConvert.DeserializeObject<OrderSend>(orderSend);
//            var CustomerInfor = JsonConvert.DeserializeObject<CustomerInfor>(customerInfor);

//            var Restaurant = new DTO.Presentation.Mobile.Location();

//            Order.Date = DateTime.Now;

//            Order.IsCompleted = false;
//            Order.Status = (int)EOrderStatus.Pending;

//            if (Order.OrderType == (int)EOrderType.Food)
//            {

//                var param = DataHelper.GenParams("Id", RestaurantId);

//                var result = await _generic.ReadByCustomAsync<DTO.Presentation.Mobile.Location>("GetDetail", param);

//                Restaurant = result.Response;              
//                Order.Latitude2 = (float)result.Response.Latitude;
//                Order.Longitude2 = (float)result.Response.Longitude;

//            }

//            var type = (Order.OrderType == 2 ? 4 : 3);

//            //Tìm driver
//            var responsedata = _client.Search<Model.UserDriverSearch>(s => s
//                               .Index("userdriver")
//                               .Type("userdriversearch")
//                               .Query(q =>
//                                    q.Term(p => p.DriverActivity, 0)
//                                    &&
//                                    q.Term(p => p.Type, type)
//                                    &&
//                                    q.Term(p => p.isActive, true)
//                                    &&
//                                    q.Term(p => p.isConnect, true)
//                                    &&
//                                    !q.Term(p => p.ConnectUser, Order.CustomerId)
//                                )
//                                 .Sort(sort => sort
//                                  .GeoDistance(g => g
//                                    .Field(f => f.Location)
//                                    .Order(Nest.SortOrder.Ascending)
//                                    .Points(new GeoLocation(Order.Latitude2, Order.Longitude2))
//                                    .DistanceType(GeoDistanceType.Arc)))
//                                    .Size(1)
//                                );

//            if (responsedata.Documents.Count > 0)
//            {
//                var userIdDriver = responsedata.Documents.FirstOrDefault().Id;
//                //Update status Driver              

//                var driverInfor = responsedata.Documents.FirstOrDefault();

//                driverInfor.DriverActivity = (int)EDriverAcivity.Pending;
//                driverInfor.ConnectUser = Order.CustomerId;

//                //Cập nhật DriverId
//                Order.DriverId = driverInfor.Id;

//                await _client.IndexAsync<Model.UserDriverSearch>(driverInfor, i => i
//                                           .Index("userdriver")
//                                           .Type("userdriversearch")
//                                           .Id(driverInfor.Id)
//                                           .Refresh(Elasticsearch.Net.Refresh.True));

//                //Tạo order nếu là lần đầu
//                if (Order.Id != "")
//                {
//                    var resultOrder = await _generic.CreateAsync<Order, string>(Order);
//                    Order.Id = resultOrder.Response;
//                    if (Order.OrderType == (int)EOrderType.Food)
//                    {

//                        //Tạo order food
//                        if (resultOrder.Success)
//                        {                          
//                            OrderFood.ForEach(c => c.OrderId = resultOrder.Response);
//                            var resultListFood = await _generic.CreateMultipleAsync<OrderFood>(OrderFood);
//                            if (!resultListFood.Success)
//                            {
//                                Order.Status = (int)EOrderStatus.ErorrSystem;
//                                var result = await _generic.UpdateAsync<Order>(Order);
//                                flag = false;
//                            }
//                        }
//                        else
//                        {                            
//                            flag = false;
//                        }
//                    }
//                    else if (Order.OrderType == (int)EOrderType.Ship)
//                    {

//                        //Tạo order send
//                        if (resultOrder.Success)
//                        {                           
//                            OrderSend.OrderId = resultOrder.Response;
//                            var resultListFood = await _generic.CreateAsync<OrderSend>(OrderSend);
//                            if (!resultListFood.Success)
//                            {
//                                Order.Status = (int)EOrderStatus.ErorrSystem;
//                                var result = await _generic.UpdateAsync<Order>(Order);
//                                flag = false;
//                            }
//                        }
//                        else
//                        {                         
//                            flag = false;

//                        }
//                    }else if (Order.OrderType == (int)EOrderType.Driver)
//                    {

//                    }
//                }
//                else //Cập nhật lại driver nếu là lần 2
//                {
//                    var result = await _generic.UpdateAsync<Order>(Order);
//                }
                

//                //Gửi thông báo cho driver nếu tạo order thành công hoặc gửi thông báo cho khách nếu tạo driver thấy bại
//                if (!flag)
//                {
//                    driverInfor.DriverActivity = (int)EDriverAcivity.None;
//                    driverInfor.ConnectUser = 0;

//                    await _client.IndexAsync<API.Mobile.Model.UserDriverSearch>(driverInfor, i => i
//                                               .Index("userdriver")
//                                               .Type("userdriversearch")
//                                               .Id(driverInfor.Id)
//                                               .Refresh(Elasticsearch.Net.Refresh.True));

//                    resError.status = "Có lỗi hệ thống xảy ra , xin vui lòng thử lại!";
//                    var jsonStr = JsonConvert.SerializeObject(resError);

//                    var connectionsCustomer = _userConnectionManager.GetUserConnections(Order.CustomerId.ToString());
//                    foreach (var connectionId in connectionsCustomer)
//                    {
//                        await Clients.Client(connectionId).SendAsync("orderAccept", jsonStr);
//                    }
//                }
//                else
//                {
                   

//                    //push message
//                    var json = new
//                    {                                      
//                        order = Order,
//                        orderFood = OrderFood,
//                        orderSend = OrderSend,
//                        restaurant = Restaurant,
//                        customerInfor = CustomerInfor
//                    };

//                    var connectionsDriver = _userConnectionManager.GetUserConnections(userIdDriver.ToString());

//                    var jsonStr = JsonConvert.SerializeObject(json);
//                    foreach (var connectionId in connectionsDriver)
//                    {
//                        await Clients.Client(connectionId).SendAsync("orderToDriver", jsonStr);
//                    }
//                }
//            }
//            else
//            {
//                resError.status = "Không tìm thấy tài xế";

//                //lấy connectionId của client
//                var connectionsCustomer = _userConnectionManager.GetUserConnections(Order.CustomerId.ToString());

//                var jsonStr = JsonConvert.SerializeObject(resError);
//                foreach (var connectionId in connectionsCustomer)
//                {
//                    await Clients.Client(connectionId).SendAsync("orderAccept", jsonStr);
//                }
//            }
            
//        }

//        public async Task DriverConfirm(string order, string customerInfor, int RestaurantId, string orderFood, string orderSend, bool confirm)
//        {
//            var Order = JsonConvert.DeserializeObject<Order>(order);
//            Order.Note = (Order.Note == null ? " " : Order.Note);
//            var driver = _client.Search<Model.UserDriverSearch>(s => s
//                     .Index("userdriver")
//                     .Type("userdriversearch")
//                     .Query(q => q
//                         .Term(f => f.Id, Order.DriverId)
//                        )
//                      );


//            var driverInfor = driver.Documents.FirstOrDefault();

//            if (confirm)
//            {
                
//                //update driver               
//                driverInfor.DriverActivity = (int)EDriverAcivity.Process;
                             

//                //Cập nhật lịch sử đơn hàng
//                var historyOrder = new OrderDriverHistory()
//                {
//                    OrderId = Order.Id,
//                    DriverId = Order.DriverId,
//                    Status = (int)EOrderDriverHistory.Accept
//                };

//                //Cập nhật lịch sử order
//                await _generic.CreateAsync<OrderDriverHistory>(historyOrder);

//                //Cập nhất order
//                Order.Status = (int)EOrderStatus.Process;
//                await _generic.UpdateAsync<Order>(Order);

//                //push message
//                driverInfor.Avatar = (driverInfor.Avatar == null ? "" : driverInfor.Avatar);
//                var resScuccess = new Response();
//                resScuccess.response = new {
//                    driverInfor = driverInfor,
//                    Order = Order
//                };

              

//                var connectionsCustomer = _userConnectionManager.GetUserConnections(Order.CustomerId.ToString());
//                var jsonStr = JsonConvert.SerializeObject(resScuccess);
//                foreach (var connectionId in connectionsCustomer)
//                {
//                    await Clients.Client(connectionId).SendAsync("orderAccept", jsonStr);
//                }

//            }
//            else
//            {
//                driverInfor.DriverActivity = (int)EDriverAcivity.None;

//                var historyOrder = new OrderDriverHistory()
//                {
//                    OrderId = Order.Id,
//                    DriverId = Order.DriverId,
//                    Status = (int)EOrderDriverHistory.NotAccept
//                };
//                await _generic.CreateAsync<OrderDriverHistory>(historyOrder);

//                CustomerOrder(order, orderFood, RestaurantId, orderSend, customerInfor);
//            }

//            //Update driver
//            await _client.IndexAsync<Model.UserDriverSearch>(driverInfor, i => i
//                                           .Index("userdriver")
//                                           .Type("userdriversearch")
//                                           .Id(driverInfor.Id)
//                                           .Refresh(Elasticsearch.Net.Refresh.True));
//        }

//        public async Task OrderConfirm(string order, int type)
//        {
//            var Order = JsonConvert.DeserializeObject<Order>(order);
//            Order.Note = (Order.Note == null ? " " : Order.Note);
//            if (type == 2)
//            {
//                //Giao hàng thành công
//                //update driver
//                var driver = _client.Search<Model.UserDriverSearch>(s => s
//                     .Index("userdriver")
//                     .Type("userdriversearch")
//                     .Query(q => q
//                         .Term(f => f.Id, Order.DriverId)
//                        )
//                      );


//                var driverInfor = driver.Documents.FirstOrDefault();

//                driverInfor.DriverActivity = (int)EDriverAcivity.None;
//                driverInfor.ConnectUser = 0;



//                await _client.IndexAsync<API.Mobile.Model.UserDriverSearch>(driverInfor, i => i
//                                           .Index("userdriver")
//                                           .Type("userdriversearch")
//                                           .Id(driverInfor.Id)
//                                           .Refresh(Elasticsearch.Net.Refresh.True));


//                //Update order         
//                Order.Status = (int)EOrderStatus.Paid;
//                Order.IsCompleted = true;
//                var result = await _generic.UpdateAsync<Order>(Order);


//                //push message
//                var json = new
//                {
//                    type = 2,
//                    Message = "Đơn hàng hoàn tất"
//                };

//                var connectionsCustomer = _userConnectionManager.GetUserConnections(Order.CustomerId.ToString());
//                var jsonStr = JsonConvert.SerializeObject(json);
//                foreach (var connectionId in connectionsCustomer)
//                {
//                    await Clients.Client(connectionId).SendAsync("orderStatus", jsonStr);
//                }
//            }
//            else if (type == 4)
//            {
//                //Bị boom hàng
//                var driver = _client.Search<Model.UserDriverSearch>(s => s
//                     .Index("userdriver")
//                     .Type("userdriversearch")
//                     .Query(q => q
//                         .Term(f => f.Id, Order.DriverId)
//                        )
//                      );


//                var driverInfor = driver.Documents.FirstOrDefault();

//                driverInfor.DriverActivity = (int)EDriverAcivity.None;
//                driverInfor.ConnectUser = 0;



//                await _client.IndexAsync<API.Mobile.Model.UserDriverSearch>(driverInfor, i => i
//                                           .Index("userdriver")
//                                           .Type("userdriversearch")
//                                           .Id(driverInfor.Id)
//                                           .Refresh(Elasticsearch.Net.Refresh.True));


//                //Update order         
//                Order.Status = (int)EOrderStatus.BOM;
//                var result = await _generic.UpdateAsync<Order>(Order);

//                var json = new
//                {
//                    type = 4,
//                    Message = "Bạn đã không hoàn tất đơn hàng theo quy đinh. Chúng tôi sẽ liên lạc lại bạn để bạn để sử lý!"
//                };

//                var connectionsCustomer = _userConnectionManager.GetUserConnections(Order.CustomerId.ToString());
//                var jsonStr = JsonConvert.SerializeObject(json);
//                foreach (var connectionId in connectionsCustomer)
//                {
//                    await Clients.Client(connectionId).SendAsync("orderStatus", jsonStr);
//                }
//            }                    
//        }
//    }

//    public interface IUserConnectionManager
//    {
//        void KeepUserConnection(string userId, string connectionId);
//        void RemoveUserConnection(string connectionId);
//        List<string> GetUserConnections(string userId);
//        string GetUserId(string connectionId);
//    }

//    public class UserConnectionManager : IUserConnectionManager
//    {
//        private static Dictionary<string, List<string>> userConnectionMap = new Dictionary<string, List<string>>();
//        private static string userConnectionMapLocker = string.Empty;//find away to avoid lock later

//        public void KeepUserConnection(string userId, string connectionId)
//        {
//            lock (userConnectionMapLocker)
//            {
//                if (!userConnectionMap.ContainsKey(userId))
//                {
//                    userConnectionMap[userId] = new List<string>();
//                }
//                userConnectionMap[userId].Add(connectionId);
//            }
//        }

//        public void RemoveUserConnection(string connectionId)
//        {
//            //Remove the connectionId of user 
//            lock (userConnectionMapLocker)
//            {
//                foreach (var userId in userConnectionMap.Keys)
//                {
//                    if (userConnectionMap.ContainsKey(userId))
//                    {
//                        if (userConnectionMap[userId].Contains(connectionId))
//                        {
//                            userConnectionMap[userId].Remove(connectionId);
//                            break;
//                        }
//                    }
//                }
//            }
//        }
//        public List<string> GetUserConnections(string userId)
//        {
//            try
//            {
//                var conn = new List<string>();
//                lock (userConnectionMapLocker)
//                {
//                    conn = userConnectionMap[userId];
//                }
//                return conn;
//            }
//            catch
//            {
//                return new List<string>();
//            }

//        }

//        public string GetUserId(string connectionId)
//        {
//            try
//            {
//                foreach (var userId in userConnectionMap.Keys)
//                {
//                    if (userConnectionMap[userId].Contains(connectionId))
//                    {
//                        return userId;
//                    }
//                }
//                return "";
//            }
//            catch
//            {
//                return "";
//            }

//        }
//    }
//}
