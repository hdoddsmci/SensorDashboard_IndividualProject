using System;
using System.IO;
using System.Text.Json;

namespace SensorApp.Config
{
    public static class ConfigLoader
    {
        public static SensorConfig Load(string path = "config.json")
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Config file not found: {path}");

            var json = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            var cfg = JsonSerializer.Deserialize<SensorConfig>(json, options)
                      ?? throw new Exception("Could not read config");

            cfg.Validate();
            return cfg;
        }
    }
}
