using Microsoft.AspNetCore.Http;

namespace Agency.ViewModels.Product
{
    public class PortfolioUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public string Category { get; set; }
        public string Client { get; set; }
        public string Info { get; set; }
    }
}
