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
    public partial class UpdateTaskForm : Form
    {

        private int taskID;

        private string SelectQuery
        {
            get
            {
                return
                "SELECT ID, DueDate, DueDateEnforced, Ongoing, " +
                "   TaskName, TaskDescription, Complete, Dismissed " +
                "   FROM Tasks " +
                "   WHERE ID=" + taskID;
            }
        }
        private string ExitMessage { get; set; }
        public UpdateTaskForm()
        {
            InitializeComponent();

            ExitMessage = null;

            // Gets the Task ID.
            taskID = (int)Navigation.TakeArg();

            // Gets the Task's current data.
            QueryResult result;

            try
            {
                result = new QueryResult(SelectQuery);
            }
            catch (Exception ex)
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show(ex.Message);
                return;
            }

            DateTime? dueDate;
            bool dueDateEnforced, ongoing;
            string taskName, taskDesc;
            bool complete, dismissed;

            // Retrieves the current data from the query result.
            try
            {
                dueDate =           (DateTime?)Util.TreatDatabaseNullAsNull(result.GetValue(0, 1));
                dueDateEnforced =   Util.TreatNullAsFalse(result.GetValue(0, 2));
                ongoing =           Util.TreatNullAsFalse(result.GetValue(0, 3));
                taskName =          (string)result.GetValue(0, 4);
                taskDesc =          (string)result.GetValue(0, 5);
                complete =          Util.TreatNullAsFalse(result.GetValue(0, 6));
                dismissed =         Util.TreatNullAsFalse(result.GetValue(0, 7));
            }
            catch (Exception ex)
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show(ex.Message);
                return;
            }

            // Initializes form with current data.
            txtDueDate.Text = dueDate.HasValue ? $"{dueDate.Value.Year}-{dueDate.Value.Month}-{dueDate.Value.Day}" : "";
            cboDueDateEnforced.Checked = dueDateEnforced;
            cboOngoing.Checked = ongoing;
            txtTaskName.Text = taskName;
            txtTaskDescription.Text = taskDesc;
            cboComplete.Checked = complete;
            cboDismissed.Checked = dismissed;

        }

        private void UpdateTaskForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Navigation.ReturnHome(ExitMessage);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private struct TaskUpdateData
        {
            public string DueDate { get; set; }
            public string DueDateEnforced { get; set; }
            public string Ongoing { get; set; }
            public string TaskName { get; set; }
            public string Comments { get; set; }
            public string Complete { get; set; }
            public string Dismissed { get; set; }
        }

        /// <summary>
        /// Gets input from the form. Displays a message box and returns null if input is invalid.
        /// </summary>
        /// <returns></returns>
        private TaskUpdateData? GetInput()
        {
            string dateInput = txtDueDate.Text;
            // If dateInput is empty, the DueDate will be NULL, and the date validation will be ignored.
            if (dateInput == "")
            {
                dateInput = "NULL";
            }
            else
            {
                // Splits the date into its components.
                string[] dateComponents = dateInput.Split('-');
                // There should be three components.
                if (dateComponents.Length != 3)
                {
                    MessageBox.Show("Date should have format YYYY-MM-DD");
                    return null;
                }
                // Tries to parse the three components.
                if (int.TryParse(dateComponents[0], out int year)
                    && int.TryParse(dateComponents[1], out int month)
                    && int.TryParse(dateComponents[2], out int day))
                {
                    // Rebuilds the date string.
                    dateInput = $"'{year}-{month}-{day}'";
                }
                else
                {
                    MessageBox.Show("Date components must be integers");
                    return null;
                }
            }

            TaskUpdateData taskUpdateData = new TaskUpdateData
            {
                DueDate = dateInput,
                DueDateEnforced = cboDueDateEnforced.Checked ? "1" : "NULL",
                Ongoing = cboOngoing.Checked ? "1" : "NULL",
                TaskName = Util.WithoutQuotes(txtTaskName.Text),
                Comments = Util.WithoutQuotes(txtTaskDescription.Text),
                Complete = cboComplete.Checked ? "1" : "0",
                Dismissed = cboDismissed.Checked ? "1" : "0"
            };
            return taskUpdateData;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            TaskUpdateData? taskUpdateDataInput = GetInput();
            if (taskUpdateDataInput == null)
            {
                return;
            }

            TaskUpdateData taskUpdateData = taskUpdateDataInput.Value;

            // Builds a SQL statement out of the input data.
            string statement = "UPDATE Tasks SET " +
                $"DueDate={taskUpdateData.DueDate}, " +
                $"DueDateEnforced={taskUpdateData.DueDateEnforced}, " +
                $"Ongoing={taskUpdateData.Ongoing}, " +
                $"TaskName='{taskUpdateData.TaskName}', " +
                $"TaskDescription='{taskUpdateData.Comments}', " +
                $"Complete={taskUpdateData.Complete}, " +
                $"Dismissed={taskUpdateData.Dismissed} " +
                $"WHERE ID={taskID}";

            try
            {
                int rowsAffected = LogicalDatabase.PerformSQLNonQuery(statement);
                ExitMessage = "Statement Executed. Rows affected: " + rowsAffected;
                Close();
            }
            catch (Exception ex)
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
