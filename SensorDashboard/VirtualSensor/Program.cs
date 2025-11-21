using System;
using System.Threading;

namespace VirtualSensor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Virtual Temperature Sensor App ---");

            // 1. Create and Initialise the Sensor
            Sensor mySensor = new Sensor();

            try
            {
                mySensor.InitialiseSensor("Server-Room-01", "Rack 4B", 21, 25);
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

            // 2. Main Loop
            while (true)
            {
                counter++;

                // --- FAULT INJECTION LOGIC ---
                // After 5 loops (approx 10 seconds), break the sensor!
                if (counter == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n*** SIMULATING HARDWARE FAILURE... ***\n");
                    mySensor.InjectFault();
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                // -----------------------------

                // A. Simulate Data
                double temp = mySensor.SimulateData();
                SensorData dataPoint = new SensorData { Value = temp, Timestamp = DateTime.Now };

                // B. Check Status (Using our new CheckThreshold logic)
                bool isThresholdBreached = mySensor.CheckThreshold(dataPoint);

                string status = "OK";

                if (isThresholdBreached)
                {
                    status = "CRITICAL (Threshold Exceeded)";
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    // If it's not critical, check for simple validity or anomalies
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

                // C. Log to Console
                mySensor.StoreData(dataPoint);
                Console.WriteLine($"[{dataPoint.Timestamp.ToLongTimeString()}] Temp: {dataPoint.Value}°C | Status: {status}");

                // Reset color
                Console.ForegroundColor = ConsoleColor.Gray;

                // D. Show Average
                double smoothed = mySensor.SmoothData(); // <--- This is the only place we define 'smoothed' now!
                Console.WriteLine($"      -> Moving Average: {smoothed}°C");

                // Pause
                Thread.Sleep(2000);
            }
        }
    }
}