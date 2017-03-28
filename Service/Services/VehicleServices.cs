using Service.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.Services
{
    #region Vehicle Services Interfaces
    public interface ISortedVehicleMakeList { List<VehicleMake> IndexMakeList(string sortOrder, string searchString); }
    public interface ISortedVehicleModelList { List<VehicleModel> IndexModelList(string sortOrder, string searchString); }
    public interface IAddVehicleMake { VehicleMake CreateMake(VehicleMake vehicleMake); }
    public interface IAddVehicleModel { VehicleModel CreateModel(VehicleModel vehicleModel); }
    public interface IVehicleMakeList { List<VehicleMake> MakeList(); }
    public interface IVehicleModelList { List<VehicleModel> ModelList(); }
    public interface IVehicleMake { VehicleMake vehicleMake(int? Id); }
    public interface IEditVehicleMake { VehicleMake EditMake(VehicleMake editMakeView); }
    public interface IVehicleModel { VehicleModel vehicleModel(int? Id); }
    public interface IEditVehicleModel { VehicleModel EditModel(VehicleModel editModelView); }
    public interface IDeleteVehicleMake { void DeleteVehicleMake(int id); }
    public interface IDeleteVehicleModel { void DeleteVehicleModel(int id); }
    #endregion

    public class VehicleServices : ISortedVehicleMakeList, ISortedVehicleModelList, IAddVehicleMake, IAddVehicleModel, IVehicleMakeList, IVehicleModelList, IVehicleMake, IEditVehicleMake, IVehicleModel, IEditVehicleModel, 
        IDeleteVehicleMake, IDeleteVehicleModel
    {
        #region Index
        public List<VehicleMake> IndexMakeList(string sortOrder, string searchString)
        {
            List<VehicleMake> vehicleMakeList = vehicleContext.VehicleMakes.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakeList = vehicleContext.VehicleMakes.Where(v => v.Name.Contains(searchString) || v.Abrv.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakeList = vehicleMakeList.OrderByDescending(v => v.Name).ToList();
                    break;
                case "Name":
                    vehicleMakeList = vehicleMakeList.OrderBy(v => v.Name).ToList();
                    break;
                case "Abrv":
                    vehicleMakeList = vehicleMakeList.OrderBy(v => v.Abrv).ToList();
                    break;
                case "abrv_desc":
                    vehicleMakeList = vehicleMakeList.OrderByDescending(v => v.Abrv).ToList();
                    break;
                default:
                    vehicleMakeList = vehicleMakeList.OrderBy(v => v.Id).ToList();
                    break;
            }
            return (vehicleMakeList);
        }
        public List<VehicleModel> IndexModelList(string sortOrder, string searchString)
        {
            List<VehicleModel> vehicleModelList = vehicleContext.VehicleModels.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModelList = vehicleContext.VehicleModels.Where(v => v.VehicleMake.Name.Contains(searchString) || v.Abrv.Contains(searchString)).ToList();
            }
                switch (sortOrder)
            {
                case "Make":
                    vehicleModelList = vehicleModelList.OrderByDescending(v => v.VehicleMake.Name).ToList();
                    break;
                case "make_desc":
                    vehicleModelList = vehicleModelList.OrderBy(v => v.VehicleMake.Name).ToList();
                    break;
                case "name_desc":
                    vehicleModelList = vehicleModelList.OrderByDescending(v => v.Name).ToList();
                    break;
                case "Name":
                    vehicleModelList = vehicleModelList.OrderBy(v => v.Name).ToList();
                    break;
                case "Abrv":
                    vehicleModelList = vehicleModelList.OrderBy(v => v.Abrv).ToList();
                    break;
                case "abrv_desc":
                    vehicleModelList = vehicleModelList.OrderByDescending(v => v.Abrv).ToList();
                    break;
                default:
                    vehicleModelList = vehicleModelList.OrderBy(v => v.Id).ToList();
                    break;
            }
            return (vehicleModelList);
        }
        #endregion

        #region Create
        public VehicleMake CreateMake(VehicleMake vehicleMake)
        {
            vehicleContext.VehicleMakes.Add(vehicleMake);
            vehicleContext.SaveChanges();
            return (vehicleMake);
        }
        public VehicleModel CreateModel(VehicleModel vehicleModel)
        {
            vehicleContext.VehicleModels.Add(vehicleModel);
            vehicleContext.SaveChanges();
            return (vehicleModel);
        }

        #endregion

        #region Find and Edit

        private VehicleContext vehicleContext = new VehicleContext();
        public List<VehicleMake> MakeList()
        {
            List<VehicleMake> vehicleMakeList = vehicleContext.VehicleMakes.ToList();
            return (vehicleMakeList);
        }
        public VehicleMake vehicleMake(int? Id)
        {
            VehicleMake vehicleMake = vehicleContext.VehicleMakes.Single(x => x.Id == Id);
            return (vehicleMake);
        }
        public VehicleMake EditMake(VehicleMake editMakeView)
        {
            vehicleContext.Entry(editMakeView).State = EntityState.Modified;
            vehicleContext.SaveChanges();
            return (editMakeView);
        }
        public List<VehicleModel> ModelList()
        {
            List<VehicleModel> vehicleModelList = vehicleContext.VehicleModels.ToList();
            return (vehicleModelList);
        }
        public VehicleModel vehicleModel(int? Id)
        {
            VehicleModel vehicleModel = vehicleContext.VehicleModels.Single(x => x.Id == Id);
            return (vehicleModel);
        }
        public VehicleModel EditModel(VehicleModel editModelView)
        {
            vehicleContext.Entry(editModelView).State = EntityState.Modified;
            vehicleContext.SaveChanges();
            return (editModelView);
        }

        #endregion

        #region Delete
        public void DeleteVehicleMake(int id)
        {
            List<VehicleModel> vehicleModelList = vehicleContext.VehicleModels.Where(x => x.MakeId == id).ToList();

            vehicleModelList.ForEach(model => vehicleContext.VehicleModels.Remove(model));
            vehicleContext.SaveChanges();

            VehicleMake vehicleMake = vehicleContext.VehicleMakes.Single(x => x.Id == id);
            vehicleContext.VehicleMakes.Remove(vehicleMake);
            vehicleContext.SaveChanges();
        }

        public void DeleteVehicleModel(int id)
        {
            VehicleModel vehicleModel = vehicleContext.VehicleModels.Single(x => x.Id == id);
            vehicleContext.VehicleModels.Remove(vehicleModel);
            vehicleContext.SaveChanges();
        }
        #endregion
    }
}
