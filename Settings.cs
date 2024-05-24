using System;
using System.IO;
using System.Text.Json;

namespace ToDoListGUI
{
    public class Settings
    {
        public string Username { get; set; } = "DefaultUser";
        public string BackgroundImagePath { get; set; } = "";

        private string settingsFilePath = "settings.json";

        public Settings()
        {
            Load();
        }

        public void Save()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(this);
                File.WriteAllText(settingsFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Speichern der Einstellungen: " + ex.Message);
            }
        }

        private void Load()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                {
                    string jsonString = File.ReadAllText(settingsFilePath);
                    var settings = JsonSerializer.Deserialize<Settings>(jsonString);
                    if (settings != null)
                    {
                        Username = settings.Username;
                        BackgroundImagePath = settings.BackgroundImagePath;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Laden der Einstellungen: " + ex.Message);
            }
        }
    }
}
