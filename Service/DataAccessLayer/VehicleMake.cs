namespace Service.DataAccessLayer
{
    using Services;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("VehicleMake")]
    public partial class VehicleMake :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
