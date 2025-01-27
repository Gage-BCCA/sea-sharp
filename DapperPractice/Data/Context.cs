using Dapper;
using DapperPractice.Data.DataModels;
using System.Data.SQLite;

namespace DapperPractice.Data
{
    class Context
    {
        private readonly String _connectionString = "DataSource=entries.sqlite";

        public void Initialize()
        {
            if (!File.Exists("entries.sqlite"))
            {

                using (var connection = new SQLiteConnection(_connectionString))
                {
                    var sql = @"
                        CREATE TABLE IF NOT EXISTS entries (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        language TEXT NOT NULL,
                        notes TEXT,
                        start_time TEXT,
                        end_time TEXT,
                        duration INT
                    );";
                    connection.Execute(sql);
                }
            }
        }

        public void InsertEntry(TimeEntry entry) {
            var sql = @"
                INSERT INTO entries (language, notes, start_time, end_time, duration)
                VALUES (@Language, @Notes, @StartTime, @EndTime, @Duration);
            ";
            var parameters = new { 
                Language = entry.Language, 
                Notes = entry.Notes,
                StartTime = entry.StartTime.ToString("yyyy-MM-dd HH:mm"),
                EndTime = entry.EndTime.ToString("yyyy-MM-dd HH:mm"),
                Duration = entry.CalculateDuration()
            };

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }

        public List<TimeEntry> FindEntryByLanguage(String language) {
            var sql = @"
            SELECT language, notes, start_time, end_time, duration
            FROM entries
            WHERE language = @Language COLLATE NOCASE
            ";

            var parameters = new {Language = language};
            var connection = new SQLiteConnection(_connectionString);
            return connection.Query<TimeEntry>(sql, parameters).ToList();
        }

    }
}