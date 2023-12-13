using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QRDataExtractor
{
    internal class DataBlender
    {
        private readonly RelatedDataContainer ContainerOfResultsColumnz = new RelatedDataContainer();
        private readonly RelatedDataContainer ContainerChildTableToResultCorrespondentColumnz = new RelatedDataContainer();
        private readonly RelatedDataContainer ContainerParentTableToResultCorrespondentColumnz = new RelatedDataContainer();
        

        private readonly string relationName = "Relation";
        private string parentTableName;
        private string childTableName;
        private string resultTableName = "result";

        private string firstJoinColumnName;
        private string secondJoinColumnName;

        public DataTable Blend(DataTable ParentTable, DataTable ChildTable)
        {
            parentTableName = ParentTable.TableName;
            childTableName = ChildTable.TableName;

            DataTable result = CreateResultTable();

            DataSet Container = new DataSet();

            Container.Tables.Add(ParentTable);
            Container.Tables.Add(ChildTable);
            Container.Tables.Add(result);

            //DataRelation Relation = new DataRelation("PosListPlusRmkTable", PosList.Columns["RMK ID"], RmkTable.Columns["РМК ID"], false);

            Container.Relations.Add(relationName, ParentTable.Columns[firstJoinColumnName], ChildTable.Columns[secondJoinColumnName], false);

            DataRow ParentRow;
            DataRow addedRow;

            var childTableRowN = Container.Tables[childTableName].Rows;

            for (int i = 0; i < childTableRowN.Count; i++)
            {
                ParentRow = childTableRowN[i].GetParentRow(relationName);

                addedRow = result.NewRow();

                for (int j = 0; j < ContainerChildTableToResultCorrespondentColumnz.GetLength(); j++)
                {
                    addedRow[ContainerChildTableToResultCorrespondentColumnz.GetFirstElement(j)] = childTableRowN[i][ContainerChildTableToResultCorrespondentColumnz.GetSecondElement(j)];
                }


                if (ParentRow is not null)
                {
                    for (int j = 0; j < ContainerParentTableToResultCorrespondentColumnz.GetLength(); j++)
                    {
                        addedRow[ContainerParentTableToResultCorrespondentColumnz.GetFirstElement(j)] = ParentRow[ContainerParentTableToResultCorrespondentColumnz.GetSecondElement(j)];
                    }
                }


                result.Rows.Add(addedRow);
            }

            return Container.Tables[resultTableName];
        }


        public void SetParentJoinColumnName(string firstJoinColumnName) => this.firstJoinColumnName = firstJoinColumnName;
        public void SetChildJoinColumnName(string secondJoinColumnName) => this.secondJoinColumnName = secondJoinColumnName;

        public void AddColumn(string columnName, string typeName) => ContainerOfResultsColumnz.AddElementz(columnName, typeName);

        public void AddParentTableToResultCorespondence(string resultTableColumn, string parentTableColumn) => ContainerParentTableToResultCorrespondentColumnz.AddElementz(
            resultTableColumn, parentTableColumn);

        public void AddChildTableToResultCorespondence(string resultTableColumn, string childTableColumn) => ContainerChildTableToResultCorrespondentColumnz.AddElementz(
            resultTableColumn, childTableColumn);

        private DataTable CreateResultTable()
        {
            DataTable result = new DataTable(resultTableName);

            for(int i = 0; i < ContainerOfResultsColumnz.GetLength(); i++)
            {
                result.Columns.Add(ContainerOfResultsColumnz.GetFirstElement(i), Type.GetType(ContainerOfResultsColumnz.GetSecondElement(i)));
            }
            return result;
        }


    }
}
