using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystem.Models;

namespace FileSystem
{
    class Program
    {
        static public string PathToFile;

        static void Main(string[] args)
        { 
            string directoryPath = "/Users/ihormolchan/Desktop/C#_Projects/FileSystem/FileSystem/DB";
            string fileName = "MyTextFile.txt";
            PathToFile = Path.Combine(directoryPath, fileName);
            FileService fileService = new FileService(PathToFile);

            if (!File.Exists(fileService.filePath))
            {
                fileService.CreateFile();
            }

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("\nChoose option\n1 - Add\n2 - Delete\n3 - Get all users\n4 - Exit\n---------------------\n");
                int input = 0;

                try
                {
                    input = int.Parse(Console.ReadLine());
                    if (input >= 5 || input <= 0)
                    {
                        Console.WriteLine("This command is not exist");
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("This command is not exist");
                }

                switch (input)
                {
                    case 1:
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter surname: ");
                        string surname = Console.ReadLine();

                        User newUser = new User(name, surname);

                        fileService.SaveToFile(newUser);
                        Console.WriteLine("Success");
                        break;
                    case 2:
                        Console.Write("Enter user ID: ");
                        int id = int.Parse(Console.ReadLine());

                        try
                        {
                            fileService.DeleteFromFile(id);
                            Console.WriteLine("Success");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"User with id: {id} is not exist");
                        }
                        break;
                    case 3:
                        try
                        {
                            List<User> users = fileService.ReadAll();

                            foreach (var item in users)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("File is empty yet");
                            Console.WriteLine("Try to add some data with 1 command");
                        }

                        break;
                    case 4:
                        isWork = false;
                        break;
                }
            }
        }
    }
}

