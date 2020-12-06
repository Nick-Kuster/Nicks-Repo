using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AMOSToNetSmart
{
    public partial class AMOSToNetSmart : ServiceBase
    {
        Timer AddTimer = new Timer();
        Timer DeleteTimer = new Timer();
        Timer ResendFailTimer = new Timer();
        public AMOSToNetSmart()
        {
            InitializeComponent();
            WriteToFile($"{DateTime.Now.ToLongTimeString()} started");
        }

        protected override void OnStart(string[] args)
        {
            AddTimer.Elapsed += new ElapsedEventHandler(SendAddInvoices);
            AddTimer.Interval = 5000; 
            AddTimer.Enabled = true;

            DeleteTimer.Elapsed += new ElapsedEventHandler(SendDeleteInvoices);
            DeleteTimer.Interval = 7000;
            DeleteTimer.Enabled = true;

            ResendFailTimer.Elapsed += new ElapsedEventHandler(ResendFailedInvoices);
            ResendFailTimer.Interval = 10000;
            ResendFailTimer.Enabled = true;
        }

        private void SendAddInvoices(object source, ElapsedEventArgs e)
        {
            WriteToFile($"{DateTime.Now.ToLongTimeString()} Add Invoices");
        }
        private void SendDeleteInvoices(object source, ElapsedEventArgs e)
        {
            WriteToFile($"{DateTime.Now.ToLongTimeString()} Delete Invoices");
        }
        private void ResendFailedInvoices(object source, ElapsedEventArgs e)
        {
            WriteToFile($"{DateTime.Now.ToLongTimeString()} Resend Failed Invoices");
        }
        private static void WriteToFile(string message)
        {
            string path = @"c:\dev\logs";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = Path.Combine(path, "TestingService.txt");

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }
            }
        }

        protected override void OnStop()
        {
            WriteToFile($"{DateTime.Now.ToLongTimeString()} stopped");
        }
    }
}
