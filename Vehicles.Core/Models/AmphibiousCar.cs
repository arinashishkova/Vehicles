using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class AmphibiousCar : Car, ISwimmable
    {
        public AmphibiousCar(string brand, string model)
            : base(brand, model)
        {
        }

        public string Swim(double km)
        {
            AddKm(km);

            return $"{Brand} {Model} swam {km} km.";
        }
    }
}