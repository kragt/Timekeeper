using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timekeeper.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public bool IsArchived { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class ProjectSlice
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ElapsedTime { get; set; }
    }
}
