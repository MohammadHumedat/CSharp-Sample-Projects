using StudentAssistant.DataLayer;
using StudentAssistent.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentAssistent.Business_Logic_Layer
{
    // Event
    public class TaskDueEventArgs : EventArgs
    {
        public models.Task Task { get; set; }
    }

    // Delegate 
    public delegate void TaskDueHandler(object sender, TaskDueEventArgs e);

    // Student Service 
    public class StudentService
    {
        private DataManager dataManager;
        private string currentUsername;
        private UserData currentUserData;

        // Event 
        public event TaskDueHandler TaskDueSoon;

        public StudentService()
        {
            dataManager = new DataManager();
        }

        // User Management

        public bool Register(string username, string password, string fullName, string email)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                FullName = fullName,
                Email = email
            };

            bool saved = dataManager.SaveUser(user);

            if (saved)
            {
                // create empty data for a new user
                UserData emptyData = new UserData
                {
                    Username = username,
                    Tasks = new List<models.Task>(),
                    Notes = new List<Note>(),
                    Contacts = new List<Contact>()
                };
                dataManager.SaveUserData(username, emptyData);
            }

            return saved;
        }

        public bool Login(string username, string password)
        {
            if (dataManager.ValidateLogin(username, password))
            {
                currentUsername = username;
                currentUserData = dataManager.LoadUserData(username);
                CheckDueTasks(); // check the tasks list when user login
                return true;
            }
            return false;
        }

        public void Logout()
        {
            if (currentUserData != null && !string.IsNullOrEmpty(currentUsername))
            {
              
                dataManager.SaveUserData(currentUsername, currentUserData);
            }

            currentUsername = null;
            currentUserData = null;
        }

        public User GetCurrentUser()
        {
            if (string.IsNullOrEmpty(currentUsername))
                return null;

            return dataManager.GetUserInfo(currentUsername);
        }

        // Task Management

        public void AddTask(models.Task task)
        {
            currentUserData.Tasks.Add(task);
            Save();
        }

        public void UpdateTask(models.Task task)
        {
            var existing = currentUserData.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existing != null)
            {
                int index = currentUserData.Tasks.IndexOf(existing);
                currentUserData.Tasks[index] = task;
                Save();
            }
        }

        public void DeleteTask(Guid taskId)
        {
            currentUserData.Tasks.RemoveAll(t => t.Id == taskId);
            Save();
        }

        public List<models.Task> GetTasks()
        {
            return currentUserData?.Tasks ?? new List<models.Task>();
        }

        // Note Management 

        public void AddNote(Note note)
        {
            currentUserData.Notes.Add(note);
            Save();
        }

        public void UpdateNote(Note note)
        {
            var existing = currentUserData.Notes.FirstOrDefault(n => n.Id == note.Id);
            if (existing != null)
            {
                int index = currentUserData.Notes.IndexOf(existing);
                currentUserData.Notes[index] = note;
                Save();
            }
        }

        public void DeleteNote(Guid noteId)
        {
            currentUserData.Notes.RemoveAll(n => n.Id == noteId);
            Save();
        }

        public List<Note> GetNotes()
        {
            return currentUserData?.Notes ?? new List<Note>();
        }

        //  Contact Management

        public void AddContact(Contact contact)
        {
            currentUserData.Contacts.Add(contact);
            Save();
        }

        public void UpdateContact(Contact contact)
        {
            var existing = currentUserData.Contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existing != null)
            {
                int index = currentUserData.Contacts.IndexOf(existing);
                currentUserData.Contacts[index] = contact;
                Save();
            }
        }

        public void DeleteContact(Guid contactId)
        {
            currentUserData.Contacts.RemoveAll(c => c.Id == contactId);
            Save();
        }

        public List<Contact> GetContacts()
        {
            return currentUserData?.Contacts ?? new List<Contact>();
        }

        //  Helper Methods 

        private void Save()
        {
            
            if (!string.IsNullOrEmpty(currentUsername) && currentUserData != null)
            {
                dataManager.SaveUserData(currentUsername, currentUserData);
            }
        }
        public int GetTotalItemCount()
{
    if (currentUserData == null)
        return 0;
    
    return currentUserData.Tasks.Count + 
           currentUserData.Notes.Count + 
           currentUserData.Contacts.Count;
}
        public void CheckDueTasks()
        {
            if (currentUserData == null)
                return;

            // to avoid notification multpile times.
            var tasksToWarn = new List<models.Task>();

            foreach (var task in currentUserData.Tasks)
            {
                // ignor complete tasks
                if (task.Status == models.TaskStatus.Completed)
                    continue;

                double daysLeft = (task.EndDate - DateTime.Now).TotalDays;

                // notification if less or equal one day
                if (daysLeft <= 1 && daysLeft >= 0)
                {
                    tasksToWarn.Add(task);
                }
            }

            // for each task its own notification
            foreach (var task in tasksToWarn)
            {
                TaskDueSoon?.Invoke(this, new TaskDueEventArgs { Task = task });// event invocation
            }
        }
    }
}