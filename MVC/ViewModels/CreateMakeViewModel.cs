using Service.DataAccessLayer;
using System.ComponentModel.DataAnnotations;


namespace MVC.ViewModels
{
    public class CreateMakeViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Abrv { get; set; }

        public static implicit operator CreateMakeViewModel(VehicleMake vehiclemake)
        {
            return new CreateMakeViewModel
            {
                Id = vehiclemake.Id,
                Name = vehiclemake.Name,
                Abrv = vehiclemake.Abrv
            };

        }

        public static implicit operator VehicleMake(CreateMakeViewModel viewModel)
        {
            return new VehicleMake
            {
                Name = viewModel.Name,
                Abrv = viewModel.Abrv
            };
        }

    }
}

