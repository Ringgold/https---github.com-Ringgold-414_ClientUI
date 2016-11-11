using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TelecomClientC
{
   
    public partial class Form1 : Form
    {
        TelecomClient client;
        public Form1()
        {
            InitializeComponent();
        }

        public void connectUDP()
        {
            //use 5 retries connect MyName to server
            int udpTimeoutCount = 0;
            while (client.connected == false)
            {
                Console.WriteLine("Connecting to server...");
                client.connect(txtMyName.Text);
                Thread.Sleep(2000);
                udpTimeoutCount += 1;
                if (udpTimeoutCount > 5)
                {
                    break;
                }
            }
            client.connect2();
            Console.WriteLine("Connection success");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            client = new TelecomClient(txtServerIP.Text);
            //Check if txtServerIP.Text is null
            Thread trd = new Thread(connectUDP);
            trd.Start();
        }

        //Get UserList
        private void Button2_Click(object sender, EventArgs e)
        {
            client.sendMessageTCP("userList");
        }


        //Connect to another user
        private void Button3_Click(object sender, EventArgs e)
        {
            client.sendMessageTCP("connect:" + this.TextBox1.Text);
            User temp = client.connectedUsers.FirstOrDefault(o => o.getUserName() == this.TextBox1.Text);
            client.targetUser = temp;
        }

        //Disconnect that one particular user
        private void button4_Click(object sender, EventArgs e)
        {
            client.sendMessageTCP("exit");
            client.close();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked");
            client.getDestinationInfo(client.targetUser);
            if (client.destinationIP == "" || client.destinationPort == 0)
            {
                Console.WriteLine("No target detected.");
            } else
            {
                client.sendMessage(this.txtInput.Text, client.destinationIP, client.destinationPort);
                Console.WriteLine(this.txtInput.Text + " " + client.destinationIP + " " + client.destinationPort);
                this.txtInput.Text = String.Empty;
            }
        }
    }
}
