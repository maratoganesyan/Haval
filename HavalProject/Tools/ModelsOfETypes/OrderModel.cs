using HavalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavalProject.Tools.ModelsOfETypes
{
    internal class OrderModel
    {
        #region CarFields
        public readonly string MarkOfCar = "Haval";
        public string ModelName;
        public string ConfigurationName;
        public string ColorName;
        public decimal PriceOfCar;
        #endregion

        #region ServicesFields
        public List<string> AdditationalServices;
        public decimal PriceOfAdditationalServices = 0M;
        #endregion

        #region Order
        public decimal TotalCost;
        public DateTime DateTimeModel;
        #endregion

        #region Manager
        public string NameOfManager;
        public string SurnameOfManager;
        #endregion

        #region Client
        public string NameOfClient;
        public string SurnameOfClient;
        #endregion

        public OrderModel(Order order)
        {
            ModelName = order.IdCarNavigation.IdModelNavigation.ModelName;
            ConfigurationName = order.IdCarNavigation.IdConfigurationNavigation.ConfigurationName;
            ColorName = order.IdCarNavigation.IdColorNavigation.ColorName;
            PriceOfCar = order.IdCarNavigation.ClientPrice;

            AdditationalServices = order.ServicesInOrders.Select(x => x.IdServiceNavigation.ServiceName).ToList();
            PriceOfAdditationalServices = order.ServicesInOrders.Select(x => x.IdServiceNavigation.ServicePrice).Sum();

            TotalCost = order.TotalPrice.Value;
            DateTimeModel = order.DateTimeOfOrder;

            NameOfManager = order.IdManagerNavigation.Name;
            SurnameOfManager = order.IdManagerNavigation.Surname;

            NameOfClient = order.IdClientNavigation.Name;
            SurnameOfClient = order.IdClientNavigation.Surname;
        }
    }
}
