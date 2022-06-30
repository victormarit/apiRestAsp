namespace ApiOrderApplication.Models
{
    public class Drink : IFood
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool? IsReady { get; set; }
        public bool? IsDeliver { get; set; }
    }
}
