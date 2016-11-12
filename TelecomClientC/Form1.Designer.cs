using System;

namespace TelecomClientC
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtMyName = new System.Windows.Forms.TextBox();
            this.Button3 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.txtMain = new System.Windows.Forms.RichTextBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(431, 41);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(61, 13);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "Target user";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(25, 41);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(58, 13);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "Username:";
            // 
            // txtMyName
            // 
            this.txtMyName.Location = new System.Drawing.Point(89, 38);
            this.txtMyName.Name = "txtMyName";
            this.txtMyName.Size = new System.Drawing.Size(100, 20);
            this.txtMyName.TabIndex = 17;
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(353, 35);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(72, 23);
            this.Button3.TabIndex = 16;
            this.Button3.Text = "Connect";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(498, 39);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(74, 20);
            this.TextBox1.TabIndex = 15;
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(264, 35);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(83, 23);
            this.Button2.TabIndex = 14;
            this.Button2.Text = "Get User List";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(195, 35);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(63, 24);
            this.Button1.TabIndex = 13;
            this.Button1.Text = "Login";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
          
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(24, 354);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(815, 166);
            this.txtInput.TabIndex = 23;
            this.txtInput.Text = "";
            // 
            // txtMain
            // 
            this.txtMain.Location = new System.Drawing.Point(24, 64);
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(592, 284);
            this.txtMain.TabIndex = 22;
            this.txtMain.Text = "";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(89, 12);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(100, 20);
            this.txtServerIP.TabIndex = 24;
            this.txtServerIP.Text = "159.203.4.199";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "ServerIP:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(588, 35);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 24);
            this.button4.TabIndex = 26;
            this.button4.Text = "Exit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(711, 535);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(127, 26);
            this.button5.TabIndex = 27;
            this.button5.Text = "Send";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(622, 65);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(216, 277);
            this.listBox1.TabIndex = 28;
            // 
            // timer2
            // 
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 573);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.txtMain);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtMyName);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form_close);
        }

        private void OnClosed(EventHandler eventHandler)
        {
            throw new NotImplementedException();
        }

        #endregion
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtMyName;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.RichTextBox txtInput;
        internal System.Windows.Forms.RichTextBox txtMain;
        internal System.Windows.Forms.TextBox txtServerIP;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer2;
    }
}

