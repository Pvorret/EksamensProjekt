using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Model
{
    public class Time
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Day { get; set; }


        public Time(int id, DateTime startTime, DateTime endTime, string day)
        {
            this.ID = id;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Day = day;

        }
        public Time(DateTime startTime, DateTime endTime, string day)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Day = day;
        }

    }
}
