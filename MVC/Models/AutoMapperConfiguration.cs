using AutoMapper;
using MVC.ViewModels;
using Service.DataAccessLayer;

namespace MVC.Models
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new IndexMakeProfile());
                cfg.AddProfile(new EditMakeProfile());
                cfg.AddProfile(new DeleteMakeProfile());

                cfg.AddProfile(new IndexModelProfile());
                cfg.AddProfile(new EditModelProfile());
                cfg.AddProfile(new DeleteModelProfile());
            });
        }
    }

    #region Make profiles
    internal class IndexMakeProfile : Profile
    {
        public IndexMakeProfile()
        {
            CreateMap<VehicleMake, IndexViewModel>();
        }
    }
    internal class EditMakeProfile : Profile
    {
        public EditMakeProfile()
        {
            CreateMap<VehicleMake, EditMakeViewModel>().ReverseMap();
        }
    }
    internal class DeleteMakeProfile : Profile
    {
        public DeleteMakeProfile()
        {
            CreateMap<VehicleMake, DeleteViewModel>();
        }
    }
    #endregion

    #region Model profiles
    internal class IndexModelProfile : Profile
    {
        public IndexModelProfile()
        {
            CreateMap<VehicleModel, IndexViewModel>();
        }
    }
    internal class EditModelProfile : Profile
    {
        public EditModelProfile()
        {
            CreateMap<EditModelViewModel, VehicleModel>().ReverseMap();
        }
    }
    internal class DeleteModelProfile : Profile
    {
        public DeleteModelProfile()
        {
            CreateMap<VehicleModel, DeleteViewModel>();
        }
    }
    #endregion
}
