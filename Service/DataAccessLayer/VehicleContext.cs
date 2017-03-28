namespace Service.DataAccessLayer
{
    using System.Data.Entity;   

    public partial class VehicleContext : DbContext
    {
        public VehicleContext()
            : base("VehicleContext")
        {
        }
        public virtual DbSet<VehicleMake> VehicleMakes { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
