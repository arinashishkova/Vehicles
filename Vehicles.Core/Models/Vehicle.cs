namespace Vehicles.Core
{
    public abstract class Vehicle
    {
        public string Brand { get; }
        public string Model { get; }
        public double Odometer { get; private set; }

        protected Vehicle(string brand, string model)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new ArgumentException("Brand is required.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Model is required.");
            }

            Brand = brand.Trim();
            Model = model.Trim();
            Odometer = 0;
        }

        public abstract string Move(double km);

        protected void AddKm(double km)
        {
            if (km <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(km),
                    "Distance must be greater than 0.");
            }

            Odometer += km;
        }

        // prettier format
        public string DisplayName => $"{Brand} {Model}";

        public virtual string Signal(Vehicle newcomer)
        {
            return $"{DisplayName} gives a signal to {newcomer.DisplayName}.";
        }

        public virtual string FlashLights(Vehicle leaving)
        {
            return $"{DisplayName} flashes lights as {leaving.DisplayName} leaves.";
        }

    }
}