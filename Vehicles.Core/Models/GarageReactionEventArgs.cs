namespace Vehicles.Core
{
    public class GarageReactionEventArgs : EventArgs
    {
        public string Message { get; }

        public GarageReactionEventArgs(string message)
        {
            Message = message;
        }
    }
}