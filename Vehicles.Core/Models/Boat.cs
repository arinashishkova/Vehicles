using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class Boat : Vehicle, ISwimmable
    {
        public Boat(string brand, string model)
            : base(brand, model)
        {
        }

        public string Swim(double km)
        {
            AddKm(km);

            return $"{Brand} {Model} swam {km} km.";
        }

        public override string Move(double km)
        {
            return Swim(km);
        }
    }
}