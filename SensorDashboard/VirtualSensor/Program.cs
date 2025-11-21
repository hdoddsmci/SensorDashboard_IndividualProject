using System;
using System.IO;            
using System.Text.Json;     
using System.Threading;

namespace VirtualSensor
{
    // 1. Define what the JSON looks like so C# can read it
    public class SensorConfig
    {
        public string SensorName { get; set; }
        public string Location { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Virtual Temperature Sensor App ---");

            Sensor mySensor = new Sensor();
            string configFilePath = "sensor_config.json";

            // 2. Try to read the configuration from the file
            try
            {
                if (File.Exists(configFilePath))
                {
                    string jsonString = File.ReadAllText(configFilePath);
                    SensorConfig config = JsonSerializer.Deserialize<SensorConfig>(jsonString);

                    // Use the values from the file!
                    mySensor.InitialiseSensor(config.SensorName, config.Location, config.MinValue, config.MaxValue);
                    Console.WriteLine("Configuration loaded successfully from 'sensor_config.json'.");
                }
                else
                {
                    // Fallback if file is missing
                    Console.WriteLine("Config file not found. Using default values.");
                    mySensor.InitialiseSensor("DefaultSensor", "Unknown", 22, 24);
                }

                Console.WriteLine($"Sensor '{mySensor.Name}' initialized at {mySensor.Location}.");
                Console.WriteLine($"Target Range: {mySensor.MinValue}°C - {mySensor.MaxValue}°C");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing sensor: {ex.Message}");
                return;
            }

            Console.WriteLine("Press CTRL+C to stop.");
            Console.WriteLine("--------------------------------------------------");

            int counter = 0;

            // 3. Main Loop (Run forever)
            while (true)
            {
                counter++;

                // Inject Fault after 5 loops (approx 10 seconds)
                if (counter == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n*** SIMULATING HARDWARE FAILURE... ***\n");
                    mySensor.InjectFault();
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                double temp = mySensor.SimulateData();
                SensorData dataPoint = new SensorData { Value = temp, Timestamp = DateTime.Now };

                bool isThresholdBreached = mySensor.CheckThreshold(dataPoint);
                string status = "OK";

                if (isThresholdBreached)
                {
                    status = "CRITICAL (Threshold Exceeded)";
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    bool isValid = mySensor.ValidateData(dataPoint);
                    bool isAnomaly = mySensor.DetectAnomaly(dataPoint);

                    if (!isValid)
                    {
                        status = "WARNING (Out of Range)";
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (isAnomaly)
                    {
                        status += " | ANOMALY (Spike)";
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }

                mySensor.StoreData(dataPoint);
                Console.WriteLine($"[{dataPoint.Timestamp.ToLongTimeString()}] Temp: {dataPoint.Value}°C | Status: {status}");
                Console.ForegroundColor = ConsoleColor.Gray;

                double smoothed = mySensor.SmoothData();
                Console.WriteLine($"      -> Moving Average: {smoothed}°C");

                Thread.Sleep(2000);
            }
        }
    }
}