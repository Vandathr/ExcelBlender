using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QRDataExtractor
{
    internal class Model
    {
        private readonly ExcelLoader LoaderOfExcel;
        private readonly ExcelSaver SaverOfExcel = new ExcelSaver();
        private readonly DataBlender Blender = new DataBlender();

        private DataTable ParentTable;
        private DataTable ChildTable;
        private DataTable result;

        private string[] parentTableColumnNameN;
        private string[] childTableColumnNameN;


        public void LoadFilez(string pathToParentTable, string pathToChildTable)
        {
            ParentTable = LoaderOfExcel.LoadExcel(pathToParentTable, "ParentTable");

            parentTableColumnNameN = new string[ParentTable.Columns.Count];

            for (int i = 0; i < ParentTable.Columns.Count; i++)
            {
                parentTableColumnNameN[i] = ParentTable.Columns[i].ColumnName;
            }

            ChildTable = LoaderOfExcel.LoadExcel(pathToChildTable, "ChildTable");

            childTableColumnNameN = new string[ChildTable.Columns.Count];

            for (int i = 0; i < ChildTable.Columns.Count; i++)
            {
                childTableColumnNameN[i] = ChildTable.Columns[i].ColumnName;
            }
        }

        public void BlendTablez() => result = Blender.Blend(ParentTable, ChildTable);
        public void SaveResult(string pathToSave) => SaverOfExcel.SaveExcel(pathToSave, result);


        public string[] GetParentTableColumnNameN() => parentTableColumnNameN;
        public string[] GetChildTableColumnNameN() => childTableColumnNameN;

        public void SetParentJoinColumnName(string columnName) => Blender.SetParentJoinColumnName(columnName);
        public void SetChildJoinColumnName(string columnName) => Blender.SetChildJoinColumnName(columnName);

        public void SetSelectedColumnz(string[] columnN)
        {
            for(int i = 0; i < columnN.Length; i++)
            {
                Blender.AddColumn(columnN[i], "System.String");

                if(Array.Find(parentTableColumnNameN, e => e == columnN[i]) != null)
                {
                    Blender.AddParentTableToResultCorespondence(columnN[i], columnN[i]);
                }else if(Array.Find(childTableColumnNameN, e => e == columnN[i]) != null)
                {
                    Blender.AddChildTableToResultCorespondence(columnN[i], columnN[i]);
                }

            }
        }

        public Model()
        {
            LoaderOfExcel = new ExcelLoader(new DefaultFill());

            //Blender.AddColumn("ID", "System.String");
            //Blender.AddColumn("Сумма", "System.Double");
            //Blender.AddColumn("Статус", "System.String");
            //Blender.AddColumn("Время платежа", "System.String");
            //Blender.AddColumn("РМК ID", "System.String");
            //Blender.AddColumn("TID", "System.String");
            //Blender.AddColumn("Номер Магазина", "System.String");
            //Blender.AddColumn("Номер кассы", "System.String");

            //Blender.AddParentTableToResultCorespondence("Номер Магазина", "№ Магазина");
            //Blender.AddParentTableToResultCorespondence("Номер кассы", "№ касы");

            //Blender.AddChildTableToResultCorespondence("ID", "ID");
            //Blender.AddChildTableToResultCorespondence("Сумма", "Сумма");
            //Blender.AddChildTableToResultCorespondence("Статус", "Статус");
            //Blender.AddChildTableToResultCorespondence("Время платежа", "Время платежа");
            //Blender.AddChildTableToResultCorespondence("РМК ID", "РМК ID");
            //Blender.AddChildTableToResultCorespondence("TID", "TID");

        }

    }
}
