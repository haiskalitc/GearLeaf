using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawReviewAmazon
{
    public class FileHandle
    {
        public List<Product> GetProductFromFile(string path)
        {
            string des = File.ReadAllText(Environment.CurrentDirectory + "\\id.txt");
            try
            {
                var ds = new List<Product>();

                HSSFWorkbook hssfwb;
                using (FileStream file = new FileStream(@"c:\test.xls", FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new HSSFWorkbook(file);
                }

                ISheet sheet = hssfwb.GetSheet(0);
                for (int row = 0; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                    {
                        MessageBox.Show(string.Format("Row {0} = {1}", row, sheet.GetRow(row).GetCell(0).StringCellValue));
                    }
                }


                return ds;
            }
            catch
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}