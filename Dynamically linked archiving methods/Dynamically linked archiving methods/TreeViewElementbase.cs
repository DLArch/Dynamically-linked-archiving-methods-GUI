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
            this.ElementCreatorWithElement();
        }
        /// <summary>
        /// Выделяет имя из пути и заполняет поля
        /// </summary>
        /// <param name="Path"></param>
        public Elementbase(string Path)
        {
            this.Path = Path;
            this.Name = string.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
            this.ElementCreatorWithElement();
        }
        public Elementbase()
        {
            Name = "Undefined";
        }
        /// <summary>
        /// Прогружает все папки на компьютере
        /// Not for use
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key"></param>
        private Elementbase(string path, int key)
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
        /// <summary>
        /// Метод, определяющий, папка ли перед ним
        /// </summary>
        /// <param name="Path"></param>
        public void DirectoryChecker()
        {
            if (System.IO.Directory.Exists(this.Path) || System.IO.File.Exists(this.Path))
            {
                if (this.Elements != null)
                {
                    this.Elements.Clear();
                }
                try
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
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Отказано в доступе");
                }
            }
        }
        /// <summary>
        /// Проверяет папки по пути this.Path
        /// TODO: 
        /// 1) Убрать try...catch
        /// 2) Создать свой диалог при ошибках
        /// </summary>
        public void TreeMakerPathLocally()
        {
            if (System.IO.Directory.Exists(this.Path) || System.IO.File.Exists(this.Path))
            {
                if (this.Elements != null)
                {
                    this.Elements.Clear();
                }
                try
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
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Отказано в доступе");
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
                    this.Elements.Add(new Elementbase(x.Name, x.Name));
                }
            }
        }
        /// <summary>
        /// Помечает папку, как содержащую эллементы
        /// </summary>
        public void ElementCreatorWithElement()
        {
            if (System.IO.Directory.Exists(this.Path))
            {
                try
                {
                    if (this.Elements == null || this.Elements.Count == 0)
                    {
                        this.ElementCreator();
                    }
                    if  (System.IO.Directory.EnumerateFileSystemEntries(this.Path).Count() != 0)
                    {
                        this.Elements.Add(new Elementbase());
                    }                    
                }
                catch
                {

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
