using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistent.models
{
    public class Note
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
