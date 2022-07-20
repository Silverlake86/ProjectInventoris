using System;

namespace ProjectInventoris
{
    public class Purchase
    {
        public int purchase_id { get; set; }
        public int supplier_id { get; set; }
        public string supplier_name { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int number_received { get; set; }
        public DateTime purchase_date { get; set; }
    }
}
