using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHelper
{
    class Task
    {
        public static int InsertTest()
        {
            string sql =
                "INSERT INTO Tasks (DueDate, DueDateEnforced, Ongoing, " +
                "MattersToOthers, MattersToSelf, Pleasant, Complete, " +
                "Dismissed, TaskName, TaskDescription) " +
                "VALUES ('1990-1-1', 0, " +
                "0, 0, 0, NULL, 0, 1, 'Test Task', " +
                "'This task is only a test.')";

            return LogicalDatabase.PerformSQLNonQuery(sql);
        }
    }
}
