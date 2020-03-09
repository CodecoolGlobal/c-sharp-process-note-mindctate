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
            List<processlist> processList = new List<processlist>();
            foreach (Process process in processes)
            {
                processList.Add(new processlist() { Id = process.Id, Name = process.ProcessName });
            }
            ProcessInfo.ItemsSource = processList;
        }

        public class processlist
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
