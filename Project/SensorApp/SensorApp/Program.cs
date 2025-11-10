using System;
using SensorApp.Config;
using SensorApp.Core;

class Program
{
    static void Main()
    {
        try
        {
            
            var cfg = ConfigLoader.Load("config.json");

            
            var sensor = new Sensor
            {
                Name = cfg.SensorId,
                Location = "Server Room",
                MinC = cfg.MinC,
                MaxC = cfg.MaxC
            };

            Console.WriteLine($"Starting sensor {sensor.Name}");


            var service = new SensorService(sensor, cfg.SampleIntervalMs, smoothWindow: 5, logPath: "logs/sensor.log");
            service.StartSensor(iterations: 10);
            service.ShutdownSensor();

            var dashboard = new DashboardService();
            dashboard.ShowDashboard();

            Console.WriteLine("Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
