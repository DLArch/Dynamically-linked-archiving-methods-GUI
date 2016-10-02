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
            this.textBox.Text = System.IO.Directory.GetCurrentDirectory();
            /// <summary>
            /// TODO: Выделить создание структуры в отдельный поток
            /// </summary>
            Elementbase Root = new Elementbase("Мой компьютер", "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}", AppDomain.CurrentDomain.BaseDirectory + @"Mc.jpg");
            this.AddSpecialFolders(ref Root);
            Root.CreateDrives();
            this.treeView.Items.Add(Root);
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
        /// <summary>
        /// Событие раскрытия эллемента treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// TODO: Убрать try...catch
        private void treeView_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(((Elementbase)((TreeViewItem)e.OriginalSource).DataContext).Path))
                {
                    ((Elementbase)((TreeViewItem)e.OriginalSource).DataContext).DirectoryChecker();
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// Добавляет специальные папки для быстрого доступа
        /// </summary>
        /// <param name="Root"></param>
        private void AddSpecialFolders(ref Elementbase Root)
        {
            if (Root.Elements == null)
            {
                Root.ElementCreator();
            }
            Root.CreateSpecialDirectoris(System.Environment.SpecialFolder.Favorites);
            Root.CreateSpecialDirectoris(System.Environment.SpecialFolder.MyVideos);
            Root.CreateSpecialDirectoris(System.Environment.SpecialFolder.MyDocuments);
            Root.CreateSpecialDirectoris(System.Environment.SpecialFolder.MyPictures);
            Root.CreateSpecialDirectoris(System.Environment.SpecialFolder.MyMusic);
            Root.CreateSpecialDirectoris(System.Environment.SpecialFolder.DesktopDirectory);
            //Root.CreateSpecialDirectoris(System.Environment.SpecialFolder);
        }
        /// <summary>
        /// Событие двойного клика для перехода
        /// на следующий уровень вложенности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ///Был вылет при клике на картинку
            ///Заменить Try на is и as
            try
            {
                if (System.IO.File.Exists(((Elementbase)((TextBlock)e.OriginalSource).DataContext).Path))
                {
                    MessageBox.Show(((Elementbase)((TextBlock)e.OriginalSource).DataContext).Path);
                }
            }
            catch
            {
                try
                {
                    if (System.IO.File.Exists(((Elementbase)((Image)e.OriginalSource).DataContext).Path))
                    {
                        MessageBox.Show(((Elementbase)((Image)e.OriginalSource).DataContext).Path);
                    }
                }
                catch
                {

                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView.Items.IndexOf(((Elementbase)this.treeView.SelectedItem).Path) == -1)
            {
                this.listView.Items.Add(((Elementbase)this.treeView.SelectedItem).Path);
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.listView.Items.Clear();
        }
    }
}