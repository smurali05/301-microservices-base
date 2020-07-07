using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
    public class Menu
    {
        public string Item { get; set; }
        public int TblCuisineId { get; set; }
        public int Id { get; set; }
        public int UserCreated { get; set; }
        public int UserModified { get; set; }
        public DateTime RecordTimeStamp { get; set; }
        public DateTime RecordTimeStampCreated { get; set; }
        public int quantity { get; set; }
    }
}
