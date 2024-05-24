using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoListGUI
{
    public partial class Form1 : Form
    {
        private ToDoList todoList;
        private Settings settings;

        public Form1(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            todoList = new ToDoList();
            LoadSettings();
            ApplySettings();
            RefreshToDoList();
        }

        private void LoadSettings()
        {
            textBoxUsername.Text = settings.Username;
            textBoxBackgroundImagePath.Text = settings.BackgroundImagePath;
        }

        private void ApplySettings()
        {
            if (!string.IsNullOrEmpty(settings.BackgroundImagePath) && System.IO.File.Exists(settings.BackgroundImagePath))
            {
                this.BackgroundImage = Image.FromFile(settings.BackgroundImagePath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void RefreshToDoList()
        {
            listBoxToDo.Items.Clear();
            foreach (var todo in todoList.GetToDoList())
            {
                listBoxToDo.Items.Add($"{settings.Username}: {todo}");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string description = textBoxDescription.Text;
            DateTime dueDate = dateTimePickerDueDate.Value;

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Das To-Do darf nicht leer sein.");
                return;
            }

            todoList.AddToDoItem(description, dueDate);
            RefreshToDoList();
            textBoxDescription.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxToDo.SelectedIndex != -1)
            {
                todoList.DeleteToDoItem(listBoxToDo.SelectedIndex);
                RefreshToDoList();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie ein To-Do zum Löschen aus.");
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (listBoxToDo.SelectedIndex != -1)
            {
                string newDescription = textBoxDescription.Text;
                DateTime newDueDate = dateTimePickerDueDate.Value;

                todoList.ChangeToDoItem(listBoxToDo.SelectedIndex, newDescription, newDueDate);
                RefreshToDoList();
                textBoxDescription.Clear();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie ein To-Do zum Ändern aus.");
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm(settings))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    LoadSettings();
                    ApplySettings();
                    RefreshToDoList();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
