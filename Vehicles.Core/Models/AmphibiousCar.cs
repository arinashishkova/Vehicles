using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class AmphibiousCar : Car, ISwimmable
    {
        // calls Car constructor
        public AmphibiousCar(string make, string model)
            : base(make, model)
        {
        }

        // move in water
        public string Swim(double km)
        {
            AddKm(km);

            return $"{Make} {Model} swam {km} km.";
        }
    }
}