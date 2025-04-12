namespace CodingTracker.Models
{
    internal class CodingSession
    {

        public int Id { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public TimeSpan duration { get; set; }

        public CodingSession(DateTime startingTime, DateTime endingTime)
        {
            startTime = startingTime;
            endTime = endingTime;
            CalculateDuration();
        }

        private void CalculateDuration()
        {
            duration = endTime - startTime;
        }
    }
}
