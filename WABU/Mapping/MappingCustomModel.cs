using AutoMapper;
using FW.Models;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;

namespace WABU.Mapping
{
    public class MappingCustomModel : Profile
    {
        public override string ProfileName => "MappingCustomModel";

        public MappingCustomModel()
        {
           CreateMap<BiddingNews, BiddingNewsBidContractionDetailVM>();
        }
    }
}