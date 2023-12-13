using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace QRDataExtractor
{
    internal class ExcelSaver
    {
        private Excel.Application ExcelCom;
        private Excel.Workbook WorkbookOfExcel;
        private Excel.Worksheet SheetOfExcel;

        public void SaveExcel(string path, DataTable Source)
        {
            ExcelCom = new Excel.Application();
            ExcelCom.SheetsInNewWorkbook = 1;

            ExcelCom.Workbooks.Add(Type.Missing);

            WorkbookOfExcel = ExcelCom.Workbooks[1];

            SheetOfExcel = (Excel.Worksheet)WorkbookOfExcel.Worksheets.Item[1];

            FillData(Source);

            WorkbookOfExcel.SaveAs(path);
            WorkbookOfExcel.Close();

            Marshal.ReleaseComObject(ExcelCom);

        }


        private void FillData(DataTable Source)
        {
            for (int i = 0; i < Source.Columns.Count; i++)
            {
                SheetOfExcel.Cells[1, i + 1] = Source.Columns[i].ColumnName;
            }


            for (int i = 0; i < Source.Rows.Count; i++)
                for (int j = 0; j < Source.Columns.Count; j++)
                {
                    ((Excel.Range)SheetOfExcel.Cells[i + 2, j + 1]).NumberFormat = "@";
                    SheetOfExcel.Cells[i + 2, j + 1] = Source.Rows[i].ItemArray[j];
                }

            SheetOfExcel.Columns.AutoFit();

        }
    }
}
