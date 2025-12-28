using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistent.models
{
    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
    }
}
