using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace SensorApp.Core
{
    public class SensorService
    {
        private readonly Sensor _sensor;
        private readonly int _intervalMs;
        private readonly int _smoothWindow;
        private readonly string _logPath;
        private readonly DatabaseService _db = new();

        public List<SensorData> History { get; } = new();

        public SensorService(Sensor sensor, int intervalMs, int smoothWindow, string logPath)
        {
            _sensor = sensor;
            _intervalMs = intervalMs;
            _smoothWindow = smoothWindow;
            _logPath = logPath;

            var dir = Path.GetDirectoryName(_logPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public void StartSensor(int iterations = 10)
        {
            for (int i = 0; i < iterations; i++)
            {
                double reading = _sensor.SimulateData();
                bool isValid = _sensor.ValidateData(reading);
                bool anomaly = _sensor.DetectAnomaly(reading);

                var data = new SensorData
                {
                    Timestamp = DateTime.Now,
                    ValueC = reading,
                    IsValid = isValid,
                    IsAnomaly = anomaly
                };

                _sensor.StoreData(reading);
                History.Add(data);

                LogData(data);

                double smoothed = SmoothData();
                Console.WriteLine($"Smoothed: {smoothed:F2} C");

                Thread.Sleep(_intervalMs);
            }
        }

        public void LogData(SensorData data)
        {
            string message = $"{data.Timestamp:yyyy-MM-dd HH:mm:ss} | {_sensor.Name} | {data.ValueC:F2} C | valid={data.IsValid} | anomaly={data.IsAnomaly}";
            Console.WriteLine(message);
            File.AppendAllText(_logPath, message + Environment.NewLine);

            _db.InsertReading(data, _sensor.Name);
        }

        public double SmoothData()
        {
            if (History.Count == 0) return 0;
            int window = Math.Min(_smoothWindow, History.Count);
            return History.TakeLast(window).Average(d => d.ValueC);
        }

        public void ShutdownSensor()
        {
            _sensor.ShutdownSensor();
        }
    }
}
