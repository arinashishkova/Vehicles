using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class Car : Vehicle, IDriveable
    {
        // call vehicle constructor
        public Car(string make, string model)
            : base(make, model)
        {
        }

        // Drive changes odometr, return message
        public string Drive(double km)
        {
            AddKm(km);

            return $"{Make} {Model} drove {km} km.";
        }

        // polymorph movement for a Car
        public override string Move(double km)
        {
            return Drive(km);
        }
    }
}
