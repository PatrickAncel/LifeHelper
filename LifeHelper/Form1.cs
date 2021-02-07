using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeHelper
{
    public partial class Form1 : Form
    {

        private const string GET_ACTIVE_TASKS = "SELECT ID, DueDate, DueDateEnforced, Ongoing, MattersToOthers, " +
                "MattersToSelf, Pleasant, TaskName, TaskDescription FROM Tasks WHERE Complete=0 AND Dismissed=0 " +
                "ORDER BY DueDate";


        private const string GET_ACTIVE_TASKS_2 =
            "SELECT ID, DueDate, DueDateEnforced, Ongoing, " +
            "   Pleasant, TaskName, TaskDescription " +
            "   FROM Tasks WHERE Complete=0 AND Dismissed=0 " +
            "   ORDER BY IIF(DueDate IS NULL, 1, 0), DueDate, DueDateEnforced DESC";

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
            "   TaskName, TaskDescription " +
            "   FROM Tasks WHERE Complete=0 AND Dismissed=0 " +
            "   ORDER BY IIF(DueDate IS NULL AND DueDateEnforced=1, 1, 0) DESC, " +
            "   IIF(DueDate IS NULL AND DueDateEnforced IS NULL, 1, 0), DueDate, DueDateEnforced DESC";

        private const string SELF_JOIN_QUERY_1 =
            "SELECT T1.ID, T1.TaskName, (1 - T1.Complete) * (1 - T2.Dismissed) AS TaskIsActive " +
            "FROM Tasks T1, Tasks T2 " +
            "WHERE T1.ID = T2.ID";

        private const string SELF_JOIN_QUERY_2 =
            "SELECT T1.ID, T1.TaskName, IIF(T1.Complete=1 OR T2.Dismissed=1, 0, 1) AS TaskIsActive " +
            "FROM Tasks T1, Tasks T2 " +
            "WHERE T1.ID = T2.ID";

        private const string GET_TASK_COUNTS =
            "SELECT (1 - Complete) * (1 - Dismissed) AS TaskIsActive, COUNT(*) AS TaskCount FROM Tasks GROUP BY (1 - Complete) * (1 - Dismissed)";

        private const string GET_TASK_COUNTS_2 =
            "SELECT COUNT(*) AS TaskCount FROM Tasks WHERE Complete=0 AND Dismissed=0";

        private Util.RowTimeState[] RowTimeStates { get; set; } = new Util.RowTimeState[0];


        public Form1()
        {
            InitializeComponent();
            Navigation.HomeForm = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Task.InsertTest();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            string tableName = "Tasks";

            string[] colNames = new string[]
                {
                    "ID",
                    "DueDate",
                    "DueDateEnforced",
                    "Ongoing",
                    "MattersToOthers",
                    "MattersToSelf",
                    "Pleasant",
                    "Complete",
                    "Dismissed",
                    "TaskName",
                    "TaskDescription"
                };

            Type[] colTypes = new Type[] {typeof(int), typeof(DateTime), typeof(bool), typeof(bool), typeof(bool),
                typeof(bool), typeof(bool?), typeof(bool), typeof(bool), typeof(string), typeof(string)};

            // Adds the columns to the data grid view.
            for (int i = 0; i < colNames.Length; i++)
            {
                DataGridViewColumn col = new DataGridViewColumn
                {
                    HeaderText = colNames[i],
                    Name = colNames[i],
                    CellTemplate = new DataGridViewTextBoxCell()
                };
                dataGridView1.Columns.Add(col);
            }

            Relation table = LogicalDatabase.PerformSQLQuery(tableName, colNames, colTypes, "SELECT * FROM Tasks");
            
            for (int i = 0; i < table.RowCount; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                object[] tuple = new object[table.ColCount];

                for (int j = 0; j < table.ColCount; j++)
                {
                    dynamic field = table.GetValue(i, j);
                    tuple[j] = field;
                }
                dataGridView1.Rows.Add(tuple);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            string tableName = "Tasks";

            string[] colNames = new string[]
                {
                    "ID",
                    "DueDate",
                    "DueDateEnforced",
                    "Ongoing",
                    "MattersToOthers",
                    "MattersToSelf",
                    "Pleasant",
                    "TaskName",
                    "TaskDescription"
                };

            Type[] colTypes = new Type[] {typeof(int), typeof(DateTime), typeof(bool), typeof(bool), typeof(bool),
                typeof(bool), typeof(bool?), typeof(string), typeof(string)};

            // Adds the columns to the data grid view.
            for (int i = 0; i < colNames.Length; i++)
            {
                DataGridViewColumn col = new DataGridViewColumn
                {
                    HeaderText = colNames[i],
                    Name = colNames[i],
                    CellTemplate = new DataGridViewTextBoxCell(),
                    Width = 200
                };
                dataGridView1.Columns.Add(col);
            }

            Relation table = LogicalDatabase.PerformSQLQuery(tableName, colNames, colTypes, "SELECT ID, DueDate, DueDateEnforced, Ongoing, MattersToOthers, " +
                "MattersToSelf, Pleasant, TaskName, TaskDescription FROM Tasks WHERE Complete=0 AND Dismissed=0");

            for (int i = 0; i < table.RowCount; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                object[] tuple = new object[table.ColCount];

                for (int j = 0; j < table.ColCount; j++)
                {
                    dynamic field = table.GetValue(i, j);
                    tuple[j] = field;
                }
                dataGridView1.Rows.Add(tuple);
            }
        }

        private void PerformQueryAndDisplayResult(string query, bool useTimeStates = true)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 30;
            //MessageBox.Show("Row height: " + dataGridView1.RowTemplate.Height);

            QueryResult result;

            try
            {
                // Performs the query.
                result = new QueryResult(query);

                if (useTimeStates)
                {

                    int timeStateColumnIndex = -1;

                    // Iterates over the columns in the QueryResult to find the one called "DueDate".
                    for (int i = 0; i < result.ColCount; i++)
                    {
                        if (result.GetColumnName(i) == "DueDate")
                        {
                            timeStateColumnIndex = i;
                            break;
                        }
                    }

                    // If such a column exists...
                    if (timeStateColumnIndex >= 0)
                    {
                        // Determines the time state of each row in the QueryResult.
                        RowTimeStates = Util.DetermineRowTimeStates(result, timeStateColumnIndex);
                    }
                    else
                    {
                        // Clears the time states because no time state column exists.
                        RowTimeStates = new Util.RowTimeState[0];
                    }
                }
                else
                {
                    RowTimeStates = new Util.RowTimeState[0];
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show(ex.Message);
                return;
            }

            int totalWidth = dataGridView1.Size.Width - 35;
            int widthUsed = 0;

            // Adds the columns to the DataGridView.
            for (int i = 0; i < result.ColCount; i++)
            {
                string colName = result.GetColumnName(i);
                int width = Util.GetDefaultColumnWidth(colName, result.GetColumnSQLTypeName(i));
                //int width = Util.GetColumnWidthProportion(result.GetColumnSQLTypeName(i));

                DataGridViewColumn col = new DataGridViewColumn
                {
                    HeaderText = colName,
                    Name = colName,
                    CellTemplate = new DataGridViewTextBoxCell(),
                    Width = width
                };
                dataGridView1.Columns.Add(col);
                widthUsed += width;
            }

            if (widthUsed > 0)
            {
                // Gets the proportion of the DataGridView width that is being used.
                double propUsedWidth = ((double)widthUsed) / totalWidth;

                int newTotal = 0;

                // Scales each column so that the full DataGridView width is used.
                for (int i = 0; i < result.ColCount; i++)
                {
                    int newWidth = (int)Math.Round(dataGridView1.Columns[i].Width / propUsedWidth);
                    dataGridView1.Columns[i].Width = newWidth;
                    newTotal += newWidth;
                }

                //MessageBox.Show($"Old: {totalWidth}\nNew: {newTotal}");
            }


            // Adds the values to the DataGridView.
            for (int i = 0; i < result.RowCount; i++)
            {
                object[] record = new object[result.ColCount];

                for (int j = 0; j < result.ColCount; j++)
                {
                    record[j] = Util.ReformatObject(result.GetValue(i, j), result.GetColumnSQLTypeName(j));
                }
                dataGridView1.Rows.Add(record);
            }

            dataGridView1.ClearSelection();
            lblRowCount.Text = "Row Count: " + result.RowCount;
        }

        private void btnShowActiveTasks_Click(object sender, EventArgs e)
        {
            PerformQueryAndDisplayResult(GET_ACTIVE_TASKS_3);
        }

        private void btnOpenCreateTaskForm_Click(object sender, EventArgs e)
        {
            Navigation.LeaveHome(new CreateTaskForm());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PerformQueryAndDisplayResult(GET_TASK_COUNTS);
        }

        private void btnShowLow_Click(object sender, EventArgs e)
        {
            PerformQueryAndDisplayResult(Util.TaskQueries.GET_ACTIVE_LOW_PRIORITY_TASKS);
        }

        private void btnShowMed_Click(object sender, EventArgs e)
        {
            PerformQueryAndDisplayResult(Util.TaskQueries.GET_ACTIVE_MED_PRIORITY_TASKS);
        }

        private void btnShowHigh_Click(object sender, EventArgs e)
        {
            PerformQueryAndDisplayResult(Util.TaskQueries.GET_ACTIVE_HIGH_PRIORITY_TASKS);
        }

        private void btnShowInactive_Click(object sender, EventArgs e)
        {
            PerformQueryAndDisplayResult(Util.TaskQueries.GET_INACTIVE_TASKS, useTimeStates: false);
        }

        private void btnOpenUpdateTaskForm_Click(object sender, EventArgs e)
        {
            // Gets Task ID from this Form.
            if (int.TryParse(txtTaskIDInput.Text, out int taskID))
            {
                // Passes the Task ID and navigates to the next form.
                Navigation.PassArgs(taskID);
                Navigation.LeaveHome(new UpdateTaskForm());
            }
            else
            {
                MessageBox.Show("Invalid task ID");
            }
        }

        // Called before a row is painted.
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Tests if there is a time state for this row.
            if (RowTimeStates.Length > e.RowIndex)
            {
                // Gets the row's time state.
                Util.RowTimeState timeState = RowTimeStates[e.RowIndex];

                // Gets the color used to depict this time state.
                Color timeStateColor = Util.GetRowColor(timeState);

                // Sets the row's background color.
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = timeStateColor;
            }
        }
    }
}
