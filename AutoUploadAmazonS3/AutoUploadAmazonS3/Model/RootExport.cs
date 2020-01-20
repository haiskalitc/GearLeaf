using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUploadAmazonS3.Model
{
    public class RootExport
    {
        public string Name { get; set; }
        public List<Export> DanhSach { get; set;}
        public List<string> Link { get; set; }

    }
}
