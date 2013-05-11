using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
namespace ProcessManagementSim
{
    public class Pro
    {
        private int ProcessID;
        private int ProcessSize;

        public Pro()
        {
            ResetProcess();
        }

        public void ResetProcess()
        {
            Random rdm = new Random();
            Thread.Sleep(10);
            ProcessID = rdm.Next(1000, 9999);
            Thread.Sleep(10);
            ProcessSize = rdm.Next(1, 300);
        }
        public bool Continue()
        {
            if (ProcessSize > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DivProcessSize()
        {
            Random rdm = new Random();
            int tmp = 30;
            ProcessSize -= tmp;
        }
        public int GetProcessID()
        {
            return ProcessID;
        }
        public int GetProcessSize()
        {
            return ProcessSize;
        }
    }
}
