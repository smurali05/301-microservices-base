﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class FoodOrderMapping
    {
        public int? TblFoodOrderId { get; set; }
        public int? TblMenuId { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }
        public int Quantity { get; set; }
        public bool IsItemOutOfStock { get; set; }
    }
}