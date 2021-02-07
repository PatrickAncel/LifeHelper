using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeHelper
{
    class Util
    {

        public static int GetDefaultColumnWidth(string colName, string dbType)
        {
            // The column must be at least wide enough to display the column name.
            int minWidth = (int)(colName.Length * Form1.ActiveForm.Font.Size);
            switch (dbType)
            {
                case "int":
                    return Math.Max(minWidth, (int)(4.2 * Form1.ActiveForm.Font.Size));
                case "bit":
                    return Math.Max(minWidth, (int)(4.2 * Form1.ActiveForm.Font.Size));
                case "varchar":
                    return Math.Max(minWidth, (int)(30 * Form1.ActiveForm.Font.Size));
                case "date":
                    return Math.Max(minWidth, (int)(12.5 * Form1.ActiveForm.Font.Size));
                default:
                    MessageBox.Show("Unexpected Type: " + dbType);
                    return Math.Max(minWidth, (int)(6.25 * Form1.ActiveForm.Font.Size));
            }
        }

        public static int GetColumnWidthProportion(string dbType)
        {
            switch (dbType)
            {
                case "int":
                    return (int)(4.2 * Form1.ActiveForm.Font.Size);
                case "bit":
                    return (int)(4.2 * Form1.ActiveForm.Font.Size);
                case "varchar":
                    return (int)(30 * Form1.ActiveForm.Font.Size);
                case "date":
                    return(int)(6 * Form1.ActiveForm.Font.Size);
                default:
                    MessageBox.Show("Unexpected Type: " + dbType);
                    return (int)(6.25 * Form1.ActiveForm.Font.Size);
            }
        }

        /// <summary>
        /// Formats one object as another object, based on its SQL type name.
        /// </summary>
        /// <param name="thing"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static object ReformatObject(object thing, string dbType)
        {
            try
            {
                if (typeof(DBNull).IsInstanceOfType(thing))
                {
                    return thing;
                }

                switch (dbType)
                {
                    case "date":
                        DateTime date = (DateTime)thing;
                        return date.Month + "/" + date.Day + "    " + date.DayOfWeek;
                    default:
                        return thing;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to format: " + thing + "\nSQL Type Name: " + dbType + "\n" + (typeof(DBNull).IsInstanceOfType(thing) ? "Is Null" : "Is Not Null"));
                return thing;
            }
        }

        public static bool TreatNullAsFalse(object thing)
        {
            if (typeof(DBNull).IsInstanceOfType(thing))
            {
                return false;
            }
            else
            {
                return (bool)thing;
            }
        }

        public static object TreatDatabaseNullAsNull(object thing)
        {
            if (typeof(DBNull).IsInstanceOfType(thing))
            {
                return null;
            }
            else
            {
                return thing;
            }
        }

        private static readonly char[] quotes = new char[] { '"', '\'' };

        /// <summary>
        /// Returns a version of <c>text</c> without quotes to prevent SQL injection.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithoutQuotes(string text)
        {
            string[] tokens = text.Split(quotes);
            string filteredText = "";
            for (int i = 0; i < tokens.Length; i++)
            {
                filteredText += tokens[i];
            }
            return filteredText;
        }



        public static class QueryBuilder
        {

            public static string SelectFromWhere(string colNames, string tableName, string condition)
            {
                return $" SELECT {colNames} FROM {tableName} WHERE {condition} ";
            }

            /// <summary>
            /// Set of methods whose parameters should be expressions that evaluate to true or false.
            /// </summary>
            public static class Conditional
            {
                public static string Not(string condition)
                {
                    return $" NOT ({condition}) ";
                }
                public static string And(string condition1, string condition2)
                {
                    return $" ({condition1}) AND ({condition2}) ";
                }
                public static string Or(string condition1, string condition2)
                {
                    return $" ({condition1}) OR ({condition2}) ";
                }
            }

        }

        public static class TaskQueries
        {

            public const string HELPFUL_COLUMN_LIST = " ID, DueDate, TaskName, TaskDescription ";
            public const string MODIFIER_IS_ACTIVE = " Dismissed=0 AND Complete=0 ";

            public const string MODIFIER_IS_HIGH_PRIORITY = " DueDate IS NULL AND DueDateEnforced IS NOT NULL ";
            public const string MODIFIER_IS_MED_PRIORITY = " DueDate IS NOT NULL ";
            public const string MODIFIER_IS_LOW_PRIORITY = " DueDate IS NULL AND DueDateEnforced IS NULL ";

            public static string MODIFIER_IS_INACTIVE => QueryBuilder.Conditional.Not(MODIFIER_IS_ACTIVE);

            // public const string MODIFIER_COLOR_STATE_IS_LATE = " DueDate < ";

            //public const string COLOR_STATE_COLUMN =
            //    "Special_ColorState = IIF()";


            public static string GET_ACTIVE_HIGH_PRIORITY_TASKS =>
                QueryBuilder.SelectFromWhere
                    (HELPFUL_COLUMN_LIST, "Tasks",
                    QueryBuilder.Conditional.And(MODIFIER_IS_ACTIVE, MODIFIER_IS_HIGH_PRIORITY))

                + " ORDER BY DueDate ";

            public static string GET_ACTIVE_MED_PRIORITY_TASKS =>
                QueryBuilder.SelectFromWhere
                    (HELPFUL_COLUMN_LIST, "Tasks",
                    QueryBuilder.Conditional.And(MODIFIER_IS_ACTIVE, MODIFIER_IS_MED_PRIORITY))

                + " ORDER BY DueDate ";

            public static string GET_ACTIVE_LOW_PRIORITY_TASKS =>
                QueryBuilder.SelectFromWhere
                    (HELPFUL_COLUMN_LIST, "Tasks",
                    QueryBuilder.Conditional.And(MODIFIER_IS_ACTIVE, MODIFIER_IS_LOW_PRIORITY))

                + " ORDER BY DueDate ";

            public static string GET_INACTIVE_TASKS =>
                QueryBuilder.SelectFromWhere(HELPFUL_COLUMN_LIST + ", Complete, Dismissed ", "Tasks", MODIFIER_IS_INACTIVE);
        }

        /// <summary>
        /// Version 3:
        /// <br></br>
        /// Tasks with enforced due dates that are Null get
        /// highest priority because their due dates exist but are unknown, and
        /// they are thus potentially urgent.
        /// <br></br>
        /// Tasks with unenforced due dates that are not Null get lowest priority
        /// because their due date doesn't matter.
        /// </summary>
        private const string GET_ACTIVE_TASKS_3 =
            "SELECT ID, DueDate, DueDateEnforced, Ongoing, " +
            "   Pleasant, TaskName, TaskDescription " +
            "   FROM Tasks WHERE Complete=0 AND Dismissed=0 " +
            "   ORDER BY IIF(DueDate IS NULL AND DueDateEnforced=1, 1, 0) DESC, " +
            "   IIF(DueDate IS NULL AND DueDateEnforced IS NULL, 1, 0), DueDate, DueDateEnforced DESC";


        public enum RowTimeState
        {
            PAST_YEAR,
            LATE,
            TODAY,
            NORMAL,
            FUTURE_YEAR,
            NONE
        }

        public static Color GetRowColor(RowTimeState state)
        {
            switch (state)
            {
                case RowTimeState.NONE:
                case RowTimeState.NORMAL:
                default:
                    return Color.White;

                case RowTimeState.PAST_YEAR:
                case RowTimeState.LATE:
                    return Color.LightPink;

                case RowTimeState.TODAY:
                    return Color.LightYellow;

                case RowTimeState.FUTURE_YEAR:
                    return Color.LightBlue;

            }
        }

        // Determines the time state of a particular (nullable) DateTime.
        private static RowTimeState DetermineTimeState(DateTime? dateTime, DateTime currentDateTime)
        {
            // If the value is null, the time state is NONE.
            if (!dateTime.HasValue)
            {
                return RowTimeState.NONE;
            }

            if (dateTime.Value.Year < currentDateTime.Year)
                return RowTimeState.PAST_YEAR;

            if (dateTime.Value.Year > currentDateTime.Year)
                return RowTimeState.FUTURE_YEAR;

            if (dateTime.Value.Date < currentDateTime.Date)
                return RowTimeState.LATE;

            if (dateTime.Value.Date == currentDateTime.Date)
                return RowTimeState.TODAY;

            return RowTimeState.NORMAL;
        }

        // Determines the RowTimeState of each row in a QueryResult by comparing the row's time value
        // to the current time. The column index used to determine the row's time state is given in the
        // timeColumnIndex parameter.
        public static RowTimeState[] DetermineRowTimeStates(QueryResult queryResult, int timeColumnIndex)
        {
            // Makes sure that the time column index is a valid column index in the QueryResult.
            if (timeColumnIndex < queryResult.ColCount)
            {
                // Makes sure that the time column has the DateTime C# type.
                if (queryResult.GetColumnType(timeColumnIndex) == typeof(DateTime))
                {
                    RowTimeState[] rowTimeStates = new RowTimeState[queryResult.RowCount];

                    // Gets the current date and time.
                    DateTime currentDateTime = DateTime.Now;

                    // Iterates over the rows in the QueryResult.
                    for (int i = 0; i < queryResult.RowCount; i++)
                    {
                        // Gets the raw time value for this row.
                        object rawTimeObject = queryResult.GetValue(i, timeColumnIndex);

                        // If the raw object has type DBNull, it is converted to regular null.
                        // Else, the object remains the same.
                        object timeObject = TreatDatabaseNullAsNull(rawTimeObject);

                        // Casts the result to nullable DateTime type.
                        DateTime? rowDateTime = (DateTime?)timeObject;

                        // Determines the row's time state using the nullable DateTime.
                        rowTimeStates[i] = DetermineTimeState(rowDateTime, currentDateTime);
                    }

                    return rowTimeStates;
                }
                else
                {
                    throw new ArgumentException($"Column {timeColumnIndex} does not have C# type DateTime.");
                }
            }
            else
            {
                throw new ArgumentException($"The time column index {timeColumnIndex} is not valid; there are only {queryResult.ColCount} column(s).");
            }
        }

    }
}
