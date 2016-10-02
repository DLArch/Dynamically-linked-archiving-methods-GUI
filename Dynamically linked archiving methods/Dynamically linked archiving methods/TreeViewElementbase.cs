using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dynamically_linked_archiving_methods
{
    public class Elementbase
    {
        public Elementbase(string Name, string Path, ObservableCollection<Elementbase> Elements)
        {
            this.Icon = DefaultIcon;
            this.Path = Path;
            this.Name = Name;
            this.Elements = Elements;
        }
        public Elementbase(string Name, string Path)
        {
            this.Icon = DefaultIcon;
            this.Path = Path;
            this.Name = Name;
            this.ElementCreatorWithElement();
        }
        public Elementbase(string Name, string Path, string Icon)
        {
            this.Icon = new Uri(Icon);
            this.Path = Path;
            this.Name = Name;
            this.ElementCreatorWithElement();
        }
        /// <summary>
        /// Выделяет имя из пути и заполняет поля
        /// И особое Icon
        /// </summary>
        /// <param name="Path"></param>
        public Elementbase(string Path, System.Drawing.Bitmap Icon)
        {
            this.Icon = DefaultIcon;
            this.Path = Path;
            this.Name = string.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
            this.ElementCreatorWithElement();
        }
        /// <summary>
        /// Выделяет имя из пути и заполняет поля
        /// </summary>
        /// <param name="Path"></param>
        public Elementbase(string Path)
        {
            this.Icon = DefaultIcon;
            this.Path = Path;
            this.Name = string.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
            this.ElementCreatorWithElement();
        }
        public Elementbase()
        {
            this.Icon = DefaultIcon;
            Name = "Undefined";
        }
        /// <summary>
        /// Прогружает все папки на компьютере
        /// Выдергивает значек папку/файла
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key"></param>
        ///
        /// TODO: Засунуть весь метод в отдельный поток с минимальным приоритетом
        ///
        public Elementbase(string Path, ConstructorMode key, string Name = "")
        {
            this.Icon = DefaultIcon;
            switch (key)
            {
                case ConstructorMode.MakeIcon:
                    this.Path = Path;
                    if (Name == "")
                    {
                        this.Name = string.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
                    }
                    else
                    {
                        this.Name = Name;
                    }
                    if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + @"res"))
                    {
                        System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + @"res");
                    }
                    string PathTIF = AppDomain.CurrentDomain.BaseDirectory + @"res" + System.IO.Path.DirectorySeparatorChar;
                    if (System.IO.File.Exists(this.Path))
                    {
                        PathTIF += string.Concat(this.Name.Reverse().TakeWhile(x => (x + "") != ".").Reverse());
                    }
                    else
                    {
                        PathTIF += @"Folder";
                    }
                    if (!System.IO.Directory.Exists(PathTIF))
                    {
                        System.IO.Directory.CreateDirectory(PathTIF);
                    }
                    PathTIF += System.IO.Path.DirectorySeparatorChar;
                    System.Drawing.Icon CIcon = new DMaker().DIMaker(Path);
                    bool EquFiles = false;
                    if (CIcon != null)
                    {
                        foreach (string x in System.IO.Directory.EnumerateFiles(PathTIF))
                        {
                            if (EquallTTwoIcons(new System.Drawing.Bitmap(x), CIcon.ToBitmap()))
                            {
                                PathTIF = x;
                                EquFiles = true;
                                break;
                            }
                        }
                    }
                    if (!EquFiles)
                    {
                        if (!System.IO.File.Exists(PathTIF + System.IO.Directory.EnumerateFiles(PathTIF).Count().ToString()))
                        {
                            PathTIF += System.IO.Directory.EnumerateFiles(PathTIF).Count().ToString();
                            if (CIcon != null)
                            {
                                CIcon.ToBitmap().Save(PathTIF);
                            }
                            else
                            {
                                PathTIF = DefaultIcon.AbsolutePath;
                            }
                        }
                    }
                    this.Icon = new System.Uri(PathTIF);
                    this.ElementCreatorWithElement();
                    break;
                case ConstructorMode.MakeAllTree:
                    this.TreeMaker();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// При True вызывает Elementbase.CreateDrives()
        /// </summary>
        /// <param name="CreateDrivesBool"></param>
        public Elementbase(bool CreateDrivesBool)
        {
            this.Icon = DefaultIcon;
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
                        this.Elements.Add(new Elementbase(x, ConstructorMode.MakeIcon));
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
        /// Добавляет специальные папки, набор которых нстроен будет из файла(пока в основной программе)
        /// </summary>
        /// <param name="Folder"></param>
        public void CreateSpecialDirectoris(System.Environment.SpecialFolder Folder)
        {
            if (this.Elements == null)
            {
                this.ElementCreator();
            }
            this.Elements.Add(new Elementbase(System.Environment.GetFolderPath(Folder), (int)ConstructorMode.MakeIcon));
        }
        /// <summary>
        /// Добавляет в поле Elements данные о дисках
        /// </summary>
        public void CreateDrives()
        {
            if (this.Elements == null)
            {
                this.ElementCreator();
            }
            foreach (System.IO.DriveInfo x in System.IO.DriveInfo.GetDrives())
            {
                if (System.IO.Directory.Exists(x.Name))
                {
                    //this.Elements.Add(new Elementbase(x.Name, x.Name));
                    this.Elements.Add(new Elementbase(x.Name, ConstructorMode.MakeIcon, x.Name));
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
                    if (System.IO.Directory.EnumerateFileSystemEntries(this.Path).Count() != 0)
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
        /// Возвращает true, если файлы одинаковы.
        /// </summary>
        /// <param name="FirstIcon"></param>
        /// <param name="SecondIcon"></param>
        /// <returns></returns>
        public bool EquallTTwoIcons(System.Drawing.Bitmap FirstIcon, System.Drawing.Bitmap SecondIcon)
        {
            if (!((FirstIcon.Width == SecondIcon.Width) && (FirstIcon.Height == SecondIcon.Height)))
            {
                return false;
            }
            for (int istr = 0; istr < FirstIcon.Width; istr++)
            {
                for (int istl = 0; istl < FirstIcon.Height; istl++)
                {
                    if (FirstIcon.GetPixel(istr, istl) != SecondIcon.GetPixel(istr, istl))
                    {
                        return false;
                    }
                }
            }
            return true;
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
        /// Иконка приложения
        /// </summary>
        public System.Uri Icon
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
        private System.Uri DefaultIcon = new System.Uri(AppDomain.CurrentDomain.BaseDirectory + @"load_error.bmp");
    }
    /// <summary>
    /// It's magic. Don't touch!
    /// </summary>
    public class DMaker
    {
        //Constants flags for SHGetFileInfo 
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon

        //Struct used by SHGetFileInfo function
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]

        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public System.Drawing.Icon DIMaker(string Path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();

            SHGetFileInfo(Path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
            
            if (File.Exists(Path) || Directory.Exists(Path))
            {
                return System.Drawing.Icon.FromHandle(shinfo.hIcon);
            }
            else
            {
                return null;
            }
        }

        public DMaker()
        {

        }
    }
}