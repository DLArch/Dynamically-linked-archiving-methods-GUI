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
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dynamically_linked_archiving_methods
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.textBox.Text = System.IO.Directory.GetCurrentDirectory();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.Directory.Exists(this.textBox.Text))
                System.IO.Directory.SetCurrentDirectory(this.textBox.Text);
            else
                System.Windows.Forms.MessageBox.Show("Путь несуществует. Укажите другой путь");
            this.treeView.Items.Clear();
            foreach (string x in System.IO.Directory.GetFileSystemEntries(System.IO.Directory.GetCurrentDirectory()))
            {
                this.treeView.Items.Add(x);
            }
        }

        private void treeView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                foreach (string x in (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop))
                {
                    if (this.treeView.Items.IndexOf(x) == -1)
                        this.treeView.Items.Add(x);
                }
            }
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                //int SItem = this.treeView.Items.IndexOf(this.treeView.SelectedItem);
                this.treeView.Items.Remove(this.treeView.SelectedItem);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.treeView.Items.Clear();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                this.button1_Click(sender, new RoutedEventArgs());
        }
    }
}