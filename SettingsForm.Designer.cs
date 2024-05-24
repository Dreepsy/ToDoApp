namespace ToDoListGUI
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxBackgroundImagePath;
        private System.Windows.Forms.Label labelBackgroundImagePath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxBackgroundImagePath = new System.Windows.Forms.TextBox();
            this.labelBackgroundImagePath = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(12, 29);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(260, 20);
            this.textBoxUsername.TabIndex = 0;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(12, 13);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(75, 13);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "Benutzername:";
            // 
            // textBoxBackgroundImagePath
            // 
            this.textBoxBackgroundImagePath.Location = new System.Drawing.Point(12, 68);
            this.textBoxBackgroundImagePath.Name = "textBoxBackgroundImagePath";
            this.textBoxBackgroundImagePath.Size = new System.Drawing.Size(179, 20);
            this.textBoxBackgroundImagePath.TabIndex = 2;
            // 
            // labelBackgroundImagePath
            // 
            this.labelBackgroundImagePath.AutoSize = true;
            this.labelBackgroundImagePath.Location = new System.Drawing.Point(12, 52);
            this.labelBackgroundImagePath.Name = "labelBackgroundImagePath";
            this.labelBackgroundImagePath.Size = new System.Drawing.Size(105, 13);
            this.labelBackgroundImagePath.TabIndex = 3;
            this.labelBackgroundImagePath.Text = "Hintergrundbild Pfad:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(197, 66);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "Durchsuchen";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(197, 97);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Speichern";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 132);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelBackgroundImagePath);
            this.Controls.Add(this.textBoxBackgroundImagePath);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
