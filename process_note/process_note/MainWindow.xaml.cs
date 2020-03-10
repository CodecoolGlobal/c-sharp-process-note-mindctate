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
        public MainWindow()
        {
            InitializeComponent();
            processes = Process.GetProcesses();
            List<ProcessList> processList = new List<ProcessList>();
            foreach (Process process in processes)
            {
                processList.Add(new ProcessList() { Id = process.Id, Name = process.ProcessName});
                
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

    }
}
