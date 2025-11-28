using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistent.models
{
    public class UserData
    {
        public string Username { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
