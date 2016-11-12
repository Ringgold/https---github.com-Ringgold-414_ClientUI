
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using TelecomClientC;

public class TelecomClient
{
    public bool connected;
    public string serverIP;
    public int serverTCPPort;
    public int serverUDPPort;
    public TcpClient tcpClient;
    private NetworkStream tcpStream;
    public UdpClient udpClient;
    public string privateIP;
    private string userName;
    private int count;
    private string connectionString;
    private string message;
    private string myPublicIP;
    private int myPublicPort;
    private string myPrivateIP;
    private int myPrivatePort;
    private bool isUDPListenerStarted = false;
    public List<User> connectedUsers = new List<User>();
    public User targetUser;
    public string destinationIP = "";
    public int destinationPort = 0;
    public bool sameClient = false;
    public string userlistString = "";
    private int silence = 0;
    private bool beatReceived = false;

    public static string messageNotice = "messageNotice:";
    public static string heartBeatNotice = "hearBeat:";

    public TelecomClient(String serverIP)
    {
        count = 0;
        connected = false;
        //serverIP = "192.168.1.8"
        this.serverIP = serverIP;
        serverTCPPort = 5090;
        serverUDPPort = 5100;

        string host = System.Net.Dns.GetHostName();
        privateIP = GetLocalIPAddress();
        Console.WriteLine(privateIP.ToString());

        bool created = false;
        int privatePort = 5000;

        //Create a private port and keep incrementing untill find available port
        while (created == false)
        {
            try
            {
                udpClient = new UdpClient(privatePort);
                created = true;
            }
            catch (Exception ex)
            {
                privatePort += 1;
            }
        }
    }

    public void sendMessageUDP(string content, string ip, int port)
    {
        udpClient.Connect(ip, port);
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(content);
        udpClient.Send(sendBytes, sendBytes.Length);
    }

    //Send message to server
    public void sendMessageTCP(string message)
    {
        this.message = message;
        Thread trd = new Thread(sendMessageTCP_trd);
        trd.Start();
    }

    private void sendMessageTCP_trd()
    {
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(this.message + Environment.NewLine);
        NetworkStream stream = tcpClient.GetStream();
        stream.Write(sendBytes, 0, sendBytes.Length);
    }

    public void connect(string userName)
    {
        this.userName = userName;
        Thread trd = new Thread(connect_UDP);
        trd.Start();
    }

    public void connect2()
    {
        Thread trd = new Thread(connect_TCP);
        trd.Start();
    }

    public void startUDP_listener()
    {
        Thread trd = new Thread(UDP_listener);
        trd.Start();
    }

    private void UDP_listener()
    {
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        Console.WriteLine("UDP Listener started");
        string heartBeat = "heartBeat:";
        string actualMessage = "actualMessage";

        while (true)
        {
            Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
            string returnData = System.Text.Encoding.ASCII.GetString(receiveBytes);
<<<<<<< HEAD

            int messageCheck = returnData.IndexOf(messageNotice);
            int heartBeatCheck = returnData.IndexOf(heartBeatNotice);

            //Indicate heartbeat received
            if (heartBeatCheck >= 0)
            {
                Console.WriteLine("HEARTBEAT received");
            }

            //Indicate message received
            if (messageCheck >= 0)
            {
                returnData = returnData.Substring(messageNotice.Length);
                messageBuffer += returnData;
            }

            //Console.WriteLine("UDP Received:" + returnData);
=======
            int heartBeatExist = returnData.IndexOf(heartBeat);
            int actualMessageExist = returnData.IndexOf(actualMessage);
            if (heartBeatExist >= 0)
            {
                beatReceived = true;
            }
            else
            {
                beatReceived = false;
                if (actualMessageExist >= 0)
                {

                }
            }

            if (!beatReceived)
            {
                Console.WriteLine("UDP Received:" + returnData);
            }
>>>>>>> origin/master
            //this.mainForm.txtChat.Text += returnData;
        }
    }

    private void connect_UDP()
    {
        // This constructor arbitrarily assigns the local port number.
        if (connected == true)
        {
            return;
        }
        if (count >= 5)
        {
            //connected = True
            count = 0;
            return;
        }
        count += 1;

        try
        {
            udpClient.Connect(serverIP, serverUDPPort);
            IPEndPoint ipEndpoint = (IPEndPoint)udpClient.Client.LocalEndPoint;
            string port = ipEndpoint.Port.ToString();
            //Dim ip As String = ipEndpoint.AddressFamily.ToString()

            // Sends a message to the host to which you have connected.
            string identity = "{\"userName\":\"" + userName + "\",publicIp\":\"0\"," + "\"privateIp\":\"" + privateIP + "\",\"privatePort\":\"" + port + "\",\"publicPort\":\"0\"}";
            Console.WriteLine(identity);
            Byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(identity);
            //sendBytes(0) = 0

            udpClient.Send(sendBytes, sendBytes.Length);

            // Sends message to a different host using optional hostname and port parameters.
            //Dim udpClientB As New UdpClient()
            //udpClientB.Send(sendBytes, sendBytes.Length, "AlternateHostMachineName", 11000)

            // IPEndPoint object will allow us to read datagrams sent from any source.
            //Dim RemoteIpEndPoint As New IPEndPoint(New IPAddress(Encoding.ASCII.GetBytes(serverIP)), serverPort)
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //Dim RemoteIpEndPoint As New IPEndPoint(New IPAddress(serverIP), serverPort)

            // UdpClient.Receive blocks until a message is received from a remote host.
            Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
            string returnData = System.Text.Encoding.ASCII.GetString(receiveBytes);
            // Which one of these two hosts responded?
            this.connectionString = returnData.ToString();
            User tempUser = JsonConvert.DeserializeObject<User>(this.connectionString);

            //update user's info of public and private IP
            myPrivateIP = tempUser.getPrivateIp();
            myPublicIP = tempUser.getPublicIp();
            myPrivatePort = tempUser.getPrivatePort();
            myPublicPort = tempUser.getPublicPort();
            userName = tempUser.getUserName();

            Console.WriteLine(("This is the message you received " + this.connectionString));
            Console.WriteLine(("This message was sent from " + RemoteIpEndPoint.Address.ToString() + " on their port number " + RemoteIpEndPoint.Port.ToString()));
            if ((returnData.ToString() == "SUCCESS"))
            {
                connected = true;
            }
            this.connected = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public void connect_TCP()
    {
        try
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer 
            // connected to the same address as specified by the server, port
            // combination.

            //Dim identity As String = "username:" + Me.userName + vbNewLine
            string identity = this.connectionString;
            //Dim publicIP As String = IpAddress()
            //Console.WriteLine(publicIP)
            //Return

            Byte[] data = System.Text.Encoding.UTF8.GetBytes(identity);
            //Data(0) = 0
            //Console.WriteLine(data(0))
            Console.WriteLine(identity);

            tcpClient = new TcpClient(serverIP, serverTCPPort);
            Console.WriteLine(identity);
            //udpClient = New UdpClient(serverIP, serverPort)
            //Get a client stream for reading and writing.
            //Stream stream = client.GetStream();
            //Dim bw As System.IO.StreamWriter = New System.IO.StreamWriter()

            tcpStream = tcpClient.GetStream();
            // Translate the passed message into ASCII and store it as a Byte array.
            Console.WriteLine(identity);
            // Send the message to the connected TcpServer. 
            tcpStream.Write(data, 0, data.Length);
            Console.WriteLine(identity);
            //stream.Close()
            //Console.WriteLine("Sent: {0}", "identity:" + identity)
            // Read the first batch of the TcpServer response bytes.
            System.IO.BufferedStream br = new System.IO.BufferedStream(tcpStream);
            System.IO.StreamReader streamReader = new System.IO.StreamReader(br);
            while ((streamReader.EndOfStream == false))
            {
                string currentLine = streamReader.ReadLine(); //return message from server
                //parse return message according to the message type
                //perform actions based on type.
                string response = handleResponse(currentLine);
                

                if (response != "Silence")
                {
                    Console.WriteLine(response);
                    Console.WriteLine("Received:" + currentLine);
                    Console.WriteLine("In busy waiting");
                } else
                {
                    silence++;
                    if (silence >= 15)
                    {
                        silence = 0;
                        Console.WriteLine("Received:" + currentLine);
                        Console.WriteLine("In busy waiting");
                    }
                }

                
                //this.mainForm.txtMain.Text += currentLine + Constants.vbNewLine;
            }
            //stream.Read(data, 0, 2)
            //Dim count As Integer
            //br.Read(data, 0, 2)
            //Dim bytes As Int32 = stream.ReadByte()
            Console.Write("TCP closed");
            tcpStream.Close();
            // Close everything.
            tcpClient.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
    }

    //Function to close the dialog and close TCP connection
    public void close()
    {
        try
        {
            if ((connected == true))
            {
                udpClient.Close();
            }

            tcpClient.Close();
            Console.Write("Client is closed");
        }
        catch(Exception e)
        {

        }
        
    }

    private Dictionary<string, string> parseResult(string message)
    {
        Dictionary<string, string> ret = new Dictionary<string, string>();

        return ret;
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
    }

    public string handleResponse(string message)
    {
        string result = "";

        //Different types of messages client can receive
        string loginNotice = "new_log_in:";
        string logoffNotice = "log_off:";
        string connectNotice = "connection_from:";
        string success = "SERVER_SUCCESS";
        string fail = "SERVER_FAIL";
        string userlist = "userList:";
        string checkNotice = "check";

        int loginExist = message.IndexOf(loginNotice);
        int logoffExist = message.IndexOf(logoffNotice);
        int connectExist = message.IndexOf(connectNotice);
        int successExist = message.IndexOf(success);
        int failExist = message.IndexOf(fail);
        int userListExist = message.IndexOf(userlist);
        int checkNoticeExist = message.IndexOf(checkNotice);

        if (failExist >= 0)
        {
            
            //Do Nothing if is only a fail message
            result = "Connection Status is updated";
        }
        else if (successExist >= 0)
        {
            //Update the UserList if server connection is successful
            sendMessageTCP("userList");
            Console.WriteLine("Update Userlist");
            result = "Connection Status is updated";

            if (isUDPListenerStarted == false)
            {
                startUDP_listener();
                isUDPListenerStarted = true;
            }

        } else 
        {
            if (loginExist >= 0)
            {
                //one user has connected to the network, save it or leave ir if it already exist in the userlist
                message = message.Substring(loginNotice.Length);
                User tempUser = JsonConvert.DeserializeObject<User>(message);
                Console.WriteLine(tempUser.getUserName()+" has connected to the network.");
                if (findIndexOfList(tempUser, connectedUsers) >= 0)
                {
                    result = "New user login detected, but no need to add to current UserList.";
                } else
                {
                    connectedUsers.Add(tempUser);

                    result = "New user " + tempUser.getUserName() + " added to UserList.";
                }

            }
            else
            {
                if (logoffExist >= 0)
                {
                    //User Logoff detected, need to delete this user from the userlist
                    message = message.Substring(logoffNotice.Length);
                    User tempUser = JsonConvert.DeserializeObject<User>(message);

                    //Stop the heartbeat sending if the target user is logged of
                    if (tempUser.getUserName() == targetUser.getUserName())
                    {
                        Form1.connectionTarget = false;
                    }

                    //Remove te user from the userlist and handle the exception error
                    if (findIndexOfList(tempUser, connectedUsers) >= 0)
                    {
<<<<<<< HEAD
                        connectedUsers.RemoveAt(findIndexOfList(tempUser, connectedUsers));
                        this.userListUpdated = true;
                        result = "Current logoff user "+ tempUser.getUserName() + " has been deleted from the UserList.";
=======
                        connectedUsers.Remove(tempUser);
                        result = "Current logoff user " + tempUser.getUserName() + " has been deleted from the UserList.";
>>>>>>> origin/master
                    }
                    else
                    {
                        result = "UserList error detected, user " + tempUser.getUserName() + " does not exist in UserList, cannot delete.";
                    }
                } else
                {
                    if (userListExist >= 0)
                    {
                        List<User> tempUserList = new List<User>();
                        message = message.Substring(userlist.Length);
                        //Parse the whole list
                        dynamic parsedUserListObject = JsonConvert.DeserializeObject(message);
                        String userName;
                        String publicIp;
                        String privateIp;
                        int privatePort;
                        int publicPort;

                        //Save each user's parameters to an object and add to the object list
                        foreach (var singleUser in parsedUserListObject)
                        {
                            userName = singleUser.userName;
                            publicIp = singleUser.publicIp;
                            privateIp = singleUser.privateIp;
                            privatePort = singleUser.privatePort;
                            publicPort = singleUser.publicPort;
                            User tempUser = new User(publicIp, publicPort, privateIp, privatePort, userName);
                            tempUserList.Add(tempUser);
                            Console.WriteLine(userName+" is added to the current UserList");
                        }

                        //update the whole userlist
                        connectedUsers = tempUserList;

                    } else
                    {
                        
                        //Connection received from a user, save this user as the target User
                        if (connectExist >= 0)
                        {
                            message = message.Substring(connectNotice.Length);
                            //Find the user with this name and save it as the target user to send message to
                            User temp = connectedUsers.FirstOrDefault(o => o.getUserName() == message);
                            targetUser = temp;
                            result = "Connection from " + targetUser.getUserName() + " is received";

                            getDestinationInfo(targetUser);
                            if (destinationIP == "" || destinationPort == 0)
                            {
                                Console.WriteLine("ERROR: Targetuser not existing.");
                            }
                            else
                            {
                                //Send punch through message
                                sendMessageUDP("Receiver Punch Through", destinationIP, destinationPort);
                                Form1.connectionTarget = true;
                                Console.WriteLine("Receiver Punch Through" + " " + destinationIP + " " + destinationPort);
                            }

                            //result = "Connection from " + message + " is received";
                        } else
                        {
                            if (checkNoticeExist >= 0)
                            {
                                result = "Silence";
                            }
                        }
                    }
                }
            }
        }
        return result;
    }

    public void getDestinationInfo(User target)
    {
        string target_publicIp = target.getPublicIp();
        string target_privateIp = target.getPrivateIp();
        int target_publicPort = target.getPublicPort();
        int target_privatePort = target.getPrivatePort();

        if (target_publicIp == myPublicIP)
        {
            destinationIP = target_privateIp;
            destinationPort = target_privatePort;
        }
        else if (target_privateIp == myPrivateIP)
        {
            sameClient = true;
            destinationIP = target_privateIp;
            destinationPort = target_privatePort;
        }
        else
        {
            destinationIP = target_publicIp;
            destinationPort = target_publicPort;
        }
    }

<<<<<<< HEAD
    public List<User> getUserListUpdate()
    {
        if (userListUpdated == false)
        {
            return null;
        }
        else
        {
            userListUpdated = false;
            return connectedUsers;
        }
    }

    public String getUsername()
    {
        return this.userName;
    }

    //Locate the index of some specific user inside the UserList
    public int findIndexOfList(User temp, List<User> list)
    {
        for (int i = 0; i < connectedUsers.Count; i++)
        {
            User cur = connectedUsers[i];
            if (temp.getUserName() == cur.getUserName())
            {
                return i;
            }
        }
        return -1;
    }
=======
>>>>>>> origin/master
}