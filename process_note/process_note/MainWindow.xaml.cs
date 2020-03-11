using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace process_note
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Process[] processes;
        private static DateTime lastTime;
        private static TimeSpan lastTotalProcessorTime;
        private static DateTime curTime;
        private static TimeSpan curTotalProcessorTime;

        public MainWindow()
        {
            InitializeComponent();
            processes = Process.GetProcesses();
            List<ProcessList> processList = new List<ProcessList>();
            foreach (Process process in processes)
            {
                processList.Add(new ProcessList() { Id = process.Id, Name = process.ProcessName, /*CpuUsage = GetCpuUsage(process),*/ MemoryUsage = CalculateMemoryUsage(process) + " MB", 
                                                    RunningTime = GetRunningTime(process) + " s", StartTime = GetStartTime(process) });
                
            }
            ProcessInfo.ItemsSource = processList;

        }
        private void ListViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ProcessList selectedProcess = (ProcessList)ProcessInfo.SelectedItems[0];
            ProcessWindow win2 = new ProcessWindow();
            win2.Title = selectedProcess.Name;
            win2.Show();
        }

        public class ProcessList
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string CpuUsage { get; set; }

            public string MemoryUsage { get; set; }

            public string RunningTime { get; set; }

            public string StartTime { get; set; }

            public string Threads { get; set; }
        }

        /*public string GetCpuUsage(Process process)
        {
            try
            {
                if (lastTime == null)
                {
                    lastTime = DateTime.Now;
                    lastTotalProcessorTime = process.TotalProcessorTime;

                    return "0.00 %";
                }
                else
                {
                    curTime = DateTime.Now;
                    curTotalProcessorTime = process.TotalProcessorTime;

                    double CpuUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) /
                        (curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount));

                    lastTime = curTime;
                    lastTotalProcessorTime = curTotalProcessorTime;
                    return $"{CpuUsage:N2} %";
                }
            }
            catch
            {
                return "Not accessible";

            }

        }*/

        private string CalculateMemoryUsage(Process process)
        {
            return (process.PrivateMemorySize64 / (1024*1024)).ToString();
        }

        private string GetStartTime(Process process)
        {
            try
            {
                return process.StartTime.ToString();
            }
            catch
            {
                return "Not accessible";
            }
        }

        private string GetRunningTime(Process process)
        {
            try
            {
                TimeSpan runningTime = DateTime.Now.ToUniversalTime() - process.StartTime.ToUniversalTime();
                return Math.Round(runningTime.TotalSeconds, 2).ToString();
            }
            catch
            {
                return "Not accessible";

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int processId = Convert.ToInt32(((Button)sender).Tag);
            Process selectedProcess = Process.GetProcessById(processId);
            ProcessWindow win2 = new ProcessWindow();
            win2.Title = selectedProcess.Threads.ToString();
            MessageBox.Show(processId.ToString());
            win2.Show();
        }

    }
}
