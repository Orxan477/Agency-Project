using Agency.ViewModels.Product;
using AutoMapper;
using Core.Models;

namespace Agency.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Portfolio, PortfolioUpdateVM>();
        }
    }
}
