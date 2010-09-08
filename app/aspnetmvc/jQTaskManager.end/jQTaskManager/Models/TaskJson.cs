using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace jQTaskManager.Models
{
    public class TaskJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public string Priority { get; set; }

        public static TaskJson DeSerialize(string json)
        {
            return new JavaScriptSerializer().Deserialize<TaskJson>(json);
        }
    }
}