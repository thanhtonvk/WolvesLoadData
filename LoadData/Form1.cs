using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using LoadData.Models;
using System;
using System.Threading;
using System.Windows.Forms;

namespace LoadData
{
    public partial class Form1 : Form
    {
        IFirebaseClient client;
        public Form1()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            Thread t = new Thread(new ThreadStart(run));
            t.Start();
        }

        SendNotification sendNotification = new SendNotification();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "R20tmZqaTY9WnrsEr9vk5nyzq6rZ6hO4OACKD1Su",
            BasePath = "https://wolfteam-f01f4-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        public void run()
        {
            try
            {
                BanLenhDAL banLenhDAL = new BanLenhDAL(client, sendNotification);
                NewsDAL newsDal = new NewsDAL(client, sendNotification);
                RateDAL rateDAL = new RateDAL();
                while (true)
                {
                
                    rateDAL.loadRate();
                 
                    banLenhDAL.loadBanLenh();
               
                    newsDal.addNormalNews();
                
                    newsDal.addNewsVip();
                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                //label1.Text = ex.Message.ToString();
                Console.WriteLine(ex.Message);
                run();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
