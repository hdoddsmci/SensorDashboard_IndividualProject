namespace SensorApp.Core
{
    public class Sensor
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public double MinC { get; set; }
        public double MaxC { get; set; }
        public bool IsActive { get; set; } = true;
        public List<double> History { get; set; } = new();
        private static readonly Random _random = new();

        public double SimulateData()
        {
            return MinC + (_random.NextDouble() * (MaxC - MinC)) + (_random.NextDouble() * 0.3 - 0.15);
        }

        public bool ValidateData(double reading)
        {
            return reading >= MinC && reading <= MaxC;
        }

        public void StoreData(double reading)
        {
            History.Add(reading);
        }

        public bool DetectAnomaly(double reading)
        {
            if (History.Count < 3) return false;
            double avg = History.Average();
            return Math.Abs(reading - avg) > 2;
        }

        public void ShutdownSensor()
        {
            History.Clear();
            Console.WriteLine($"{Name} shut down at {DateTime.Now}");
        }
    }
}
