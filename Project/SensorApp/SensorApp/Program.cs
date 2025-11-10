using System;
using SensorApp.Config;
using SensorApp.Core;

class Program
{
    static void Main()
    {
        try
        {
            // Load config file
            var cfg = ConfigLoader.Load("config.json");

            // Initialise sensor
            var sensor = new Sensor
            {
                Name = cfg.SensorId,
                Location = "Server Room",
                MinC = cfg.MinC,
                MaxC = cfg.MaxC
            };

            Console.WriteLine($"Starting sensor {sensor.Name}");

            // Start sensor service
            var service = new SensorService(sensor, cfg.SampleIntervalMs, smoothWindow: 5, logPath: "logs/sensor.log");
            service.StartSensor(iterations: 10);
            service.ShutdownSensor();

            Console.WriteLine("Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
