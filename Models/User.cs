using System;

namespace FileSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public User(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }

        public void SetId(int id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Surname}";
        }
    }
}

