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
    public partial class CreateTaskForm : Form
    {

        private string ExitMessage { get; set; }

        public CreateTaskForm()
        {
            InitializeComponent();

            ExitMessage = null;

            // Initializes the DueDate textbox with today's date.
            DateTime now = DateTime.Now;
            txtDueDate.Text = $"{now.Year}-{now.Month}-{now.Day}";
        }

        private void CreateTaskForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Navigation.ReturnHome(ExitMessage);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private struct TaskData
        {
            public string DueDate { get; set; }
            public string DueDateEnforced { get; set; }
            public string Ongoing { get; set; }
            public string TaskName { get; set; }
            public string Comments { get; set; }
        }

        /// <summary>
        /// Gets input from the form. Displays a message box and returns null if input is invalid.
        /// </summary>
        /// <returns></returns>
        private TaskData? GetInput()
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
                if (   int.TryParse(dateComponents[0], out int year)
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

            TaskData taskData = new TaskData
            {
                DueDate = dateInput,
                DueDateEnforced = cboDueDateEnforced.Checked ? "1" : "NULL",
                Ongoing = cboOngoing.Checked ? "1" : "NULL",
                TaskName = Util.WithoutQuotes(txtTaskName.Text),
                Comments = Util.WithoutQuotes(txtTaskDescription.Text)
            };
            return taskData;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            TaskData? taskDataInput = GetInput();
            if (taskDataInput == null)
            {
                return;
            }

            TaskData taskData = taskDataInput.Value;

            //MessageBox.Show($"Date: {taskData.DueDate}\nEnforced: {taskData.DueDateEnforced}\nOngoing: {taskData.Ongoing}\nTask Name: {taskData.TaskName}\nComments: {taskData.Comments}");

            // Builds a SQL query out of the input data.
            string statement = "INSERT INTO Tasks (DueDate, DueDateEnforced, Ongoing, Complete, Dismissed, TaskName, TaskDescription) " +
            $"VALUES ({taskData.DueDate}, {taskData.DueDateEnforced}, {taskData.Ongoing}, 0, 0, '{taskData.TaskName}', '{taskData.Comments}')";
            
            //MessageBox.Show(statement);
            
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
