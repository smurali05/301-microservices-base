#region References
using Microsoft.Extensions.Configuration;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#endregion

#region namespace
namespace MT.OnlineRestaurant.BusinessLayer
{
    #region Class Definition
    /// <summary>
    /// Implements the interface methods for booking the table for a restaurant
    /// </summary>
    public class BookYourTableBusiness : IBookYourTableBusiness
    {
        #region privateVariables
        private readonly IBookYourTableRepository _bookYourTableRepository;
        private string CallforTableNumberURL;
        //  "https://localhost:44381/api/Search/GetTableBooked";

        #endregion
        private IConfiguration _configuration;
        public BookYourTableBusiness(IBookYourTableRepository bookYourTableRepository, IConfiguration configuration)
        {
            _bookYourTableRepository = bookYourTableRepository;
            _configuration = configuration;
            CallforTableNumberURL = configuration.GetSection("SearchUrl").Value;
        }

        #region Interface Methods
        public async Task<bool> BookYourTable(BookTable bookingTable)
        {

            TblTableOrder tblTableOrder;

            if (bookingTable != null)
            {
                tblTableOrder = new TblTableOrder();
                tblTableOrder.TblRestaurantId = bookingTable.RestaurantId;
                tblTableOrder.TblCustomerId = bookingTable.CustomerId;
                tblTableOrder.FromDate = bookingTable.BookingDate;
                tblTableOrder.RecordTimeStampCreated = DateTime.Now;
                var tableorderId = _bookYourTableRepository.BookYourTable(tblTableOrder);
                string urlParameters = "?RestaurantId=" + bookingTable.RestaurantId + "&Capacity=" + bookingTable.capacityCount;
                int NumberofTables = 0;
                using (var httpClientHandler = new HttpClientHandler())
                {

                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    HttpClient client = new HttpClient(httpClientHandler);
                    HttpResponseMessage response = await client.GetAsync(new Uri(CallforTableNumberURL + urlParameters));
                    NumberofTables = Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
                }
                TblTableOrderMapping tbltableOrderMappings = new TblTableOrderMapping()
                {

                    TableNo = NumberofTables,
                    TblTableOrderId = tableorderId,
                    RecordTimeStamp = DateTime.UtcNow,
                    RecordTimeStampCreated = DateTime.UtcNow,
                    Active = true,
                    UserCreated = bookingTable.CustomerId,
                    UserModified = bookingTable.CustomerId,
                    Price = 50 * NumberofTables

                };
                _bookYourTableRepository.AddTableOrderMapping(tbltableOrderMappings);
                if (tableorderId > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// verifies whether the table is available for booking or not
        /// </summary>
        /// <param name="tblTableOrder"></param>
        /// <returns></returns>
        public bool CheckTableAvailability(BookTable bookingTable)
        {
            TblTableOrder tblTableOrder;

            if (bookingTable != null)
            {
                tblTableOrder = new TblTableOrder();
                tblTableOrder.TblRestaurantId = bookingTable.RestaurantId;
                tblTableOrder.TblCustomerId = bookingTable.CustomerId;
                tblTableOrder.FromDate = bookingTable.BookingDate;

                return _bookYourTableRepository.CheckTableAvailability(tblTableOrder);

            }

            return false;

        }

        /// <summary>
        /// Updates the existing booking info
        /// </summary>
        /// <param name="bookTable"></param>
        /// <returns></returns>
        public bool UpdateBooking(BookTable bookTable)
        {
            TblTableOrder tblTableOrder;

            if (bookTable != null)
            {
                tblTableOrder = new TblTableOrder();
                tblTableOrder.TblRestaurantId = bookTable.RestaurantId;
                tblTableOrder.TblCustomerId = bookTable.CustomerId;
                tblTableOrder.FromDate = bookTable.BookingDate;
                tblTableOrder.RecordTimeStamp = DateTime.Now;
                if (_bookYourTableRepository.BookYourTable(tblTableOrder) > 0)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
    #endregion
}
#endregion
