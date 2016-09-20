using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Text.RegularExpressions;
//using shell32.dll;

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
            /// <summary>
            /// Root - первый элемент для TreeView(корень дерева)
            /// </summary>
            Elementbase root = new Elementbase("Мой компьютер", "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
            root.CreateDrives();
            //DataContext = root;
            this.textBox.Text = System.IO.Directory.GetCurrentDirectory();
            this.treeView.Items.Add(root);
        }

        /// <summary>
        /// В treeView запишится путь к файлу, если перетянут файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                foreach (string x in (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop))
                {
                    if (this.listView.Items.IndexOf(x) == -1)
                    {
                        this.listView.Items.Add(x);
                    }
                }
            }
        }
    }
}