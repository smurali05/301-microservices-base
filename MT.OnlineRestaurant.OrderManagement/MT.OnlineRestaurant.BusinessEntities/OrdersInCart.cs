using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class OrdersInCart
    {
        public int? TblCustomerId { get; set; }
        public int? TblRestaurantId { get; set; }
        public int? TblOrderStatusId { get; set; }
        public int? TblPaymentTypeId { get; set; }
        public decimal TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }

    }
}
