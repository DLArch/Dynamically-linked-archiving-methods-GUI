using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Dynamically_linked_archiving_methods
{
    public class Elementbase
    {
        public Elementbase(string Name, string Path, ObservableCollection<Elementbase> Elements)
        {
            this.Name = Name;
            this.Path = Path;
            this.Elements = Elements;
        }
        public Elementbase(string Name, string Path)
        {
            this.Name = Name;
            this.Path = Path;
            DirectoryChecker(Path);
        }
        /// <summary>
        /// Выделяет имя из пути и заполняет поля
        /// </summary>
        /// <param name="Path"></param>
        public Elementbase(string Path)
        {
            this.Path = Path;
            this.Name = string.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
            DirectoryChecker(Path);
        }
        public Elementbase()
        {
            Name = "Undefined";
            DirectoryChecker(Path);
        }
        /// <summary>
        /// Прогружает все папки на компьютере
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key"></param>
        public Elementbase(string path, int key)
        {
            if (key == 1)
            {
                this.TreeMaker();
            }
        }
        /// <summary>
        /// При True вызывает Elementbase.CreateDrives()
        /// </summary>
        /// <param name="CreateDrivesBool"></param>
        public Elementbase(bool CreateDrivesBool)
        {
            if (CreateDrivesBool)
            {
                CreateDrives();
            }
        }
        public void TreeMakerPath(string Path)
        {
            
        }
        /// <summary>
        /// Метод, определяющий, папка ли перед ним
        /// </summary>
        /// <param name="Path"></param>
        public void DirectoryChecker(string Path)
        {
            if (System.IO.Directory.Exists(this.Path))
            {
                if (this.Elements == null || this.Elements.Count == 0)
                {
                    this.Elements = new ObservableCollection<Elementbase>();
                }
                this.Elements.Add(new Elementbase());
                //this.ElementCreator();
            }
        }
        public void TreeMakerPathLocally()
        {
            if (System.IO.Directory.Exists(this.Path) || System.IO.File.Exists(this.Path))
            {
                foreach (string x in System.IO.Directory.EnumerateFileSystemEntries(this.Path))
                {
                    if (this.Elements == null)
                    {
                        this.ElementCreator();
                    }
                    this.Elements.Add(new Elementbase(x));
                }
            }
        }
        /// <summary>
        /// Создает дерево
        /// </summary>
        public void TreeMaker()
        {

        }
        /// <summary>
        /// Добавляет в поле Elements данные о дисках
        /// </summary>
        public void CreateDrives()
        {
            this.ElementCreator();
            foreach (System.IO.DriveInfo x in System.IO.DriveInfo.GetDrives())
            {
                if (System.IO.Directory.Exists(x.Name))
                {
                    ObservableCollection<Elementbase> g = new ObservableCollection<Elementbase>();
                    g.Add(new Elementbase());
                    this.Elements.Add(new Elementbase(x.Name, x.Name, g));
                }
            }
        }
        /// <summary>
        /// Создает пустую колекцию
        /// </summary>
        public void ElementCreator()
        {
            this.Elements = new ObservableCollection<Elementbase>();
        }
        /// <summary>
        /// Имя файла/папки
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Путь к файлу/папке
        /// </summary>
        public string Path
        {
            get;
            set;
        }
        /// <summary>
        /// Коллекция вложенных типов
        /// </summary>
        public ObservableCollection<Elementbase> Elements
        {
            get;
            set;
        }
    }
}
