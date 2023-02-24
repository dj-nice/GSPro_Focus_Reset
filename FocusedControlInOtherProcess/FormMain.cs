using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FocusedControlInOtherProcess
{
    public partial class FormMain : Form
    {

       
        
       

        public FormMain()
        {
            InitializeComponent();
            Start();
                   
        }
        private static string AppName;
        private System.Windows.Forms.Timer timerUpdate;

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
           labelAppName.ForeColor = System.Drawing.Color.Red;
           labelAppName.Text = AppName;
           if (labelAppName.Text!="")
            {
                labelAppName.ForeColor = System.Drawing.Color.Green;
            }
           string fileName = tBAppName.Text;
           int fileExtPos = fileName.LastIndexOf(".");
           if (fileExtPos >= 0)
           fileName = fileName.Substring(0, fileExtPos);
           FocusProcess(fileName);
        }
        private static void FocusProcess(string fileName)
        {
            AppName = fileName;
            IntPtr hWnd; 
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == AppName)
                {
                    //AppName = pr.ProcessName;
                    hWnd = pr.MainWindowHandle; //use it as IntPtr not int
                    //ShowWindow(hWnd, 3);
                    SetForegroundWindow(hWnd);
                }
            }
        }
        private void Start()
        {
            int x = 3000;
            Int32.TryParse(tBInterval.Text, out x);
            timerUpdate.Interval = x;
            timerUpdate.Enabled = true;
            timerUpdate.Tick += new System.EventHandler(this.TimerUpdate_Tick);
            timerUpdate.Start();
            buttonStart.Enabled = false;

        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
            
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timerUpdate.Enabled = false;
            labelAppName.ForeColor = System.Drawing.Color.Red;
            buttonStart.Enabled = true;
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
    }
}
