using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace QRDataExtractor
{
    internal interface IDataFillLogic
    {
        DataTable FillData(Excel.Worksheet SheetOfExcel, string tableName);
    }
}
