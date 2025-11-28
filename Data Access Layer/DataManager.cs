
using StudentAssistent.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StudentAssistant.DataLayer
{
    public class DataManager
    {
        private readonly string txtFilePath;
        private readonly string jsonFilePath;

        public DataManager()
        {
            // The Data Folder Path 
            string dataFolder = @"C:\Users\humed\source\repos\.NET Projects\StudentAssistent\StudentAssistent\Data";


            // if the folder doesnt Exists => create new one
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            txtFilePath = Path.Combine(dataFolder, "FileData.txt");
            jsonFilePath = Path.Combine(dataFolder, "JsonData.json");

            // if the files doesnt exist, then create the files
            if (!File.Exists(txtFilePath))
                File.WriteAllText(txtFilePath, "");

            if (!File.Exists(jsonFilePath))
                File.WriteAllText(jsonFilePath, "{}");
        }

        // txt file operations
        public bool SaveUser(User user) // Method to save user registration data
        {
            if (UserExists(user.Username))
                return false;

            string line = $"{user.Username}|{user.Password}|{user.FullName}|{user.Email}";
            File.AppendAllText(txtFilePath, line + Environment.NewLine);
            return true;
        }

       
        public bool ValidateLogin(string username, string password) // check loging validation
        {
            if (!File.Exists(txtFilePath))
                return false;

            var lines = File.ReadAllLines(txtFilePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');
                if (parts.Length >= 2)
                {
                    if (parts[0].Trim() == username && parts[1].Trim() == password)
                        return true;
                }
            }
            return false;
        }

       
        public bool UserExists(string username) // check if the user exist or no
        {
            if (!File.Exists(txtFilePath))
                return false;

            var lines = File.ReadAllLines(txtFilePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');
                if (parts.Length >= 1 && parts[0].Trim() == username)
                    return true;
            }
            return false;
        }

       
        public User GetUserInfo(string username) // Get the user data
        {
            if (!File.Exists(txtFilePath))
                return null;

            var lines = File.ReadAllLines(txtFilePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');
                if (parts.Length >= 4 && parts[0].Trim() == username)
                {
                    return new User
                    {
                        Username = parts[0].Trim(),
                        Password = parts[1].Trim(),
                        FullName = parts[2].Trim(),
                        Email = parts[3].Trim()
                    };
                }
            }
            return null;
        }

        //  JSON File Operations 

        
        public void SaveAllUsersData(Dictionary<string, UserData> allData) // save user data
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string json = JsonSerializer.Serialize(allData, options);
                File.WriteAllText(jsonFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON: {ex.Message}");
            }
        }

       
        public Dictionary<string, UserData> LoadAllUsersData()
        {
            try
            {
                if (!File.Exists(jsonFilePath))
                    return new Dictionary<string, UserData>();

                string json = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrWhiteSpace(json) || json == "{}")
                    return new Dictionary<string, UserData>();

                var data = JsonSerializer.Deserialize<Dictionary<string, UserData>>(json);
                return data ?? new Dictionary<string, UserData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON: {ex.Message}");
                return new Dictionary<string, UserData>();
            }
        }

       
        public void SaveUserData(string username, UserData userData) // save a single user data
        {
            var allData = LoadAllUsersData();
            allData[username] = userData;
            SaveAllUsersData(allData);
            
        }

        
        public UserData LoadUserData(string username) // Load a single user data
        {
            var allData = LoadAllUsersData();

            if (allData.ContainsKey(username))
                return allData[username];

            // if it doesnt exist, return a empty data
            return new UserData
            {
                Username = username,
                Tasks = new List<StudentAssistent.models.Task>(),
                Notes = new List<Note>(),
                Contacts = new List<Contact>()
            };
            
        }

        
        public bool DeleteUserData(string username) // delete a user data.
        {
            try
            {
                var allData = LoadAllUsersData();
                if (allData.ContainsKey(username))
                {
                    allData.Remove(username);
                    SaveAllUsersData(allData); // re-saving data without the removes one.
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user data: {ex.Message}");
                return false;
            }
        }

        
        public List<string> GetAllUsernames() // get all user names from txt file
        {
            var usernames = new List<string>();

            if (!File.Exists(txtFilePath))
                return usernames;

            var lines = File.ReadAllLines(txtFilePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split('|');
                if (parts.Length >= 1)
                    usernames.Add(parts[0].Trim());
            }

            return usernames;
        }
    }
}