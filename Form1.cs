using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace MessageClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ipadr = IPAddress.Loopback;
        }

        Socket clientSocket = null;
        static Boolean isListen = true;
        Thread thDataFromServer;
        IPAddress ipadr;

        private void button3_Click(object sender, EventArgs e)
        {
            SendMessage();
        }
        private void SendMessage()
        {
            if (String.IsNullOrWhiteSpace(textBox2.Text.Trim()))
            {
                MessageBox.Show("发送内容不能为空哦~");
                return;
            }
            if (clientSocket != null && clientSocket.Connected)
            {
                Byte[] bytesSend = Encoding.UTF8.GetBytes(textBox2.Text + "$");
                clientSocket.Send(bytesSend);
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("未连接服务器或者服务器已停止，请联系管理员~");
                return;
            }
        }


        //每一个连接的客户端必须设置一个唯一的用户名，在服务器端是把用户名和套接字保存在Dictionary<userName,ClientSocket>中
        private void button1_Click(object sender, EventArgs e)
        {

            if (clientSocket == null || !clientSocket.Connected)
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //参考网址： https://msdn.microsoft.com/zh-cn/library/6aeby4wt.aspx
                    // Socket.BeginConnect 方法 (String, Int32, AsyncCallback, Object)
                    //开始一个对远程主机连接的异步请求
                    /* string host,     远程主机名
                     * int port,        远程主机的端口
                     * AsyncCallback requestCallback,   一个 AsyncCallback 委托，它引用连接操作完成时要调用的方法，也是一个异步的操作
                     * object state     一个用户定义对象，其中包含连接操作的相关信息。 当操作完成时，此对象会被传递给 requestCallback 委托
                     */
                    //如果txtIP里面有值，就选择填入的IP作为服务器IP，不填的话就默认是本机的
                    if (!String.IsNullOrWhiteSpace(textBox1.Text.ToString().Trim()))
                    {
                        try
                        {
                            ipadr = IPAddress.Parse(textBox1.Text.ToString().Trim());
                        }
                        catch
                        {
                            MessageBox.Show("请输入正确的IP后重试");
                            return;
                        }
                    }
                    else
                    {
                        ipadr = IPAddress.Loopback;
                    }
                    //IPAddress ipadr = IPAddress.Parse("192.168.1.100");
                    clientSocket.BeginConnect(ipadr, 8080, (args) =>
                    {
                        if (args.IsCompleted)   //判断该异步操作是否执行完毕
                        {
                            Byte[] bytesSend = new Byte[4096];
                            textBox1.BeginInvoke(new Action(() =>
                            {
                                if (clientSocket != null && clientSocket.Connected)
                                {
                                    textBox1.Enabled = false;
                                }
                            }));
                        }
                    }, null);
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("你已经连接上服务器了");
            }
        }

        private void ShowMsg(String msg)
        {
            textBox3.BeginInvoke(new Action(() =>
            {
                textBox3.Text += Environment.NewLine + msg;    // 在 Windows 环境中，C# 语言 Environment.NewLine == "\r\n" 结果为 true
                //txtReceiveMsg.ScrollToCaret();
            }));
        }

        //获取服务器端的消息
        private void DataFromServer()
        {
            ShowMsg("Connected to the Chat Server...");
            isListen = true;
            try
            {
                while (isListen)
                {
                    Byte[] bytesFrom = new Byte[4096];
                    Int32 len = clientSocket.Receive(bytesFrom);

                    String dataFromClient = Encoding.UTF8.GetString(bytesFrom, 0, len);
                    if (!String.IsNullOrWhiteSpace(dataFromClient))
                    {
                        //如果收到服务器已经关闭的消息，那么就把客户端接口关了，免得出错，并在客户端界面上显示出来
                        if (dataFromClient.ToString().Length >= 17 && dataFromClient.ToString().Substring(0, 17).Equals("Server has closed"))
                        {
                            clientSocket.Close();
                            clientSocket = null;

                            textBox3.BeginInvoke(new Action(() =>
                            {
                                textBox3.Text += Environment.NewLine + "服务器已关闭";
                            }));

                            textBox1.BeginInvoke(new Action(() =>
                            {

                                textBox1.Enabled = true;

                            }));

                            thDataFromServer.Abort();   //这一句必须放在最后，不然这个进程都关了后面的就不会执行了

                            return;
                        }


                        if (dataFromClient.StartsWith("#") && dataFromClient.EndsWith("#"))
                        {
                            String userName = dataFromClient.Substring(1, dataFromClient.Length - 2);
                            this.BeginInvoke(new Action(() =>
                            {
                                MessageBox.Show("用户名：[" + userName + "]已经存在，请尝试其他用户名并重试");

                            }));
                            isListen = false;

                            textBox1.BeginInvoke(new Action(() =>
                            {

                                textBox1.Enabled = true;

                            }));

                        }
                        else
                        {
                            //txtName.Enabled = false;    //当用户名唯一时才禁止再次输入用户名
                            ShowMsg(dataFromClient);
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                isListen = false;
                if (clientSocket != null && clientSocket.Connected)
                {
                    //没有在客户端关闭连接，而是给服务器发送一个消息，在服务器端关闭连接
                    //这样可以将异常的处理放到服务器。客户端关闭会让客户端和服务器都抛异常
                    clientSocket.Send(Encoding.UTF8.GetBytes("$"));
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Send(Encoding.UTF8.GetBytes("$"));
            }
        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                thDataFromServer.Abort();
                clientSocket.Send(Encoding.UTF8.GetBytes("$"));

                clientSocket.Close();
                clientSocket = null;

                textBox3.BeginInvoke(new Action(() =>
                {
                    textBox3.Text += Environment.NewLine + "已断开与服务器的连接";
                }));

                textBox1.BeginInvoke(new Action(() =>
                {

                    textBox1.Enabled = true;

                }));
            }
        }
    }
}