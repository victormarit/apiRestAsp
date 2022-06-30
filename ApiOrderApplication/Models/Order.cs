namespace ApiOrderApplication.Models
{
    public class Order
    {
        public long Id { get; set; }
        public Entry? Entry { get; set; }
        public Dish? Dish { get; set; }
        public Dessert? Dessert { get; set; }
        public Drink? Drink { get; set; }
    }
}