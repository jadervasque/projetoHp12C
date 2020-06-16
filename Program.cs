using System;
using System.Diagnostics;
using System.Windows.Forms;
using HP12C.Messages;

namespace HP12C
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                ShowMessage.Info("O programa já está em execução!");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    Application.Run(new FrmHP12C());
                }
                catch (Exception ex)
                {
                    ShowMessage.Erro(ex);
                }
            }
        }
    }
}
