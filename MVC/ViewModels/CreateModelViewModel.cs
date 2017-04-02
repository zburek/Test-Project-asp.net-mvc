﻿using Service.DataAccessLayer;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class CreateModelViewModel
    {
        public int Id { get; set; }

        public int MakeId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Abrv { get; set; }

        private static IRepository<VehicleMake> repository = new VehicleMakeRepository();
        public List<VehicleMake> vehicleMakeList = repository.List;

        public static implicit operator CreateModelViewModel(VehicleModel vehiclemodel)
        {
            return new CreateModelViewModel
            {
                Id = vehiclemodel.Id,
                MakeId = vehiclemodel.MakeId,
                Name = vehiclemodel.Name,
                Abrv = vehiclemodel.Abrv
            };
        }
        public static implicit operator VehicleModel(CreateModelViewModel viewModel)
        {
            return new VehicleModel
            {
                MakeId = viewModel.MakeId,
                Name = viewModel.Name,
                Abrv = viewModel.Abrv
            };
        }
    }
}