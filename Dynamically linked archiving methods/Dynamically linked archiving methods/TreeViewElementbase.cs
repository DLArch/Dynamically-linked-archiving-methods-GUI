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
            //System.IntPtr Point;
            //Point.ToPointer
        }
        /// <summary>
        /// Метод, определяющий, папка ли перед ним
        /// TODO: Изменить алгоритм отображения папок
        /// </summary>
        /// <param name="Path"></param>
        private void DirectoryChecker(string Path)
        {
            if (System.IO.Directory.Exists(Path))
            {
                this.Elements = new ObservableCollection<Elementbase>();
                string[] Directories_;
                try
                {
                    Directories_ = System.IO.Directory.GetDirectories(this.Path);
                }
                catch
                {
                    Directories_ = new string[1];
                }
                foreach (string x in Directories_)
                {
                    if (System.IO.Directory.Exists(x))
                    {
                        this.Elements.Add(new Dynamically_linked_archiving_methods.Elementbase(x));
                    }
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
            this.Elements = new ObservableCollection<Elementbase>();
            foreach (System.IO.DriveInfo x in System.IO.DriveInfo.GetDrives())
            {
                this.Elements.Add(new Elementbase(x.Name, x.Name));
            }
        }
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
