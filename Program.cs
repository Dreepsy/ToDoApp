using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

public class ToDoItem
{
    public string Description { get; set; }
    public DateTime DueDate { get; set; }

    public ToDoItem(string description, DateTime dueDate)
    {
        Description = description;
        DueDate = dueDate;
    }

    public override string ToString()
    {
        return $"{Description} (Fällig bis: {DueDate:dd.MM.yyyy})";
    }
}

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
            return todoList;
        }
    }

    public void AddToDo()
    {
        Console.Write("Bitte geben Sie ein neues To-Do ein: ");
        string description = Console.ReadLine();
        if (string.IsNullOrEmpty(description))
        {
            Console.WriteLine("Das To-Do darf nicht leer sein.");
            return;
        }

        Console.Write("Bitte geben Sie das Fälligkeitsdatum (dd.MM.yyyy) ein: ");
        if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dueDate))
        {
            lock (lockObject)
            {
                todoList.Add(new ToDoItem(description, dueDate));
            }
            Console.WriteLine("To-Do wurde hinzugefügt.");
            SaveToToDoFile();
        }
        else
        {
            Console.WriteLine("Ungültiges Datum.");
        }
    }

    public void DeleteToDo()
    {
        Console.Write("Geben Sie die Nummer des zu löschenden To-Do's an: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= todoList.Count)
        {
            lock (lockObject)
            {
                todoList.RemoveAt(index - 1);
            }
            Console.WriteLine("Das To-Do wurde gelöscht.");
            SaveToToDoFile();
        }
        else
        {
            Console.WriteLine("Ungültige Nummer.");
        }
    }

    public void PrintToDo()
    {
        if (todoList.Count == 0)
        {
            Console.WriteLine("Es gibt keine To-Dos.");
        }
        else
        {
            for (int i = 0; i < todoList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {todoList[i]}");
            }
        }
    }

    public void ChangeToDo()
    {
        Console.Write("Geben Sie die Zahl des ToDo's ein, welches Sie ändern möchten: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= todoList.Count)
        {
            var todo = todoList[index - 1];
            Console.Write($"Neuen Text für das ToDo ({todo.Description}) eingeben: ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDescription))
            {
                todo.Description = newDescription;
            }

            Console.Write($"Neues Fälligkeitsdatum für das ToDo ({todo.DueDate:dd.MM.yyyy}) eingeben (dd.MM.yyyy): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime newDueDate))
            {
                todo.DueDate = newDueDate;
            }
            else
            {
                Console.WriteLine("Ungültiges Datum, Fälligkeitsdatum bleibt unverändert.");
            }

            SaveToToDoFile();
            Console.WriteLine("Das To-Do wurde geändert.");
        }
        else
        {
            Console.WriteLine("Ungültige Nummer.");
        }
    }

    public void AddToDoItem(string description, DateTime dueDate)
    {

        lock (lockObject)
        {
            todoList.Add(new ToDoItem(description, dueDate));
        }
        SaveToToDoFile();

        if(description.Equals("Rick Roll", StringComparison.OrdinalIgnoreCase))
        {
            PlayRickRollVideo();
        }
        
    }

    private void PlayRickRollVideo()
    {
        try
        {
            Process.Start(new ProcessStartInfo{
                FileName = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Abspielen des Videos" +ex);
        }
    }

    public void DeleteToDoItem(int index)
    {
        lock (lockObject)
        {
            todoList.RemoveAt(index);
        }
        SaveToToDoFile();
    }

    public void ChangeToDoItem(int index, string newDescription, DateTime newDueDate)
    {
        var todo = todoList[index];
        todo.Description = newDescription;
        todo.DueDate = newDueDate;
        SaveToToDoFile();
    }
    
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
}

namespace ToDoListGUI
{
    class ToDoProgram
    {
        static void Main(string[] args)
        {
            try
            {
                ToDoList todolist = new ToDoList();

                Thread guiThread = new Thread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                });
                guiThread.Start();

                Thread consoleThread = new Thread(() =>
                {
                    ConsoleInput(todolist); // Übergabe der ToDoList-Instanz an die Konsolen-Eingabe-Methode
                });
                consoleThread.Start();

                guiThread.Join();
                consoleThread.Join(); // Warten darauf, dass der Konsolen-Eingabe-Thread beendet wird
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
            }
        }

        static void ConsoleInput(object obj)
        {
            ToDoList todolist = (ToDoList)obj;
        }

        static void Cleanup()
        {
            Console.WriteLine("Exiting application...");
            Thread.Sleep(2000);
        }
    }
}