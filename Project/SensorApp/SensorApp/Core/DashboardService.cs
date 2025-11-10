using System;
using System.Data.SQLite;
using System.IO;

namespace SensorApp.Core
{
    public class DashboardService
    {
        private readonly string _dbPath;

        public DashboardService()
        {
            _dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "SensorData.db");
        }

        public void ShowDashboard()
        {
            if (!File.Exists(_dbPath))
            {
                Console.WriteLine("Database not found. Run the simulation first.");
                return;
            }

            try
            {
                using var connection = new SQLiteConnection($"Data Source={_dbPath}");
                connection.Open();

                Console.Clear();
                Console.WriteLine("=======================================");
                Console.WriteLine("          SENSOR DASHBOARD");
                Console.WriteLine("=======================================\n");

                int totalReadings = ExecuteScalarInt(connection, "SELECT COUNT(*) FROM SensorReadings;");
                Console.WriteLine($"Total Readings: {totalReadings}");

                double avgTemp = ExecuteScalarDouble(connection, "SELECT AVG(Value) FROM SensorReadings;");
                Console.WriteLine($"Average Temperature: {avgTemp:F2} °C");

                double minTemp = ExecuteScalarDouble(connection, "SELECT MIN(Value) FROM SensorReadings;");
                double maxTemp = ExecuteScalarDouble(connection, "SELECT MAX(Value) FROM SensorReadings;");
                Console.WriteLine($"Min Temperature: {minTemp:F2} °C");
                Console.WriteLine($"Max Temperature: {maxTemp:F2} °C");

                int anomalies = ExecuteScalarInt(connection, "SELECT COUNT(*) FROM SensorReadings WHERE IsAnomaly = 1;");
                Console.WriteLine($"Anomalies Detected: {anomalies}");

                string latest = ExecuteScalarString(connection, "SELECT Timestamp FROM SensorReadings ORDER BY Id DESC LIMIT 1;");
                Console.WriteLine($"Last Reading: {latest}");

                Console.WriteLine("\n=======================================");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading dashboard: " + ex.Message);
            }
        }

        private int ExecuteScalarInt(SQLiteConnection conn, string query)
        {
            using var cmd = new SQLiteCommand(query, conn);
            var result = cmd.ExecuteScalar();
            return result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        private double ExecuteScalarDouble(SQLiteConnection conn, string query)
        {
            using var cmd = new SQLiteCommand(query, conn);
            var result = cmd.ExecuteScalar();
            return result == DBNull.Value ? 0.0 : Convert.ToDouble(result);
        }

        private string ExecuteScalarString(SQLiteConnection conn, string query)
        {
            using var cmd = new SQLiteCommand(query, conn);
            var result = cmd.ExecuteScalar();
            return result == DBNull.Value ? "N/A" : result.ToString()!;
        }
    }
}
