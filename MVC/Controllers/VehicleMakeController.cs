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
    public class VehicleMakeController : Controller
    {
        //public VehicleServices vehicleServices = new VehicleServices();
        // GET: VehicleMake
        public ActionResult Index(string currentFilter, int? page, string sortOrder = null, string searchString = null)
        {
            ViewBag.CurrentSort = sortOrder;  
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ISortedVehicleMakeList vehicleServices = new VehicleServices();
            List<VehicleMake> vehicleMakeList = vehicleServices.IndexMakeList(sortOrder, searchString);
            List<IndexViewModel> indexViewModel = Mapper.Map<List<VehicleMake>, List<IndexViewModel>>(vehicleMakeList);
            return View(indexViewModel.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: VehicleMake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Abrv")] CreateMakeViewModel makeViewMake)
        {
            IAddVehicleMake vehicleServices = new VehicleServices();
            try
            {
                if (ModelState.IsValid)
                {
                    vehicleServices.CreateMake(makeViewMake);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to sace changes. Try again and if the problem persists see your system administrator.");
            }
            return View(makeViewMake);
        }
        
        // GET: VehicleMake/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVehicleMakeList vehicleServicesMakeList = new VehicleServices();
            List<VehicleMake> vehicleMakeList = vehicleServicesMakeList.MakeList();
            IVehicleMake vehicleServicesMake = new VehicleServices();
            VehicleMake vehicleMake = vehicleServicesMake.vehicleMake(id);
            EditMakeViewModel editMakeView = Mapper.Map<VehicleMake, EditMakeViewModel>(vehicleMake);
            if (editMakeView == null)
            {
                return HttpNotFound();
            }
            return View(editMakeView);
        }
        
        // POST: VehicleMake/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Abrv")] EditMakeViewModel editMakeView)
        {
            IEditVehicleMake vehicleServices = new VehicleServices();
            try
            {
                if (ModelState.IsValid)
                {
                    VehicleMake editVehicleMake = Mapper.Map<EditMakeViewModel, VehicleMake>(editMakeView);
                    vehicleServices.EditMake(editVehicleMake);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again and if the problem persists see your system administrator.");
            }
            return View(editMakeView);
        }

        // GET: VehicleMake/Delete/5
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
            IVehicleMake vehicleServices = new VehicleServices();
            VehicleMake vehicleMake = vehicleServices.vehicleMake(id);
            DeleteViewModel deleteMakeView = Mapper.Map<VehicleMake, DeleteViewModel>(vehicleMake);
            if (deleteMakeView == null)
            {
                return HttpNotFound();
            }
            return View(deleteMakeView);
        }
        
        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IDeleteVehicleMake vehicleServices = new VehicleServices();
            try
            {
                vehicleServices.DeleteVehicleMake(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }         
    }
    
}
