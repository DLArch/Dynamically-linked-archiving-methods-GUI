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
            this.textBox.Text = System.IO.Directory.GetCurrentDirectory();
            //this.treeView.ItemTemplate.DataType = "{x:Type example:Drive}" ItemsSource = "{Binding Path=Children}";
        }

        

        public void treeMakerHelper(string Path)
        {
            
        }

        public void treeMaker(string Path)
        {
            foreach (string Disc in System.IO.Directory.GetLogicalDrives())
            {
                this.treeView.Items.Add(new TreeVeiwEllementBase(Disc));
            }
            for (int i = 1; i <= this.treeView.Items.Count-1; i++)
            {
                if (((TreeVeiwEllementBase)this.treeView.Items[i]).Path == string.Concat(Path.TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar)) + System.IO.Path.DirectorySeparatorChar)
                {
                    foreach (string EllPath in System.IO.Directory.GetFileSystemEntries(Path))
                    {
                        try
                        {
                            ((TreeVeiwEllementBase)this.treeView.Items[i]).Children.Add(new TreeVeiwEllementBase(EllPath));
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Установка дирректории из textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// TODO Отловить исключениядоступа к файлам
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show(System.IO.Directory.Exists(this.textBox.Text) ? "True" : "False");
            if (this.textBox.Text[this.textBox.Text.Count()-1] == System.IO.Path.VolumeSeparatorChar)
                this.textBox.Text += System.IO.Path.DirectorySeparatorChar;
            if (System.IO.Directory.Exists(this.textBox.Text))
                System.IO.Directory.SetCurrentDirectory(this.textBox.Text);
            else
                System.Windows.Forms.MessageBox.Show("Путь не существует. Укажите другой путь");
            this.treeView.Items.Clear();
            this.treeView.Items.Add(this.textBox.Text);
            treeMaker(System.IO.Directory.GetCurrentDirectory());
            //foreach (string Path in System.IO.Directory.GetFileSystemEntries(System.IO.Directory.GetCurrentDirectory()))
            //{
            //    this.treeView.Items.Add(new TreeVeiwEllementBase(Path, null));
            //}
            //this.treeView.SetValue(new DependencyPropertyKey(), this.textBox.Text.Where(x => x == System.IO.Path.DirectorySeparatorChar).Count());
        }

        /// <summary>
        /// В treeView запишится путь к файлу, если перетянут файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            else
                if (e.Key == System.Windows.Input.Key.Back)
                {
                    this.textBox.Text = string.Concat(this.textBox.Text.Take(this.textBox.Text.Count() - (this.textBox.Text.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Count() + 1)));
                    button1_Click(sender, new System.Windows.RoutedEventArgs());
                    //bool if_ = false;
                    //System.IO.Directory.GetLogicalDrives.Where(x => x ==)
                    //this.textBox.Text = System.IO.Directory.GetParent(this.textBox.Text).FullName;
                }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.treeView.Items.Clear();
        }

        /// <summary>
        /// Если нажата "Enter", установить путь из textBox.Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                this.button1_Click(sender, new System.Windows.RoutedEventArgs());
        }

        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                this.textBox.Text = ((TreeVeiwEllementBase)this.treeView.SelectedItem).Path;
            this.button1_Click(sender, new System.Windows.RoutedEventArgs());
        }
    }
}