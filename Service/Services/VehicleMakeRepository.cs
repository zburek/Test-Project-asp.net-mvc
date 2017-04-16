using PagedList;
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
        public IPagedList<VehicleMake> IndexList(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            IQueryable<VehicleMake> vehicleMake;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMake = from make in vehicleContext.VehicleMakes
                              where make.Name.Contains(searchString) || make.Abrv.Contains(searchString)
                              select make;
            }
            else
            {
                vehicleMake = from make in vehicleContext.VehicleMakes
                              select make;
            }

            IPagedList<VehicleMake> vehicleMakeList; 
            switch (sortOrder)
            {
                case "Name":
                    vehicleMakeList = vehicleMake.OrderBy(v => v.Name).ToPagedList(pageNumber, pageSize);
                    break;
                case "name_desc":
                    vehicleMakeList = vehicleMake.OrderByDescending(v => v.Name).ToPagedList(pageNumber, pageSize);
                    break;
                case "Abrv":
                    vehicleMakeList = vehicleMake.OrderBy(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                    break;
                case "abrv_desc":
                    vehicleMakeList = vehicleMake.OrderByDescending(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                    break;
                default:
                    vehicleMakeList = vehicleMake.OrderBy(v => v.Id).ToPagedList(pageNumber, pageSize);
                    break;
            }
            return (vehicleMakeList);
        }
        public void Add(VehicleMake vehicleMake)
        {
            vehicleContext.VehicleMakes.Add(vehicleMake);
            vehicleContext.SaveChanges();
        }
        public IEnumerable<VehicleMake> List
        {
            get
            {
                var vehicleMakeList = from make in vehicleContext.VehicleMakes
                                      select make;
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
