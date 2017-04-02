using Service.DataAccessLayer;

namespace MVC.ViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        public int MakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }    
}
