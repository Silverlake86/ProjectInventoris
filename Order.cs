using System;

namespace ProjectInventoris
{
    public class Order
    {
        public int order_id { get; set; }
        public string title { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_type_name { get; set; }
        public int number_shipped { get; set; }
        public DateTime order_date { get; set; }

    }
}
