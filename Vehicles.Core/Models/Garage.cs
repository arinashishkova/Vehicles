using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Vehicles.Core
{
    public class Garage<T> where T : Vehicle
    {
        public string Name { get; }

        public ObservableCollection<T> Items { get; }
            = new ObservableCollection<T>();

        public event EventHandler<GarageReactionEventArgs>? ReactionOccurred;

        public Garage(string name)
        {
            Name = name;
        }

        public void Add(T vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            foreach (T other in Items)
            {
                string message = other.Signal(vehicle);

                OnReactionOccurred(message);
            }

            Items.Add(vehicle);
        }

        public bool Remove(T vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            if (!Items.Remove(vehicle))
            {
                return false;
            }

            foreach (T remaining in Items)
            {
                string message = remaining.FlashLights(vehicle);

                OnReactionOccurred(message);
            }

            return true;
        }

        private void OnReactionOccurred(string message)
        {
            ReactionOccurred?.Invoke(
                this,
                new GarageReactionEventArgs(message));
        }
    }
}