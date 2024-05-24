using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

//define how a to-do looks like
public class ToDoItem
{
    public string Description {get; set;}
    public DateTime DueDate {get; set;}

    public ToDoItem(string description, DateTime dueDate)
    {
        Description = description;
        DueDate = dueDate;
    }

    //set formate for date check
    public override string ToString()
    {
        return $"{Description} (Fällig bis: {DueDate:dd.MM.yyyy})";
    }
}

    //generate to-do list
public class ToDoList
{
    private List<ToDoItem> todoList = new List<ToDoItem>();
    private string filepath = @"C:\Users\M02447\coding woche\ToDo.txt";
    private readonly object lockObject = new object();

    public ToDoList()
    {
        LoadFromToDoFile();
    }

    public List<ToDoItem> GetToDoList()
    {
        lock (lockObject)
        {
            return new List<ToDoItem>(todoList);
        }
    }

    //add to-do
    public void AddToDoItem(string description, DateTime dueDate)
    {
        lock (lockObject)
        {
            todoList.Add(new ToDoItem(description, dueDate));
        }
        SaveToToDoFile();

        if (description.Equals("Rick Roll", StringComparison.OrdinalIgnoreCase))
        {
            PlayRickRollVideo(); //see line 128
        }
    }

    //delete to-do
    public void DeleteToDoItem(int index)
    {
        lock (lockObject)
        {
            todoList.RemoveAt(index);
        }
        SaveToToDoFile();
    }

    //change to-do
    public void ChangeToDoItem(int index, string newDescription, DateTime newDueDate)
    {
        lock (lockObject)
        {
            var todo = todoList[index];
            todo.Description = newDescription;
            todo.DueDate = newDueDate;
        }
        SaveToToDoFile();
    }

    //write into .txt
    private void SaveToToDoFile()
    {
        lock (lockObject)
        {
            try
            {
                var lines = new List<string>();
                foreach (var item in todoList)
                {
                    lines.Add($"{item.Description}|{item.DueDate:dd.MM.yyyy}");
                }
                File.WriteAllLines(filepath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Schreiben der Datei: " + ex.Message);
            }
        }
    }

    //read from .txt
    private void LoadFromToDoFile()
    {
        lock (lockObject)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    var lines = File.ReadAllLines(filepath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2 && DateTime.TryParseExact(parts[1], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dueDate))
                        {
                            todoList.Add(new ToDoItem(parts[0], dueDate));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Laden der Datei: " + ex.Message);
            }
        }
    }

    //easter-egg
    private void PlayRickRollVideo()
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Abspielen des Videos" + ex);
        }
    }
}