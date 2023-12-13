using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection.Metadata.Ecma335;

namespace QRDataExtractor
{
    internal class ExcelLoader
    {
        private readonly IDataFillLogic Logic;

        private Excel.Application ExcelCom;
        private Excel.Workbook WorkbookOfExcel;
        private Excel.Worksheet SheetOfExcel;

        public DataTable LoadExcel(string path, string tableName)
        {
            ExcelCom = new Excel.Application();

            WorkbookOfExcel = ExcelCom.Workbooks.Open(path);

            SheetOfExcel = (Excel.Worksheet)WorkbookOfExcel.Worksheets.Item[1];

            var result = Logic.FillData(SheetOfExcel, tableName);

            WorkbookOfExcel.Close();

            ExcelCom.Quit();

            Marshal.ReleaseComObject(ExcelCom);

            return result;

        }


        public ExcelLoader(IDataFillLogic Logic)
        {
            this.Logic = Logic;
        }



    }
}
