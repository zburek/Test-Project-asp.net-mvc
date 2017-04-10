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
        IRepository<VehicleMake> repository = new VehicleMakeRepository();
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
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentFilter = searchString;

            var vehicleMakeList = repository.IndexList(sortOrder, searchString, pageNumber, pageSize);
            var indexViewModel = Mapper.Map<IEnumerable<IndexViewModel>>(vehicleMakeList);
            var pagedIndexViewModel = new StaticPagedList<IndexViewModel>(indexViewModel, vehicleMakeList.GetMetaData());
            return View(pagedIndexViewModel);
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
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Add(makeViewMake);
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
            var vehicleMakeList = repository.List;
            var vehicleMake = repository.FindById(id);
            var editMakeView = Mapper.Map<VehicleMake, EditMakeViewModel>(vehicleMake);
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
            try
            {
                if (ModelState.IsValid)
                {
                    var editVehicleMake = Mapper.Map<EditMakeViewModel, VehicleMake>(editMakeView);
                    repository.Edit(editVehicleMake);
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
            var vehicleMake = repository.FindById(id);
            var deleteMakeView = Mapper.Map<VehicleMake, DeleteViewModel>(vehicleMake);
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
