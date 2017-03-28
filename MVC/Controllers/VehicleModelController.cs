using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Mvc;
using MVC.ViewModels;
using Service.DataAccessLayer;
using Service.Services;
using AutoMapper;
using PagedList;

namespace MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        // GET: VehicleMake
        public ActionResult Index(string currentFilter, int? page, string sortOrder = null, string searchString = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            ViewBag.MakeSortParm = sortOrder == "Make" ? "make_desc" : "Make";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ISortedVehicleModelList vehicleServices = new VehicleServices();
            List<VehicleModel> vehicleModelList = vehicleServices.IndexModelList(sortOrder, searchString);
            List<IndexViewModel> indexViewModel = Mapper.Map<List<VehicleModel>, List<IndexViewModel>>(vehicleModelList);
            return View(indexViewModel.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: VehicleModel/Create
        public ActionResult Create()
        {
            CreateModelViewModel vehicleMakeList = new CreateModelViewModel();
            return View(vehicleMakeList);
        }

        // POST: VehicleModel/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MakeId,Name,Abrv")] CreateModelViewModel modelViewModel)
        {
            IAddVehicleModel vehicleServices = new VehicleServices();
            try
            {
                if (ModelState.IsValid)
                {
                    vehicleServices.CreateModel(modelViewModel);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(modelViewModel);
        }
        
        // GET: VehicleModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleModel vehicleServices = new VehicleServices();
            VehicleModel vehicleModel = vehicleServices.vehicleModel(id);
            EditModelViewModel editModelView = Mapper.Map<VehicleModel, EditModelViewModel>(vehicleModel);
            if (editModelView == null)
            {
                return HttpNotFound();
            }
            return View(editModelView);
        }
        
        // POST: VehicleModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MakeId,Name,Abrv")] EditModelViewModel editModelView)
        {
            IEditVehicleModel vehicleServices = new VehicleServices();
            try
            {
                if (ModelState.IsValid)
                {
                    VehicleModel editVehicleModel = Mapper.Map<EditModelViewModel, VehicleModel>(editModelView);
                    vehicleServices.EditModel(editVehicleModel);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(editModelView);
        }

        // GET: VehicleModel/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete faild. Try again and if the problem persists see your system administrator.";
            }
            IVehicleModel vehicleServices = new VehicleServices();
            VehicleModel vehicleModel = vehicleServices.vehicleModel(id);
            DeleteViewModel deleteModelView = Mapper.Map<VehicleModel, DeleteViewModel>(vehicleModel);
            if (deleteModelView == null)
            {
                return HttpNotFound();
            }
            return View(deleteModelView);
        }
        
        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IDeleteVehicleModel vehicleServices = new VehicleServices();
            try
            {
                vehicleServices.DeleteVehicleModel(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
