using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace QRDataExtractor
{
    internal class DefaultFill : IDataFillLogic
    {
        public DataTable FillData(Excel.Worksheet SheetOfExcel, string tableName)
        {
            DataTable Result = new DataTable(tableName);

            int maxColumnzAmount = 15;
            int maxRowzAmount = 200;

            int columnzLimit = maxColumnzAmount;

            for (int i = 0; i < maxColumnzAmount; i++)
            {
                if (SheetOfExcel.Cells[1, i + 1].Value is null)
                {
                    columnzLimit = i;
                    break;
                }

                var Column = new DataColumn(SheetOfExcel.Cells[1, i + 1].Value, Type.GetType("System.String"));

                Result.Columns.Add(Column);

            }


            for (int i = 0; i < maxRowzAmount; i++)
            {
                if (SheetOfExcel.Cells[i + 2, 1].Value is null) break;

                var Row = Result.NewRow();
                var itemN = new object[columnzLimit];

                for (int j = 0; j < columnzLimit; j++)
                {
                    itemN[j] = SheetOfExcel.Cells[i + 2, j + 1].Value;
                }

                Row.ItemArray = itemN;

                Result.Rows.Add(Row);
            }



            return Result;
        }
    }
}
