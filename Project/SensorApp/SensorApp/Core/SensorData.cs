using System;

namespace SensorApp.Core
{
    public class SensorData
    {
        public DateTime Timestamp { get; set; }
        public double ValueC { get; set; }
        public bool IsValid { get; set; }
        public bool IsAnomaly { get; set; }
    }
}
