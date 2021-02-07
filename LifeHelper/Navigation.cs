using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeHelper
{
    class Navigation
    {
        public static Form HomeForm { get; set; }
        public static Form AwayForm { get; set; }

        private static Queue<object> arguments = new Queue<object>();

        public static void PassArgs(params object[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                Navigation.arguments.Enqueue(arguments[i]);
            }
        }

        public static object TakeArg()
        {
            return arguments.Dequeue();
        }

        public static void LeaveHome(Form destination)
        {
            AwayForm = destination;
            HomeForm.Hide();
            AwayForm.Show();
        }

        public static void ReturnHome(string exitMessage)
        {
            if (exitMessage != null)
            {
                MessageBox.Show(exitMessage);
            }
            AwayForm = null;
            HomeForm.Show();
        }
    }
}