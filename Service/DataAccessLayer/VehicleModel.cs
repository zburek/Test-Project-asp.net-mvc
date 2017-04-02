namespace Service.DataAccessLayer
{
    using Services;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("VehicleModel")]
    public partial class VehicleModel : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("VehicleMake")]
        public int MakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
    }
}
