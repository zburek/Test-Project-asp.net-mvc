using Service.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.Services
{
    public class VehicleMakeRepository : IRepository<VehicleMake>
    {
        VehicleContext vehicleContext;
        public VehicleMakeRepository()
        {
            vehicleContext = new VehicleContext();
        }
        public List<VehicleMake> IndexList(string sortOrder, string searchString)
        {
            var vehicleMakeList = vehicleContext.VehicleMakes.ToList();

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
        public void Add(VehicleMake vehicleMake)
        {
            vehicleContext.VehicleMakes.Add(vehicleMake);
            vehicleContext.SaveChanges();
        }
        public List<VehicleMake> List
        {
            get
            {
                var vehicleMakeList = vehicleContext.VehicleMakes.ToList();
                return (vehicleMakeList);
            }
        } 
        public VehicleMake FindById(int? Id)
        {
            var vehicleMake = vehicleContext.VehicleMakes.Single(x => x.Id == Id);
            return (vehicleMake);
        }
        public void Edit(VehicleMake vehicleMake)
        {
            vehicleContext.Entry(vehicleMake).State = EntityState.Modified;
            vehicleContext.SaveChanges();
        }
        public void Delete(int? Id)
        {
            var vehicleModelList = vehicleContext.VehicleModels.Where(x => x.MakeId == Id).ToList();

            vehicleModelList.ForEach(model => vehicleContext.VehicleModels.Remove(model));
            vehicleContext.SaveChanges();

            var vehicleMake = vehicleContext.VehicleMakes.Single(x => x.Id == Id);
            vehicleContext.VehicleMakes.Remove(vehicleMake);
            vehicleContext.SaveChanges();
        }
    }
}
