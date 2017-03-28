using Service.DataAccessLayer;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class EditModelViewModel
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Abrv { get; set; }

        private static IVehicleMakeList vehicleServices = new VehicleServices();
        public List<VehicleMake> vehicleMakeList = vehicleServices.MakeList();
    }

    
}