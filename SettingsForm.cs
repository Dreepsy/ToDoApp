using System;
using System.Windows.Forms;

namespace ToDoListGUI
{
    public partial class SettingsForm : Form
    {
        private Settings settings;

        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            textBoxUsername.Text = settings.Username;
            textBoxBackgroundImagePath.Text = settings.BackgroundImagePath;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            settings.Username = textBoxUsername.Text;
            settings.BackgroundImagePath = textBoxBackgroundImagePath.Text;
            settings.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxBackgroundImagePath.Text = openFileDialog.FileName;
                }
            }
        }
    }
}
