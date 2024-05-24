using System;
using System.Threading;
using System.Windows.Forms;

namespace ToDoListGUI
{
    class ToDoProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var settings = new Settings();
                var form = new Form1(settings);
                Application.Run(form);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
            }
        }
    }
}
