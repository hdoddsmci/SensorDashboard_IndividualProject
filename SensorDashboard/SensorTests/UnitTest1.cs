using Xunit;
using VirtualSensor; // This lets us use the Sensor class
using System;

namespace SensorTests
{
    public class SensorUnitTests
    {
        [Fact]
        public void InitialiseSensor_ShouldSetValues_WhenInputIsValid()
        {
            // Arrange
            var sensor = new Sensor();
            string expectedName = "ServerRoomSensor";
            double expectedMin = 18;
            double expectedMax = 26;

            // Act
            sensor.InitialiseSensor(expectedName, "Room 101", expectedMin, expectedMax);

            // Assert
            Assert.Equal(expectedName, sensor.Name);
            Assert.Equal(expectedMin, sensor.MinValue);
        }

        [Fact]
        public void InitialiseSensor_ShouldThrowError_WhenMinIsGreaterThanMax()
        {
            // Arrange
            var sensor = new Sensor();

            // Act & Assert
            // We expect this to fail because Min (50) is bigger than Max (20)
            Assert.Throws<ArgumentException>(() =>
                sensor.InitialiseSensor("BadSensor", "Room 101", 50, 20)
            );
        }
    }
}