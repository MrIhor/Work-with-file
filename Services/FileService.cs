using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystem.Models;
using Newtonsoft.Json;

namespace FileSystem
{
    public class FileService
    {
        public string filePath { get; set; }

        public FileService(string file)
        {
            this.filePath = file;
        }

        public void CreateFile()
        {
            var StreamFile = File.Create(filePath);
            StreamFile.Close();
        }

        public void SaveToFile(User user)
        {
            List<User> allCurrentUsers = ReadAll();

            if (allCurrentUsers == null)
            {
                List<User> newUser = new List<User>();
                int lastId = 1;
                user.SetId(lastId);
                newUser.Add(user);
                string json = JsonConvert.SerializeObject(newUser, Formatting.Indented);

                File.WriteAllText(filePath, json);
            }
            else
            {
                int getLastId = allCurrentUsers.Last().Id;
                user.SetId(getLastId + 1);
                allCurrentUsers.Add(user);
                string json = JsonConvert.SerializeObject(allCurrentUsers, Formatting.Indented);

                File.WriteAllText(filePath, json);
            }
        }

        public void SaveToFile(List<User> users)
        {
            string sereilizedUsers = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, sereilizedUsers);
        }

        public void DeleteFromFile(int id)
        {
            List<User> allCurrentUsers = ReadAll();
            if (allCurrentUsers.Count == 1)
            {
                var streamFile = File.Create(filePath);
                streamFile.Close();
            }
            else
            {
                User userToDelete = allCurrentUsers.First(x => x.Id == id);

                allCurrentUsers.Remove(userToDelete);
                SaveToFile(allCurrentUsers);
            }
        }

        public List<User> ReadAll()
        {
            string text = File.ReadAllText(filePath);
            List<User> result = JsonConvert.DeserializeObject<List<User>>(text);

            return result;
        }
    }
}

