namespace Core.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string Client { get; set; }
        public string Info { get; set; }
        public bool IsDeleted { get; set; }
    }
}
