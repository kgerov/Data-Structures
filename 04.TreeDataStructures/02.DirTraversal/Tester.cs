using System.IO;
using System;

namespace DirTraversal
{
    class Tester
    {
        static void Main()
        {
            string StartDir = @"C:\Users\Konstantin\Desktop\Test";
            DirectoryInfo d = new DirectoryInfo(StartDir);

            Folder start = new Folder(d.Name);

            TraverseDir(d, start);

            Console.WriteLine("The end");
        }

        public static void TraverseDir(DirectoryInfo currentDirectory, Folder currentFolder)
        {
            foreach (var file in currentDirectory.GetFiles())
            {
                File FileToAdd = new File(file.Name, file.Length);
                currentFolder.Files.Add(FileToAdd);
            }

            foreach (var dir in currentDirectory.GetDirectories())
            {
                Folder FolderToAdd = new Folder(dir.Name);
                TraverseDir(new DirectoryInfo(currentDirectory + "\\" + dir.Name), FolderToAdd);
                currentFolder.ChildFolders.Add(FolderToAdd);
            }
        }
    }
}
