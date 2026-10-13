using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class Car : Vehicle, IDriveable
    {
        public Car(string brand, string model)
            : base(brand, model)
        {
        }

        public string Drive(double km)
        {
            AddKm(km);

            return $"{Brand} {Model} drove {km} km.";
        }

        public override string Move(double km)
        {
            return Drive(km);
        }
    }
}