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
                cfg.AddProfile(new EditMakeGetProfile());
                cfg.AddProfile(new EditMakePostProfile());
                cfg.AddProfile(new DeleteMakeProfile());

                cfg.AddProfile(new IndexModelProfile());
                cfg.AddProfile(new EditModelGetProfile());
                cfg.AddProfile(new EditModelPostProfile());
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
    internal class EditMakeGetProfile : Profile
    {
        public EditMakeGetProfile()
        {
            CreateMap<VehicleMake, EditMakeViewModel>();
        }
    }
    internal class EditMakePostProfile : Profile
    {
        public EditMakePostProfile()
        {
            CreateMap<EditMakeViewModel, VehicleMake>();
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
    internal class EditModelGetProfile : Profile
    {
        public EditModelGetProfile()
        {
            CreateMap<VehicleModel, EditModelViewModel>();
        }
    }
    internal class EditModelPostProfile : Profile
    {
        public EditModelPostProfile()
        {
            CreateMap<EditModelViewModel, VehicleModel>();
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
