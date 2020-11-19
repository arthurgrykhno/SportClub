using System;

namespace SportClub.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public int TrainerId { get; set; }

        public int TrainingId { get; set; }

        public int GroupId { get; set; }
    }
}
