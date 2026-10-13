using System.Windows;
using System.Windows.Controls;
using Vehicles.Core;

namespace Vehicles.WpfApp
{
    public partial class MainWindow : Window
    {
        // typed garages
        private readonly Garage<Car> carGarage =
            new Garage<Car>("Cars garage");

        private readonly Garage<Boat> boatGarage =
            new Garage<Boat>("Boats garage");

        public MainWindow()
        {
            InitializeComponent();

            VehicleTypeComboBox.Items.Add("Car");
            VehicleTypeComboBox.Items.Add("Boat");
            VehicleTypeComboBox.Items.Add("AmphibiousCar");
            VehicleTypeComboBox.SelectedIndex = 0;

            CarsListBox.ItemsSource = carGarage.Items;
            BoatsListBox.ItemsSource = boatGarage.Items;

            carGarage.ReactionOccurred += Garage_ReactionOccurred;
            boatGarage.ReactionOccurred += Garage_ReactionOccurred;

            UpdateActionButtons();
        }

        private void Garage_ReactionOccurred(
            object? sender,
            GarageReactionEventArgs e)
        {
            LogListBox.Items.Add(e.Message);
        }

        // add vehicle
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string brand = BrandTextBox.Text;
                string model = ModelTextBox.Text;

                Vehicle vehicle;

                switch (VehicleTypeComboBox.SelectedItem?.ToString())
                {
                    case "Car":
                        Car car = new Car(brand, model);
                        carGarage.Add(car);
                        vehicle = car;
                        break;

                    case "Boat":
                        Boat boat = new Boat(brand, model);
                        boatGarage.Add(boat);
                        vehicle = boat;
                        break;

                    case "AmphibiousCar":
                        AmphibiousCar amphibiousCar =
                            new AmphibiousCar(brand, model);

                        carGarage.Add(amphibiousCar);
                        vehicle = amphibiousCar;
                        break;

                    default:
                        MessageBox.Show("Please select a vehicle type.");
                        return;
                }

                LogListBox.Items.Add(
                    $"Added: {vehicle.DisplayName}");

                BrandTextBox.Clear();
                ModelTextBox.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GarageListBox_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (sender == CarsListBox &&
                CarsListBox.SelectedItem is not null)
            {
                BoatsListBox.SelectedItem = null;
            }

            if (sender == BoatsListBox &&
                BoatsListBox.SelectedItem is not null)
            {
                CarsListBox.SelectedItem = null;
            }

            UpdateSelectedVehicleInfo();
            UpdateActionButtons();
        }

        private Vehicle? GetSelectedVehicle()
        {
            return CarsListBox.SelectedItem as Vehicle
                ?? BoatsListBox.SelectedItem as Vehicle;
        }

        // info about selected vehicle
        private void UpdateSelectedVehicleInfo()
        {
            Vehicle? vehicle = GetSelectedVehicle();

            if (vehicle is null)
            {
                SelectedVehicleTextBlock.Text =
                    "No vehicle selected";

                return;
            }

            SelectedVehicleTextBlock.Text =
                $"{vehicle.DisplayName} | Odometer: {vehicle.Odometer} km";
        }

        // buttons enabling
        private void UpdateActionButtons()
        {
            Vehicle? vehicle = GetSelectedVehicle();

            MoveButton.IsEnabled = vehicle is not null;

            DriveButton.IsEnabled = vehicle is IDriveable;

            SwimButton.IsEnabled = vehicle is ISwimmable;

            RemoveCarButton.IsEnabled = CarsListBox.SelectedItem is Car;

            RemoveBoatButton.IsEnabled = BoatsListBox.SelectedItem is Boat;
        }

        private bool TryGetDistance(out double km)
        {
            if (!double.TryParse(DistanceTextBox.Text, out km))
            {
                MessageBox.Show("Please enter a valid distance.");
                return false;
            }

            return true;
        }

        private void MoveButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            Vehicle? vehicle = GetSelectedVehicle();

            if (vehicle is null)
            {
                MessageBox.Show("Please select a vehicle.");
                return;
            }

            if (!TryGetDistance(out double km))
            {
                return;
            }

            try
            {
                // car or boat
                string message = vehicle.Move(km);

                LogListBox.Items.Add(message);

                UpdateSelectedVehicleInfo();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DriveButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (GetSelectedVehicle() is not IDriveable driveable)
            {
                MessageBox.Show("This vehicle cannot drive.");
                return;
            }

            if (!TryGetDistance(out double km))
            {
                return;
            }

            try
            {
                // IDriveable objects can use Drive()
                string message = driveable.Drive(km);

                LogListBox.Items.Add(message);

                UpdateSelectedVehicleInfo();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SwimButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (GetSelectedVehicle() is not ISwimmable swimmable)
            {
                MessageBox.Show("This vehicle cannot swim.");
                return;
            }

            if (!TryGetDistance(out double km))
            {
                return;
            }

            try
            {
                // ISwimmable objects can use Swim()
                string message = swimmable.Swim(km);

                LogListBox.Items.Add(message);

                UpdateSelectedVehicleInfo();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveCarButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (CarsListBox.SelectedItem is not Car car)
            {
                MessageBox.Show("Please select a car.");
                return;
            }

            if (carGarage.Remove(car))
            {
                LogListBox.Items.Add(
                    $"Removed: {car.DisplayName}");
            }

            UpdateSelectedVehicleInfo();
            UpdateActionButtons();
        }

        private void RemoveBoatButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (BoatsListBox.SelectedItem is not Boat boat)
            {
                MessageBox.Show("Please select a boat.");
                return;
            }

            if (boatGarage.Remove(boat))
            {
                LogListBox.Items.Add(
                    $"Removed: {boat.DisplayName}");
            }

            UpdateSelectedVehicleInfo();
            UpdateActionButtons();
        }
    }
}