namespace FocusedControlInOtherProcess
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelHeader = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.tBInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tBAppName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rB_Focus = new System.Windows.Forms.RadioButton();
            this.rB_onTop = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelAppName = new System.Windows.Forms.Label();
            this.bg2 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(12, 29);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(205, 20);
            this.labelHeader.TabIndex = 10;
            this.labelHeader.Text = "set foreground app:";
            this.labelHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.SystemColors.Control;
            this.buttonStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonStart.Location = new System.Drawing.Point(104, 111);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(90, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.SystemColors.Control;
            this.buttonStop.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.buttonStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.ForeColor = System.Drawing.Color.Red;
            this.buttonStop.Location = new System.Drawing.Point(8, 111);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(90, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // tBInterval
            // 
            this.tBInterval.Location = new System.Drawing.Point(6, 55);
            this.tBInterval.Name = "tBInterval";
            this.tBInterval.Size = new System.Drawing.Size(71, 20);
            this.tBInterval.TabIndex = 3;
            this.tBInterval.Text = "3000";
            this.tBInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBInterval_KeyDown);
            this.tBInterval.Leave += new System.EventHandler(this.tBInterval_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Interval in ms:";
            // 
            // tBAppName
            // 
            this.tBAppName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tBAppName.Location = new System.Drawing.Point(8, 98);
            this.tBAppName.Name = "tBAppName";
            this.tBAppName.Size = new System.Drawing.Size(163, 20);
            this.tBAppName.TabIndex = 5;
            this.tBAppName.Text = "GSPro.exe";
            this.tBAppName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBAppName_KeyDown);
            this.tBAppName.Leave += new System.EventHandler(this.tBAppName_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "*.exe name of app to focus";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(198, 111);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rB_Focus);
            this.panel1.Controls.Add(this.rB_onTop);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tBInterval);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tBAppName);
            this.panel1.Location = new System.Drawing.Point(243, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 175);
            this.panel1.TabIndex = 8;
            this.panel1.Visible = false;
            // 
            // rB_Focus
            // 
            this.rB_Focus.AutoSize = true;
            this.rB_Focus.Location = new System.Drawing.Point(8, 148);
            this.rB_Focus.Name = "rB_Focus";
            this.rB_Focus.Size = new System.Drawing.Size(185, 17);
            this.rB_Focus.TabIndex = 10;
            this.rB_Focus.TabStop = true;
            this.rB_Focus.Text = "Force App stay on top + get focus";
            this.rB_Focus.UseVisualStyleBackColor = true;
            this.rB_Focus.CheckedChanged += new System.EventHandler(this.rB_Focus_CheckedChanged);
            // 
            // rB_onTop
            // 
            this.rB_onTop.AutoSize = true;
            this.rB_onTop.Checked = true;
            this.rB_onTop.Location = new System.Drawing.Point(8, 125);
            this.rB_onTop.Name = "rB_onTop";
            this.rB_onTop.Size = new System.Drawing.Size(129, 17);
            this.rB_onTop.TabIndex = 9;
            this.rB_onTop.TabStop = true;
            this.rB_onTop.Text = "Force App stay on top";
            this.rB_onTop.UseVisualStyleBackColor = true;
            this.rB_onTop.CheckedChanged += new System.EventHandler(this.rB_onTop_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "default Values";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(83, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "(1000 - 10000 ms)";
            // 
            // labelAppName
            // 
            this.labelAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppName.ForeColor = System.Drawing.Color.Red;
            this.labelAppName.Location = new System.Drawing.Point(12, 61);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(205, 20);
            this.labelAppName.TabIndex = 9;
            this.labelAppName.Text = "-";
            this.labelAppName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bg2
            // 
            this.bg2.WorkerSupportsCancellation = true;
            this.bg2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg2_DoWork);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(230, 146);
            this.Controls.Add(this.labelAppName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 220);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(246, 185);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Force & Reset AppFocus";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
       
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox tBInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBAppName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bg2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rB_Focus;
        private System.Windows.Forms.RadioButton rB_onTop;
    }
}

