using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeHelper
{
    class Relation
    {

        // These parallel arrays and the Name constitute the Relation's schema.
        private readonly string[] colNames;
        private readonly Type[] colTypes;
        public string Name { get; }

        private readonly object[][] data;

        public string GetColName(int index) => colNames[index];
        public Type GetColType(int index) => colTypes[index];
        public dynamic GetValue(int rowIndex, int colIndex) => data[rowIndex][colIndex];

        public int ColCount { get { return colNames.Length; } }
        public int RowCount { get { return data.Length; } }

        public Relation(string name, string[] colNames, Type[] colTypes, SqlDataReader reader)
        {
            if (colNames.Length != colTypes.Length)
            {
                throw new Exception();
            }

            Name = name;
            this.colNames = new string[colNames.Length];
            this.colTypes = new Type[colTypes.Length];

            // Copies the contents of the arrays.
            for (int i = 0; i < colNames.Length; i++)
            {
                this.colNames[i] = colNames[i];
                this.colTypes[i] = colTypes[i];
            }

            // Creates a List to hold the rows of the table.
            List<object[]> rows = new List<object[]>();

            while (reader.Read())
            {
                IDataRecord record = reader;

                // Ensures that the record has the correct number of fields.
                if (record.FieldCount != colNames.Length)
                {
                    throw new Exception();
                }

                // Constructs the array of fields.
                object[] fields = new object[colNames.Length];

                // Adds the fields to the array.
                for (int i = 0; i < colNames.Length; i++)
                {
                    // MessageBox.Show(record[i] + " " + record.GetDataTypeName(i));
                    fields[i] = record[i];
                }

                // Adds the row to the List.
                rows.Add(fields);
            }

            // Converts the List to an array.
            data = rows.ToArray();
        }

        
    }
}
