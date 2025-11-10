using System;
using System.Data.SQLite;
using System.IO;

namespace SensorApp.Core
{
    public class DatabaseService
    {
        private readonly string _dbFolder;
        private readonly string _dbPath;

        public DatabaseService()
        {
            
            _dbFolder = Path.Combine(AppContext.BaseDirectory, "Data");
            if (!Directory.Exists(_dbFolder))
                Directory.CreateDirectory(_dbFolder);

     
            _dbPath = Path.Combine(_dbFolder, "SensorData.db");

          
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                using var connection = new SQLiteConnection($"Data Source={_dbPath}");
                connection.Open();

                string tableSql = @"CREATE TABLE IF NOT EXISTS SensorReadings (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Timestamp TEXT,
                                        SensorName TEXT,
                                        Value REAL,
                                        IsValid INTEGER,
                                        IsAnomaly INTEGER
                                    );";

                using var cmd = new SQLiteCommand(tableSql, connection);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Database initialized successfully: " + _dbPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database initialization failed: " + ex.Message);
            }
        }

        public void InsertReading(SensorData data, string sensorName)
        {
            try
            {
                using var connection = new SQLiteConnection($"Data Source={_dbPath}");
                connection.Open();

                string insertSql = @"INSERT INTO SensorReadings 
                                    (Timestamp, SensorName, Value, IsValid, IsAnomaly)
                                    VALUES (@time, @name, @val, @valid, @anom);";

                using var cmd = new SQLiteCommand(insertSql, connection);
                cmd.Parameters.AddWithValue("@time", data.Timestamp.ToString("s"));
                cmd.Parameters.AddWithValue("@name", sensorName);
                cmd.Parameters.AddWithValue("@val", data.ValueC);
                cmd.Parameters.AddWithValue("@valid", data.IsValid ? 1 : 0);
                cmd.Parameters.AddWithValue("@anom", data.IsAnomaly ? 1 : 0);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to insert reading: " + ex.Message);
            }
        }
    }
}
