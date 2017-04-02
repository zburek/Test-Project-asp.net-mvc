using Service.DataAccessLayer;
using Service.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class EditModelViewModel
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Abrv { get; set; }

        private static IRepository<VehicleMake> repository = new VehicleMakeRepository();
        public List<VehicleMake> vehicleMakeList = repository.List;
    }  
}