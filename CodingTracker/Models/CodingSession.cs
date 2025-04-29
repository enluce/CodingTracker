namespace CodingTracker.Models
{
    internal class CodingSession
    {

        public int Id { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }

        public CodingSession() { } //required for dapper
        public CodingSession(string startingTime, string endingTime)
        {
            StartTime = DateTime.Parse(startingTime).ToString("T");
            EndTime = DateTime.Parse(endingTime).ToString("T");
            Date = DateTime.Parse(endingTime).ToString("D");
            CalculateDuration(DateTime.Parse(startingTime), DateTime.Parse(endingTime));
        }

        private void CalculateDuration(DateTime startingTime, DateTime endingTime)
        {
            TimeSpan durationDT = endingTime - startingTime;

            Duration = durationDT.ToString();
        }
    }
}
