using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vehicles.Core;

namespace Vehicles.WpfApp
{
    public partial class MainWindow : Window
    {
        // collection for all vehicle types
        private ObservableCollection<Vehicle> vehicles =
            new ObservableCollection<Vehicle>();

        public MainWindow()
        {
            InitializeComponent();

            // visible in app vehicles
            VehicleTypeComboBox.Items.Add("Car");
            VehicleTypeComboBox.Items.Add("Boat");
            VehicleTypeComboBox.Items.Add("AmphibiousCar");
            VehicleTypeComboBox.SelectedIndex = 0;

            VehiclesListBox.ItemsSource = vehicles;

            UpdateActionButtons();
        }

        // add to collection
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string make = MakeTextBox.Text;
                string model = ModelTextBox.Text;

                Vehicle vehicle;

                // choose vehicle type to create
                switch (VehicleTypeComboBox.SelectedItem?.ToString())
                {
                    case "Car":
                        vehicle = new Car(make, model);
                        break;

                    case "Boat":
                        vehicle = new Boat(make, model);
                        break;

                    case "AmphibiousCar":
                        vehicle = new AmphibiousCar(make, model);
                        break;

                    default:
                        MessageBox.Show("Please select a vehicle type.");
                        return;
                }

                vehicles.Add(vehicle);

                LogListBox.Items.Add(
                    $"Added: {vehicle.DisplayName}");

                MakeTextBox.Clear();
                ModelTextBox.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VehiclesListBox_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            UpdateSelectedVehicleInfo();
            UpdateActionButtons();
        }

        // info about selected vehicle
        private void UpdateSelectedVehicleInfo()
        {
            if (VehiclesListBox.SelectedItem is not Vehicle vehicle)
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
            Vehicle? vehicle =
                VehiclesListBox.SelectedItem as Vehicle;

            MoveButton.IsEnabled =
                vehicle is not null;

            DriveButton.IsEnabled =
                vehicle is IDriveable;

            SwimButton.IsEnabled =
                vehicle is ISwimmable;
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
            if (VehiclesListBox.SelectedItem is not Vehicle vehicle)
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
            if (VehiclesListBox.SelectedItem is not IDriveable driveable)
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
            if (VehiclesListBox.SelectedItem is not ISwimmable swimmable)
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
    }
}