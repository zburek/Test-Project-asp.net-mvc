using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class EditMakeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}