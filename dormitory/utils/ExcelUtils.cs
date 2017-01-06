using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Dormitory
{
    class ExcelUtils
    {
        private string path;
        private List<List<string>> data;
        public ExcelUtils(string path, List<List<string>> data)
        {
            this.path = path;
            this.data = data;
        }
        public void ReadExcelData()
        {
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Open(path);
                ws = wb.Worksheets.Item[1];
                Excel.Range mg = ws.UsedRange;
                object[,] data = mg.Value;
                for (int r = 1, i = 0; r <= data.GetLength(0); r++)
                {
                    for (int c = 1; c <= data.GetLength(1); c++)
                    {
                        if (data[r, c] == null)
                            continue;
                        Console.Write("|" + data[r, c]);
                    }
                    Console.WriteLine();
                }
                wb.Close(true);
                excelApp.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WriteXlsx()
        {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;
            try
            {
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                oSheet.Cells[1, 1] = "학번";
                oSheet.Cells[1, 2] = "이름";
                oSheet.Cells[1, 3] = "집가고싶어";
                oSheet.Cells[1, 4] = "집가는 비용";

                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                string[,] saNames = new string[2, 2];

                saNames[0, 0] = "20119";
                saNames[1, 0] = "20302";
                saNames[0, 1] = "갓원태";
                saNames[1, 1] = "존못 경달";

                oSheet.get_Range("A2", "C3").Value2 = saNames;

                oRng = oSheet.get_Range("C2", "C6");
                oRng.Formula = "=(IF($B3=\"존못 경달\",TRUE,FALSE))";
                oRng = oSheet.get_Range("D2", "D3");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs("D:\\TEST.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
