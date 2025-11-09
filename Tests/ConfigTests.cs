using System;
using System.Text.Json;
using SensorApp.Config;
using Xunit;

public class ConfigTests
{
    [Fact]
    public void Valid_config_deserializes_and_validates()
    {
        var json = @"{ \"SensorId\": \"t-1\", \"SampleIntervalMs\": 500, \"MinC\": 20, \"MaxC\": 25, \"SmoothingWindow\": 3 }";
        var cfg = JsonSerializer.Deserialize<SensorConfig>(json)!;
        cfg.Validate();
        Assert.Equal("t-1", cfg.SensorId);
        Assert.Equal(500, cfg.SampleIntervalMs);
    }

    [Fact]
    public void MinC_greater_than_MaxC_throws()
    {
        var json = @"{ \"SensorId\": \"t-1\", \"SampleIntervalMs\": 500, \"MinC\": 30, \"MaxC\": 25 }";
        var cfg = JsonSerializer.Deserialize<SensorConfig>(json)!;
        Assert.Throws<ArgumentException>(() => cfg.Validate());
    }
}
