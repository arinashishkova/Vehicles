using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Core
{
    public class Boat : Vehicle, ISwimmable
    {
        // calls vehicle constructor
        public Boat(string make, string model)
            : base(make, model)
        {
        }

        // swimming chanfes odometr and returns message
        public string Swim(double km)
        {
            AddKm(km);
            return $"{Make} {Model} swam {km} km.";
        }

        // polymorphic movement for a boat
        public override string Move(double km)
        {
            return Swim(km);
        }
    }
}
