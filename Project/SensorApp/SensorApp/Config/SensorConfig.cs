using System;

namespace SensorApp.Config
{
    public class SensorConfig
    {
        public string SensorId { get; set; } = "temp-1";
        public int SampleIntervalMs { get; set; } = 1000;
        public double MinC { get; set; } = 22.0;
        public double MaxC { get; set; } = 24.0;
        public int SmoothingWindow { get; set; } = 5;
        public double AnomalyStdDev { get; set; } = 3.0;
        public bool LogToFile { get; set; } = true;
        public string LogFilePath { get; set; } = "logs/sensor.log";

        public void Validate()
        {
            if (SampleIntervalMs <= 0) throw new ArgumentException("SampleIntervalMs must be positive");
            if (MinC >= MaxC) throw new ArgumentException("MinC must be less than MaxC");
            if (SmoothingWindow < 1) throw new ArgumentException("SmoothingWindow must be at least 1");
            if (string.IsNullOrWhiteSpace(SensorId)) throw new ArgumentException("SensorId is required");
        }
    }
}
