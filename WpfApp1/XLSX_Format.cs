using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OfficeOpenXml;
using System.IO;




namespace GPD_Test
{
    internal class XLSX_Format
    {

        public static void SetLicense()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static List<Anomaly> GetData(string path)
        {

            List<Anomaly> data = new();

            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(path)))
            {
                var Worksheet = xlPackage.Workbook.Worksheets.First(); 

                var totalRows = Worksheet.Dimension.End.Row;

                var totalColumns = Worksheet.Dimension.End.Column;

                if(totalColumns != 6)
                {
                    throw new Exception("Неверный формат данных");
                }

                var sb = new StringBuilder(); 

                for (int rowNum = 2; rowNum <= totalRows; rowNum++) 
                {

                    Anomaly anomaly = new()
                    {
                        Name = (string)(Worksheet.Cells[rowNum, 1].Value),

                        Distance = (double)(Worksheet.Cells[rowNum, 2].Value),

                        Angle = (double)(Worksheet.Cells[rowNum, 3].Value),

                        Width = (double)(Worksheet.Cells[rowNum, 4].Value),

                        Height = (double)(Worksheet.Cells[rowNum, 5].Value),

                        IsDefect = YesNoToBool((string)Worksheet.Cells[rowNum, 6].Value)
                    };

                    data.Add(anomaly);
                }

                return data;

            }
        }

        private static bool YesNoToBool(string txt)
        {
            if (txt.ToLower() == "yes") 
                return true;

            else return false;            
            
        }

    }
}
