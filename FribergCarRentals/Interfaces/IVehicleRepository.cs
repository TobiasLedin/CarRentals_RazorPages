﻿using FribergCarRentals.Models;

namespace FribergCarRentals.Interfaces
{
    public interface IVehicleRepository
    {
        void Create(Vehicle vehicle);
        Vehicle GetById(int id);
        List<Vehicle> GetAll();
        void Update(Vehicle vehicle);
        void Delete(int id);
    }
}
