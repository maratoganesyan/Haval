using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Car
    {
        public Car()
        {
            CarPhoto = new HashSet<CarPhoto>();
            GoodsInSupply = new HashSet<GoodsInSupply>();
            Order = new HashSet<Order>();
        }

        public int IdCar { get; set; }
        public int IdModel { get; set; }
        public int IdEngine { get; set; }
        public int IdBody { get; set; }
        public int IdCountry { get; set; }
        public int IdColor { get; set; }
        public int IdConfiguration { get; set; }
        public int IdCarStatus { get; set; }
        public int IdYear { get; set; }
        public int IdRudderSide { get; set; }
        public int IdDriveType { get; set; }
        public int IdTransmission { get; set; }
        public int IdEngineType { get; set; }
        public decimal ClientPrice { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public decimal EngineCapacity { get; set; }
        public int TankCapacity { get; set; }
        public int Quantity { get; set; }

        public virtual Body IdBodyNavigation { get; set; }
        public virtual CarStatus IdCarStatusNavigation { get; set; }
        public virtual Color IdColorNavigation { get; set; }
        public virtual Configuration IdConfigurationNavigation { get; set; }
        public virtual Country IdCountryNavigation { get; set; }
        public virtual DriveType IdDriveTypeNavigation { get; set; }
        public virtual Engine IdEngineNavigation { get; set; }
        public virtual EngineType IdEngineTypeNavigation { get; set; }
        public virtual Models IdModelNavigation { get; set; }
        public virtual RudderSide IdRudderSideNavigation { get; set; }
        public virtual Transmission IdTransmissionNavigation { get; set; }
        public virtual Year IdYearNavigation { get; set; }
        public virtual ICollection<CarPhoto> CarPhoto { get; set; }
        public virtual ICollection<GoodsInSupply> GoodsInSupply { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
