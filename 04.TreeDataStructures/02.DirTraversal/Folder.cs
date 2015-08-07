using System.Collections.Generic;

namespace DirTraversal
{
    public class Folder
    {
        public Folder(string name)
        {
            this.Name = name;
            this.ChildFolders = new List<Folder>();
            this.Files = new List<File>();
        }

        public string Name { get; set; }

        public IList<Folder> ChildFolders { get; private set; }

        public IList<File> Files { get; private set; }
    }
}
