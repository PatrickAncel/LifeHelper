using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHelper
{
    class QueryResult
    {

        private const string CONNECTION_STRING = "Server=localhost;Database=ToDo;Trusted_Connection=True;";

        /// <summary>
        /// Holds the name, SQL type name, and C# Type of each column.
        /// </summary>
        private readonly Tuple<string, string, Type>[] schema;

        /// <summary>
        /// Holds the table records.
        /// </summary>
        private readonly object[][] data;

        public int RowCount { get { return data.Length; } }
        public int ColCount { get { return schema.Length; } }
        public string GetColumnName(int colIndex) => schema[colIndex].Item1;
        public string GetColumnSQLTypeName(int colIndex) => schema[colIndex].Item2;
        public Type GetColumnType(int colIndex) => schema[colIndex].Item3;
        public object GetValue(int rowIndex, int colIndex) => data[rowIndex][colIndex];

        public QueryResult(string query)
        {

            // Constructs the List that will hold the rows of the table.
            List<object[]> rows = new List<object[]>();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                // Builds the SQLCommand to execute.
                SqlCommand command = new SqlCommand(query, connection);

                // Opens the connection.
                connection.Open();

                // Creates the reader.
                SqlDataReader reader = command.ExecuteReader();

                // Gets the number of columns.
                int fieldCount = reader.FieldCount;

                // Initializes the schema array.
                schema = new Tuple<string, string, Type>[fieldCount];

                // Stores the structure (column information) of the table.
                for (int colIndex = 0; colIndex < fieldCount; colIndex++)
                {
                    schema[colIndex] = new Tuple<string, string, Type>(reader.GetName(colIndex),
                        reader.GetDataTypeName(colIndex), reader.GetFieldType(colIndex));
                }

                // Reads each row.
                while (reader.Read())
                {
                    // Casts the reader to a record.
                    IDataRecord record = reader;

                    // Constructs the array of fields.
                    object[] fields = new object[fieldCount];

                    // Adds the fields to the array.
                    for (int colIndex = 0; colIndex < fieldCount; colIndex++)
                    {
                        fields[colIndex] = record[colIndex];
                    }

                    // Adds the row to the List.
                    rows.Add(fields);
                }

                // Closes the reader.
                reader.Close();
            }

            // Converts the List to an array.
            data = rows.ToArray();
        }

    }
}
