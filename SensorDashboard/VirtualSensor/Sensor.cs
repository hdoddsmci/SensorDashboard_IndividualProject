namespace VirtualSensor
{
    public class Sensor
    {
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
    }
}