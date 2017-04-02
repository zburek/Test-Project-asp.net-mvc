using Service.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.Services
{
    public class VehicleModelRepository : IRepository<VehicleModel>
    {
        VehicleContext vehicleContext;
        public VehicleModelRepository()
        {
            vehicleContext = new VehicleContext();
        }
        public List<VehicleModel> IndexList(string sortOrder, string searchString)
        {
            var vehicleModelList = vehicleContext.VehicleModels.ToList();

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
        public void Add(VehicleModel vehicleModel)
        {
            vehicleContext.VehicleModels.Add(vehicleModel);
            vehicleContext.SaveChanges();
        }
        public List<VehicleModel> List
        {
            get
            {
                var vehicleModelList = vehicleContext.VehicleModels.ToList();
                return (vehicleModelList);
            }
        }
        public VehicleModel FindById(int? Id)
        {
            var vehicleModel = vehicleContext.VehicleModels.Single(x => x.Id == Id);
            return (vehicleModel);
        }
        public void Edit(VehicleModel editModelView)
        {
            vehicleContext.Entry(editModelView).State = EntityState.Modified;
            vehicleContext.SaveChanges();
        }
        public void Delete(int? Id)
        {
            var vehicleModel = vehicleContext.VehicleModels.Single(x => x.Id == Id);
            vehicleContext.VehicleModels.Remove(vehicleModel);
            vehicleContext.SaveChanges();
        }
    }
}
