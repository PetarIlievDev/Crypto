namespace WebApi.MapperProfiles
{
    using AutoMapper;
    using WebApi.Models;
    using WebApi.Models.ChangeFromInitialBuy;
    using WebApi.Services.Models.ChangeFromInitialBuy;
    using WebApi.Services.Models.LogToFile;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChangeFormInitialBuyRequest, InitialBuyData>();
            CreateMap<ChangeFromInitialBuyDataRequest, InitialBuyDataFromRequest>();
            CreateMap<CalculatedOverallChangeFromInitialBuy, OverallChangeFromInitalBuyResponse>();
            CreateMap<CalculatedChangeFromInitialBuy, ChangeFromInitialBuyDataResponse>();
            CreateMap<SaveToLogRequest, LogToFileData>();
        }
    }
}
