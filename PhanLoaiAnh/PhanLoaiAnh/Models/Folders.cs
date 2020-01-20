using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanLoaiAnh.Models
{
    public class Folders
    {
        private DirectoryInfo _folder;
        private List<Folders> _subFolders;
        private List<MyFileInfo> _files;

        public Folders(string path)
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

        public List<MyFileInfo> Files
        {
            get
            {
                try
                {
                    if (this._files == null)
                    {
                        this._files = new List<MyFileInfo>();
                        FileInfo[] fi = this._folder.GetFiles();
                        for (int i = 0; i < fi.Length; i++)
                        {
                            this._files.Add(new MyFileInfo()
                            {
                                Id = i.ToString(),
                                File = fi[i]
                            });
                        }
                    }
                    return this._files;
                }
                catch
                {
                    return new List<MyFileInfo>();
                }
            }
        }

        public List<Folders> SubFolders
        {
            get
            {
                try
                {
                    if (this._subFolders == null)
                    {
                        this._subFolders = new List<Folders>();
                        DirectoryInfo[] di = this._folder.GetDirectories();
                        for (int i = 0; i < di.Length; i++)
                        {
                            Folders newFolder = new Folders(FullPath);
                            newFolder.FullPath = di[i].FullName;
                            this._subFolders.Add(newFolder);

                        }
                    }
                    return this._subFolders;
                }
                catch
                {
                    return new List<Folders>();
                }
            }
        }
    }
}
