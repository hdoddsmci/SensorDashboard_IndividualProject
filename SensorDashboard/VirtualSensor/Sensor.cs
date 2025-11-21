namespace VirtualSensor
{
    public class Sensor
    {
        private Random _rnd = new Random();
        public string Name { get; set; }
        public string Location { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        // This matches your assignment requirement: "InitialiseSensor"
        public void InitialiseSensor(string name, string location, double min, double max)
        {
            // Validation 1: Name cannot be empty
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Sensor name cannot be empty.");
            }

            // Validation 2: Min value cannot be greater than Max value
            if (min > max)
            {
                throw new ArgumentException("Minimum value cannot be greater than maximum value.");
            }

            Name = name;
            Location = location;
            MinValue = min;
            MaxValue = max;
        }
        public double SimulateData()
        {
            // This math generates a random number between Min and Max
            double nextVal = _rnd.NextDouble() * (MaxValue - MinValue) + MinValue;

            // Round it to 2 decimal places so it looks like a real temperature (e.g., 23.45)
            return Math.Round(nextVal, 2);
        }
    }
}