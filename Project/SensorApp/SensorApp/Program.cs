using System;
using SensorApp.Config;

try
{
    var cfg = ConfigLoader.Load("config.json");
    Console.WriteLine("Loaded configuration for " + cfg.SensorId + " at interval " + cfg.SampleIntervalMs + " ms.");
    // TODO: Start sensor simulation using cfg
}
catch (Exception ex)
{
    Console.Error.WriteLine("Configuration error: " + ex.Message);
}
