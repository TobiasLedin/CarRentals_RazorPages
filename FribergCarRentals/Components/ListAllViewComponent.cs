﻿using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Components
{
    [ViewComponent]
    public class ListAll : ViewComponent   // TODO: Ändra till "ListAll" efter felsökning?
    {

        private readonly IVehicleRepository _vehicleRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly ICustomerRepository _customerRepo;

        public ListAll(IVehicleRepository vehicleRepo, IBookingRepository bookingRepo, ICustomerRepository customerRepo)
        {
            _vehicleRepo = vehicleRepo;
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
            Lists = new ListAllVM();
        }

        public ListAllVM Lists { get; set; }

        // type = Model to list.
        // user = Customize data and options according admin/customer.
        public IViewComponentResult Invoke(string type, string user)
        {
            if (type == "Vehicle")
            {
                Lists.Vehicles = _vehicleRepo.GetAll().ToList();
            }
            else if (type == "Booking")
            {
                Lists.Bookings = _bookingRepo.GetAll().ToList(); // TODO: Null return problem när VehicleId tagits bort!
            }
            else if (type == "Customer")
            {
                Lists.Customers = _customerRepo.GetAll().ToList();
            }
            ViewData["type"] = type;
            ViewData["user"] = user;
            return View(Lists);

        }

    }
}