using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusedControlInOtherProcess
{
    public partial class FormMain : Form
    {
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private static string AppName;
        int interval = 3000;
        string interval2 = "";
        int dfinterval = 3000;
        string dfAppName = "GSPro.exe";

        public FormMain()
        {
            InitializeComponent();
            FillTextBox();
            interval = int.TryParse(tBInterval.Text, out interval) ? interval : dfinterval;
            Start();
        }

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        //static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        //static readonly IntPtr HWND_TOP = new IntPtr(0);
        //static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
      //const UInt32 SWP_NOZORDER = 0x0004;
      //const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x00010;
      //const UInt32 SWP_FRAMECHANGED = 0x00020;
        const UInt32 SWP_SHOWWINDOW = 0x00040;
      //const UInt32 SWP_HIDEWINDOW = 0x00080;
      //const UInt32 SWP_NOCOPYBITS = 0x000100;
      //const UInt32 SWP_NOOWNERZORDER = 0x000200;
      //const UInt32 SWP_NOSENDCHANGING = 0x000400;
      //const UInt32 SWP_DEFERERASE = 0x0002000;
      //const UInt32 SWP_ASYNCWINDOWPOS = 0x0004000;

    //SWP_NOSIZE verhindert, dass das Fenster eine neue Größe bekommt.cx und cy sind dann irrelevant und können auf 0 gesetzt werden.
    //SWP_NOMOVE verhindert, dass das Fenster verschoben wird.x und y sind dann irrelevant und können auf 0 gesetzt werden.
    //SWP_NOZORDER verhindert, dass die Z-Order-Position verändert wird.
    //SWP_NOREDRAW verhindert, dass irgendetwas automatisch neu gezeichnet wird. Das betrifft sowohl das Fenster selbst, aber auch alle verdeckten Fenster werden nicht invalidiert.
    //SWP_NOACTIVATE verhindert, dass das Fenster den Fokus erhält.
    //SWP_FRAMECHANGED wird benutzt um Änderungen der SetWindowLong-Funktion anzuwenden. Sendet eine WM_NCCALCSIZE-Nachricht an das Fenster.
    //SWP_SHOWWINDOW sorgt dafür, dass das Fenster sichtbar wird.Entspricht.Show() bzw. den Ändern der Visible-Eigenschaft.
    //SWP_HIDEWINDOW sorgt dafür, dass das Fenster unsichtbar wird.Entspricht.Hide() bzw. den Ändern der Visible-Eigenschaft.
    //SWP_NOCOPYBITS verwirft den kompletten dargestellten Fensterinhalt und sorgt so für ein vollständiges Neuzeichnen.
    //SWP_NOOWNERZORDER verschiebt nicht das besitzende Fenster in der Z-Order.
    //SWP_NOSENDCHANGING verhindert, dass das Fenster die WM_WINDOWPOSCHANGING-Nachricht erhält
    //SWP_DEFERERASE verhindert, dass das Fenster die WM_SYNCPAINT-Nachricht erhält
    //SWP_ASYNCWINDOWPOS verhindert das der aufrufende Thread durch den Thread, der das Fenster verarbeitet, blockiert werden kann.


    [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        // Old Way < V14
        // [DllImport("user32.dll")]
        //  public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);
        
        //[DllImport("User32.dll")]
        //private static extern int ShowWindow(int hwnd, int nCmdShow);
        
        // Win32 API Constants for ShowWindowAsync()
        //private const int SW_HIDE = 0;
        //private const int SW_SHOWNORMAL = 1;
        //private const int SW_NORMAL = 1;
        //private const int SW_SHOWMINIMIZED = 2;
        //private const int SW_SHOWMAXIMIZED = 3;
        //private const int SW_MAXIMIZE = 3;
        //private const int SW_SHOWNOACTIVATE = 4;
        //private const int SW_SHOW = 5;
        //private const int SW_MINIMIZE = 6;
        //private const int SW_SHOWMINNOACTIVE = 7;
        //private const int SW_SHOWNA = 8;
          private const int SW_RESTORE = 9;
        //private const int SW_SHOWDEFAULT = 10;
        //private const int SW_FORCEMINIMIZE = 11;
        //private const int SW_MAX = 11;

        private void Start()
        {
            AppName = tBAppName.Text;
            int fileExtPos = AppName.LastIndexOf(".");
            if (fileExtPos >= 0)
                AppName = AppName.Substring(0, fileExtPos);
            if (tBAppName.Text != "")
            {
                labelAppName.ForeColor = Color.Red;
                labelAppName.Text = AppName;
            }
            try
            {
                bg2.RunWorkerAsync();
                buttonStart.Enabled = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void bg2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (!bg2.CancellationPending)
            {
                try
                {
                    FocusProcess(AppName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n\n" + ex.Message, "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Thread.Sleep(interval);
            }
            e.Cancel = true;
        }
        private void FocusProcess(string fileName)
        {
            AppName = fileName;
            IntPtr hWnd;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName.ToLower() == AppName.ToLower())
                {
                    hWnd = pr.MainWindowHandle; //use it as IntPtr not int

                    if (rB_onTop.Checked)
                    {
                        //V14:
                        // Old Way 1st: ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTORE);
                        // Old Way 2nd: SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                        //New Way: 
                        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOACTIVATE + SWP_SHOWWINDOW + SWP_NOSIZE + SWP_NOMOVE);
                    }
                        if(rB_Focus.Checked) 
                    {
                        SetForegroundWindow(hWnd);
                        //V14:
                        // Old Way 1st: ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTORE);
                        // Old Way 2nd: ActivateWindow(hWnd);
                        // New Way:
                        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_SHOWWINDOW + SWP_NOSIZE + SWP_NOMOVE);
                    }
                    labelAppName.BeginInvoke(new Action(() => { labelAppName.ForeColor = System.Drawing.Color.Green; ; }));
                    
                }
            }
        }

        //Solution found: https://stackoverflow.com/questions/10740346/setforegroundwindow-only-working-while-visual-studio-is-open
        private const int ALT = 0xA4;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const uint Restore = 9;

        // Old Way < V14
        //[DllImport("user32.dll")]
        //private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        // Old Way < V14
        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool IsIconic(IntPtr hWnd);
        // Old Way < V14
        //[DllImport("user32.dll")]
        //private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        // Old Way < V14
        //[DllImport("user32.dll")]
        //private static extern IntPtr GetForegroundWindow();
        
        // Old Way < V14
        //public static void ActivateWindow(IntPtr mainWindowHandle)
        //{
        //    //check if already has focus
        //    if (mainWindowHandle == GetForegroundWindow()) return;
        //    //check if window is minimized
        //    if (IsIconic(mainWindowHandle))
        //    {
        //        ShowWindow(mainWindowHandle, Restore);
            
        //    }
        //    // Simulate a key press
        //    keybd_event(0, 0, 0, 0);
        //    SetForegroundWindow(mainWindowHandle);
        //}
        private void buttonStart_Click(object sender, EventArgs e)
        {
            setConfig();
            Start();
            buttonStop.Focus();
        }

        private async void buttonStop_Click(object sender, EventArgs e)
        {
            bg2.CancelAsync();
            bg2.Dispose();
            labelAppName.ForeColor = Color.Red;
            buttonStart.Text = "Wait " + (interval / 1000).ToString() + " sec.";
            await Task.Delay(interval);
            buttonStart.Text = "Start";
            buttonStart.Enabled = true;
            buttonStart.Focus();
        }

        private void buttonZoom_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == true) infozoom(true);
            else infozoom(false);
        }

        private void infozoom(bool zoom)
        {
            if (zoom == false)
            {
                panel1.Visible = true;
                this.Width = 460;
                this.Height = 220;
                button3.Text = "<<";
            }
            else
            {
                panel1.Visible = false;
                this.Width = 246;
                this.Height = 185;
                button3.Text = ">>";
            }
        }
        private void setConfig()
        {
            //make changes
            config.AppSettings.Settings["AppName"].Value = tBAppName.Text;
            config.AppSettings.Settings["Interval"].Value = tBInterval.Text;
            if (rB_Focus.Checked)
            {
                config.AppSettings.Settings["forceFocus"].Value = "true";
            }
            else 
            { 
                config.AppSettings.Settings["forceFocus"].Value = "false";
            }
            //save to apply changes
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void FillTextBox()
        {
            if (ConfigurationManager.AppSettings["AppName"] != null)
            {
                AppName = ConfigurationManager.AppSettings["AppName"];
                tBAppName.Text = AppName;
            }
            else
            {
                tBAppName.Text = dfAppName;
            }
            if (ConfigurationManager.AppSettings["interval"] != null)
            {
                interval2 = ConfigurationManager.AppSettings["interval"];
                tBInterval.Text = interval2;
            }
            else
            {
                tBInterval.Text = dfinterval.ToString();
            }
            if (ConfigurationManager.AppSettings["forceFocus"] == "true")
            {
                rB_Focus.Checked= true;
            }
            else
            {
                rB_onTop.Checked = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tBInterval.Text = dfinterval.ToString();
            tBAppName.Text = dfAppName;
            tBAppName.Update();
            tBInterval.Update();
            setConfig();
        }

        private void tBInterval_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(tBInterval.Text, out _))
            {
                MessageBox.Show("Only numbers please!" + Environment.NewLine + "set default interval = 3000");
                tBInterval.Text = dfinterval.ToString();
            }
            setConfig();
            interval2 = tBInterval.Text;
            interval = int.TryParse(tBInterval.Text, out interval) ? interval : dfinterval;
        }

        private void tBAppName_Leave(object sender, EventArgs e)
        {
            setConfig();
        }

        private void tBInterval_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tBAppName.Focus();
            }
        }

        private void tBAppName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tBInterval.Focus();
            }
        }

        private void rB_Focus_CheckedChanged(object sender, EventArgs e)
        {
            setConfig();
        }

        private void rB_onTop_CheckedChanged(object sender, EventArgs e)
        {
            setConfig();
        }
    }
}
