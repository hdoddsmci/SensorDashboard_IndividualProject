using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualSensor
{
    public class SensorData
    {
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }

    }

    public class Sensor
    {
        private Random _rnd = new Random();
        public List<SensorData> History { get; private set; } = new List<SensorData>();
        public string Name { get; set; }
        public string Location { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double SmoothData()
        {
            // If we have no history, we can't smooth anything.
            if (History.Count == 0) return 0;

            // If we have less than 3 records, just return the latest one (not enough to average)
            if (History.Count < 3)
            {
                return History.Last().Value;
            }

            // Take the last 3 readings and calculate the average
            var lastThree = History.Skip(History.Count - 3).Take(3);
            double average = lastThree.Average(d => d.Value);

            return Math.Round(average, 2);
        }
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
        public bool ValidateData(SensorData data)
        {
            if (data == null) return false;
            return (data.Value >= MinValue && data.Value <= MaxValue);
        }

        public void StoreData(SensorData data)
        {
            if (data != null)
            {
                History.Add(data);
            }
        }

        public void LogData(SensorData data)
        {
            // Simple console logging for now
            Console.WriteLine($"[{data.Timestamp}] Sensor '{Name}' ({Location}): {data.Value}°C");
        }

        public bool DetectAnomaly(SensorData data)
        {
            // If we don't have enough history to compare against, it's not an anomaly yet.
            if (History.Count < 3) return false;

            // Get the average of the previous readings
            double recentAverage = SmoothData();

            // Calculate the difference (absolute value ignores negative signs)
            double difference = Math.Abs(data.Value - recentAverage);

            // If the difference is greater than 5 degrees, it's an anomaly (spike)
            if (difference > 5.0)
            {
                return true;
            }

            return false;
        }

        public void ShutdownSensor()
        {
            History.Clear();
            Console.WriteLine($"Sensor '{Name}' has been shut down and history cleared.");
        }
    }
}