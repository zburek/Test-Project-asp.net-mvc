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
        IRepository<VehicleModel> repository = new VehicleModelRepository();
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

            var vehicleModelList = repository.IndexList(sortOrder, searchString);
            var indexViewModel = Mapper.Map<List<VehicleModel>, List<IndexViewModel>>(vehicleModelList);
            return View(indexViewModel.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: VehicleModel/Create
        public ActionResult Create()
        {
            var vehicleMakeList = new CreateModelViewModel();
            return View(vehicleMakeList);
        }

        // POST: VehicleModel/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MakeId,Name,Abrv")] CreateModelViewModel modelViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Add(modelViewModel);
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
            var vehicleModel = repository.FindById(id);
            var editModelView = Mapper.Map<VehicleModel, EditModelViewModel>(vehicleModel);
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
            try
            {
                if (ModelState.IsValid)
                {
                    var editVehicleModel = Mapper.Map<EditModelViewModel, VehicleModel>(editModelView);
                    repository.Edit(editVehicleModel);
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
            var vehicleModel = repository.FindById(id);
            var deleteModelView = Mapper.Map<VehicleModel, DeleteViewModel>(vehicleModel);
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
            try
            {
                repository.Delete(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
