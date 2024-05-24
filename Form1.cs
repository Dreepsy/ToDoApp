using System;
using System.Windows.Forms;

namespace ToDoListGUI
{
    public partial class Form1 : Form
    {
        private ToDoList todoList;

        public Form1()
        {
            InitializeComponent();
            todoList = new ToDoList();
            RefreshToDoList();
        }

        private void RefreshToDoList()
        {
            listBoxToDo.Items.Clear();
            foreach (var todo in todoList.GetToDoList())
            {
                listBoxToDo.Items.Add(todo);
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
