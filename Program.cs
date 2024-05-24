using System;
using System.Threading;
using System.Windows.Forms;

namespace ToDoListGUI
{
    class ToDoProgram
    {
        static void Main(string[] args)
        {
            try
            {
                Thread guiThread = new Thread(() =>
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                });
                guiThread.Start();
                guiThread.Join(); // Warten darauf, dass der GUI-Thread beendet wird
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
            }
        }
    }
}
