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
            // TODO: Add validation here later
            Name = name;
            Location = location;
            MinValue = min;
            MaxValue = max;
        }
    }
}