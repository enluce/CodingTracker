using Spectre.Console;
using static CodingTracker.Enums;

namespace CodingTracker
{
    internal class Validation
    {
        public static bool UpdateValidation(string time, int recordID, TimeType timeType)
        {
            string command;
            string startTime = "";
            string endTime = "";
            if (timeType == TimeType.Start)
            {
                command = $"SELECT endTime from coding_sessions where ID = {recordID}";
                endTime = DatabaseManager.Scalar(command);
                if (DateTime.Parse(endTime) <= DateTime.Parse(time))
                {
                    return false;
                }
                UpdateDuration(DateTime.Parse(time), DateTime.Parse(endTime), recordID);



            }
            else
            {
                command = $"SELECT startTime from coding_sessions where ID = {recordID}";
                startTime = DatabaseManager.Scalar(command);
                if (DateTime.Parse(time) <= DateTime.Parse(startTime))
                {
                    return false;
                }
                UpdateDuration(DateTime.Parse(startTime), DateTime.Parse(time), recordID);

            }
            return true;
        }

        public static bool IDValidation(int id)
        {
            string command = "SELECT COUNT(*) from coding_sessions where ID = @ID";

            string rowsUpdated = DatabaseManager.Scalar(command, new { ID = id });
            if (int.Parse(rowsUpdated) == 0)
            {
                return false;
            }
            return true;
        }

        private static void UpdateDuration(DateTime startTime, DateTime endTime, int recordID)
        {
            TimeSpan duration = endTime - startTime;

            string command = "UPDATE coding_sessions SET duration = @Duration where ID = @RecordID";

            DatabaseManager.NonQuery(command, new { Duration = duration , RecordID = recordID});

        }
    }
}
