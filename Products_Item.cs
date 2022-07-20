namespace ProjectInventoris
{
    public class Products_Item
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_type_name { get; set; }
        public int starting_inventory { get; set; }
        public int inventory_received { get; set; }
        public int inventory_shipped { get; set; }
        public int inventory_on_hand { get; set; }
        public int minimum_required { get; set; }
    }
}
