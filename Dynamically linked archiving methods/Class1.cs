using System;

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
    }
    public Elementbase(string Path)
    {
        this.Path = Path;
        this.Name = string.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
    }
    public string Name
    {
        get;
        set;
    }
    public string Path
    {
        get;
        set;
    }
    public ObservableCollection<Elementbase> Elements
    {
        get;
        set;
    }
}
