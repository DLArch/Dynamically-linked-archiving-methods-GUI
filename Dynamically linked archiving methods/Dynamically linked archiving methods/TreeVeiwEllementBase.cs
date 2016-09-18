using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamically_linked_archiving_methods
{
    public class TreeVeiwEllementBase
    {
        public TreeVeiwEllementBase(string Path, Object Children)
        {
            this.Path = Path;
            this.Name = String.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
            if (Children != null)
                this.Children.Add(Children);
        }
        public TreeVeiwEllementBase(string Path)
        {
            this.Path = Path;
            //this.Name = String.Concat(Path.Reverse().TakeWhile(x => x != System.IO.Path.DirectorySeparatorChar).Reverse());
        }
        public string Path
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public List<object> Children
        {
            get;
            set;
        }
    }
}
