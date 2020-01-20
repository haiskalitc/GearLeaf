using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AutoUploadAmazonS3.Model
{
    public class Folder
    {
        private DirectoryInfo _folder;
        private ArrayList _subFolders;
        private ArrayList _files;

        public Folder(string path)
        {
            this.FullPath = path;

        }

        public string Name
        {
            get
            {
                return this._folder.Name;
            }
            set
            {
            }
        }

        public string FullPath
        {
            get
            {
                return this._folder.FullName;
            }
            set
            {
                if (Directory.Exists(value))
                {
                    this._folder = new DirectoryInfo(value);
                }
                else
                {
                    throw new ArgumentException("Directory must exist", "full path");
                }
            }
        }

        public ArrayList Files
        {
            get
            {
                try
                {
                    if (this._files == null)
                    {
                        this._files = new ArrayList();
                        FileInfo[] fi = this._folder.GetFiles();
                        for (int i = 0; i < fi.Length; i++)
                        {
                            this._files.Add(fi[i]);
                        }
                    }
                    return this._files;
                }
                catch
                {
                    return new ArrayList();
                }
            }
        }

        public ArrayList SubFolders
        {
            get
            {
                try
                {
                    if (this._subFolders == null)
                    {
                        this._subFolders = new ArrayList();
                        DirectoryInfo[] di = this._folder.GetDirectories();
                        for (int i = 0; i < di.Length; i++)
                        {
                            Folder newFolder = new Folder(FullPath);
                            newFolder.FullPath = di[i].FullName;
                            this._subFolders.Add(newFolder);

                        }
                    }
                    return this._subFolders;
                }
                catch {
                    return new ArrayList();
                }
            }
        }
    }
}



