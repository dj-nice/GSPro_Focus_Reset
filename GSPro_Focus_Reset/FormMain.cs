using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

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


        

       

       
        

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);
        // Win32 API Import
        [DllImport("User32.dll")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        // Win32 API Constants for ShowWindowAsync()
        //private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_NORMAL = 1;
        //private const int SW_SHOWMINIMIZED = 2;
        //private const int SW_SHOWMAXIMIZED = 3;
        //private const int SW_MAXIMIZE = 3;
        //private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
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
                if (pr.ProcessName == AppName)
                {
                    hWnd = pr.MainWindowHandle; //use it as IntPtr not int
                    SetForegroundWindow(hWnd);
                    ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTORE);
                    labelAppName.BeginInvoke(new Action(() => { labelAppName.ForeColor = System.Drawing.Color.Green; ; }));
                    ActivateWindow(hWnd);
                }
            }
        }


        //Solution found: https://stackoverflow.com/questions/10740346/setforegroundwindow-only-working-while-visual-studio-is-open
        private const int ALT = 0xA4;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const uint Restore = 9;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        public static void ActivateWindow(IntPtr mainWindowHandle)
        {
            //check if already has focus
            if (mainWindowHandle == GetForegroundWindow()) return;

            //check if window is minimized
            if (IsIconic(mainWindowHandle))
            {
                ShowWindow(mainWindowHandle, Restore);
            }

            // Simulate a key press
            keybd_event(0, 0, 0, 0);

            SetForegroundWindow(mainWindowHandle);
        }
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
                     
            buttonStart.Text = "Wait "+(interval/1000).ToString()+" sec.";
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
                this.Width = 445;
                button3.Text = "<<";
            }
            else
            {
                panel1.Visible = false;
                this.Width = 235;
                button3.Text = ">>";
            }
        }
        private void setConfig()
        {
            //make changes
            config.AppSettings.Settings["AppName"].Value = tBAppName.Text;
            config.AppSettings.Settings["Interval"].Value = tBInterval.Text;

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
                MessageBox.Show("Only numbers please!"+Environment.NewLine+"set default interval = 3000");
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
    }
}
