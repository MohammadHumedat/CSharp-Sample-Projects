using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistent.models
{
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
