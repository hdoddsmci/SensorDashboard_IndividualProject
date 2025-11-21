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

        [Fact]
        public void SmoothData_ShouldReturnAverageOfLastThreeValues()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 10, 30);

            // Add 3 specific data points
            sensor.StoreData(new SensorData { Value = 22 });
            sensor.StoreData(new SensorData { Value = 24 });
            sensor.StoreData(new SensorData { Value = 23 });

            // Act
            double result = sensor.SmoothData();

            // Assert
            // The average of 22, 24, and 23 is exactly 23
            Assert.Equal(23, result);
        }

        [Fact]
        public void DetectAnomaly_ShouldReturnTrue_WhenValueIsSpike()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 10, 30);

            // Fill history with steady data (Average is 20)
            sensor.StoreData(new SensorData { Value = 20 });
            sensor.StoreData(new SensorData { Value = 20 });
            sensor.StoreData(new SensorData { Value = 20 });

            // Act
            // 30 is 10 degrees away from 20. That is a spike!
            var spikeData = new SensorData { Value = 30 };
            bool isAnomaly = sensor.DetectAnomaly(spikeData);

            // Assert
            Assert.True(isAnomaly);
        }

        [Fact]
        public void ShutdownSensor_ShouldClearHistory()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 10, 30);
            sensor.StoreData(new SensorData { Value = 25 }); // Add some data

            // Act
            sensor.ShutdownSensor();

            // Assert
            // The history should be empty after shutdown
            Assert.Empty(sensor.History);
        }

        [Fact]
        public void InjectFault_ShouldForceHighValues()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 20, 25);

            // Act
            sensor.InjectFault(); // Break the sensor
            double result = sensor.SimulateData();

            // Assert
            // Result should be > 45 (because we set fault values to 45+)
            Assert.True(result >= 45);
        }

        [Fact]
        public void CheckThreshold_ShouldReturnTrue_WhenValueExceedsMax()
        {
            // Arrange
            var sensor = new Sensor();
            sensor.InitialiseSensor("Test", "Room", 20, 25);
            var badData = new SensorData { Value = 30 }; // 30 is > Max (25)

            // Act
            bool alert = sensor.CheckThreshold(badData);

            // Assert
            Assert.True(alert);
        }

    }
}