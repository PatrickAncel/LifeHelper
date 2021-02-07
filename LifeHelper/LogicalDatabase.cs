using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LifeHelper
{

    /// <summary>
    /// Provides the interface between the database and other classes.
    /// </summary>
    class LogicalDatabase
    {
        private const string CONNECTION_STRING = "Server=localhost;Database=ToDo;Trusted_Connection=True;";

        /// <summary>
        /// Executes a SQL statement that mutates the database. Returns the number of rows affected.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns>The number of rows affected.</returns>
        public static int PerformSQLNonQuery(string statement)
        {
            int numRowsAffected;

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                // Opens the connection.
                connection.Open();

                // Builds a SQLCommand to execute.
                SqlCommand command = new SqlCommand(statement, connection);

                // Executes the statement.
                numRowsAffected = command.ExecuteNonQuery();
            }

            return numRowsAffected;
        }

        /// <summary>
        /// Executes a SQL query and returns the resulting table as a 2D array of objects.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Obsolete("Use the QueryResult constructor instead of this method")]
        public static Relation PerformSQLQuery(string tableName, string[] colNames, Type[] colTypes, string query)
        {
            Relation table;

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {

                // Builds the SQLCommand to execute.
                SqlCommand command = new SqlCommand(query, connection);

                // Opens the connection.
                connection.Open();

                // Creates the reader.
                SqlDataReader reader = command.ExecuteReader();

                table = new Relation(tableName, colNames, colTypes, reader);

                // Closes the reader.
                reader.Close();
            }

            return table;
        }

        //public static Relation PerformSQLQuery(string query)
        //{
        //    using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        //    {
        //        // Builds the SQLCommand to execute.
        //        SqlCommand command = new SqlCommand(query, connection);

        //        // Opens the connection.
        //        connection.Open();

        //        // Creates the reader.
        //        SqlDataReader reader = command.ExecuteReader();

        //        reader.G
        //    }
        //}
    }
}
