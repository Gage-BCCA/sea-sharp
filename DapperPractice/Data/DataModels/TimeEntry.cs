namespace DapperPractice.Data.DataModels
{
    class TimeEntry
    {
        public String Language { get; set; }
        public String Notes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? Duration { get; set;}

        // Constructor for User Provided Information
        public TimeEntry(
            String language,
            String notes,
            String startTime,
            String endTime
        )
        {
            Language = language;
            Notes = notes;
            StartTime = Convert.ToDateTime(startTime);
            EndTime = Convert.ToDateTime(endTime);
        }

        // Default Constructor for Dapper
        public TimeEntry() {}

        public int CalculateDuration()
        {
            return (int)EndTime.Subtract(StartTime).TotalMinutes;
        }
    }
}