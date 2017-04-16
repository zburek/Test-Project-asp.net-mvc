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
            IQueryable<VehicleModel> vehicleModel;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModel = from model in vehicleContext.VehicleModels
                               where model.VehicleMake.Name.Contains(searchString) || model.Abrv.Contains(searchString)
                               select model;
            }
            else
            {
                vehicleModel = from model in vehicleContext.VehicleModels
                               select model;
            }

            IPagedList<VehicleModel> vehicleModelList;
            switch (sortOrder)
            {
                case "Make":
                    vehicleModelList = vehicleModel.OrderBy(v => v.VehicleMake.Name).ToPagedList(pageNumber, pageSize);
                    break;
                case "make_desc":
                    vehicleModelList = vehicleModel.OrderByDescending(v => v.VehicleMake.Name).ToPagedList(pageNumber, pageSize);
                    break;
                case "Name":
                    vehicleModelList = vehicleModel.OrderBy(v => v.Name).ToPagedList(pageNumber, pageSize);
                    break;
                case "name_desc":
                    vehicleModelList = vehicleModel.OrderByDescending(v => v.Name).ToPagedList(pageNumber, pageSize);
                    break;
                case "Abrv":
                    vehicleModelList = vehicleModel.OrderBy(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                    break;
                case "abrv_desc":
                    vehicleModelList = vehicleModel.OrderByDescending(v => v.Abrv).ToPagedList(pageNumber, pageSize);
                    break;
                default:
                    vehicleModelList = vehicleModel.OrderBy(v => v.Id).ToPagedList(pageNumber, pageSize);
                    break;
            }
            return (vehicleModelList);
        }
        public void Add(VehicleModel vehicleModel)
        {
            vehicleContext.VehicleModels.Add(vehicleModel);
            vehicleContext.SaveChanges();
        }
        public IEnumerable<VehicleModel> List
        {
            get
            {
                var vehicleModelList = from model in vehicleContext.VehicleModels
                                       select model;
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
