using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRDataExtractor
{
    internal class RelatedDataContainer
    {
        private readonly List<string> columnNameN = new List<string>();
        private readonly List<string> typeNameN = new List<string>();


        public int GetLength() => columnNameN.Count;

        public string GetFirstElement(int index) => columnNameN[index];
        public string GetSecondElement(int index) => typeNameN[index];


        public void AddElementz(string columnName, string typeName)
        {
            columnNameN.Add(columnName);
            typeNameN.Add(typeName);
        }

    }
}
