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

        public List<SensorData> History { get; } = new();

        public SensorService(Sensor sensor, int intervalMs = 1000, int smoothWindow = 5, string logPath = "logs/sensor.log")
        {
            _sensor = sensor;
            _intervalMs = intervalMs;
            _smoothWindow = smoothWindow;
            _logPath = logPath;

            var dir = Path.GetDirectoryName(_logPath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public void StartSensor(int iterations = 10)
        {
            for (int i = 0; i < iterations; i++)
            {
                var value = _sensor.SimulateData();
                var isValid = _sensor.ValidateData(value);

                _sensor.StoreData(value);
                var isAnomaly = _sensor.DetectAnomaly(value);

                var item = new SensorData
                {
                    Timestamp = DateTime.Now,
                    ValueC = value,
                    IsValid = isValid,
                    IsAnomaly = isAnomaly
                };

                History.Add(item);

                LogData(item);

                var smoothed = SmoothData();
                if (!double.IsNaN(smoothed))
                    Console.WriteLine($"Smoothed: {smoothed:F2} C");

                Thread.Sleep(_intervalMs);
            }
        }

        public void LogData(SensorData item)
        {
            var line = $"{item.Timestamp:yyyy-MM-dd HH:mm:ss} | {_sensor.Name} | {item.ValueC:F2} C | valid={item.IsValid} | anomaly={item.IsAnomaly}";
            Console.WriteLine(line);
            File.AppendAllText(_logPath, line + Environment.NewLine);
        }

        public double SmoothData()
        {
            if (_sensor.History.Count == 0) return double.NaN;
            int take = Math.Min(_smoothWindow, _sensor.History.Count);
            return _sensor.History.Skip(_sensor.History.Count - take).Average();
        }

        public void ShutdownSensor()
        {
            _sensor.ShutdownSensor();
        }
    }
}
