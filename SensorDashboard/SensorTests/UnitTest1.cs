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
        [Fact]
        public void SimulateData_ShouldReturnValuesWithinRange()
        {
            // Arrange
            var sensor = new Sensor();
            double min = 20;
            double max = 30;
            sensor.InitialiseSensor("TestSensor", "Lab", min, max);

            // Act
            double result = sensor.SimulateData();

            // Assert
            // This checks if the result is actually between 20 and 30
            Assert.InRange(result, min, max);
        }

        [Fact]
        public void ValidateData_ShouldReturnTrue_WhenValueIsWithinRange()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 10, 30);
            var data = new SensorData { Value = 20 }; // 20 is safe (between 10 and 30)

            // Act
            bool result = sensor.ValidateData(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateData_ShouldReturnFalse_WhenValueIsOutOfRange()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 10, 30);
            var data = new SensorData { Value = 50 }; // 50 is too high!

            // Act
            bool result = sensor.ValidateData(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void StoreData_ShouldAddToHistory_WhenDataIsValid()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 10, 30);
            var data = new SensorData { Value = 25 };

            // Act
            sensor.StoreData(data);

            // Assert
            // The list count should be 1 because we added one item
            Assert.Single(sensor.History);
            Assert.Equal(25, sensor.History[0].Value);
        }
    }
}