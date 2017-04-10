using PagedList;
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
        public IPagedList<VehicleModel> IndexList(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            IPagedList<VehicleModel> vehicleModelList = new List<VehicleModel>().ToPagedList(pageNumber, pageSize);
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModelList = vehicleContext.VehicleModels.Where(v => v.VehicleMake.Name.Contains(searchString) || v.Abrv.Contains(searchString)).OrderBy(v => v.Id).ToPagedList(pageNumber, pageSize);
                switch (sortOrder)
                {
                    case "Make":
                        vehicleModelList = vehicleModelList.OrderBy(v => v.VehicleMake.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "make_desc":
                        vehicleModelList = vehicleModelList.OrderByDescending(v => v.VehicleMake.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "name_desc":
                        vehicleModelList = vehicleModelList.OrderByDescending(v => v.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "Name":
                        vehicleModelList = vehicleModelList.OrderBy(v => v.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "Abrv":
                        vehicleModelList = vehicleModelList.OrderBy(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                        break;
                    case "abrv_desc":
                        vehicleModelList = vehicleModelList.OrderByDescending(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "Make":
                        vehicleModelList = vehicleContext.VehicleModels.OrderByDescending(v => v.VehicleMake.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "make_desc":
                        vehicleModelList = vehicleContext.VehicleModels.OrderBy(v => v.VehicleMake.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "name_desc":
                        vehicleModelList = vehicleContext.VehicleModels.OrderByDescending(v => v.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "Name":
                        vehicleModelList = vehicleContext.VehicleModels.OrderBy(v => v.Name).ToPagedList(pageNumber, pageSize);
                        break;
                    case "Abrv":
                        vehicleModelList = vehicleContext.VehicleModels.OrderBy(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                        break;
                    case "abrv_desc":
                        vehicleModelList = vehicleContext.VehicleModels.OrderByDescending(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                        break;
                    default:
                        vehicleModelList = vehicleContext.VehicleModels.OrderBy(v => v.Id).ToPagedList(pageNumber, pageSize);
                        break;
                }
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
