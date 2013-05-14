using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ProcessManagementSim
{
    public partial class MainPage : UserControl
    {
        Pro P1;
        Pro P2;
        Pro P3;
        Pro P4;
        RunState runState;
        int sel;
        int arraySize = 4; //doing 4 for now since queue size is static
        bool continueSimulation = false;
        int key = 0;  //it used in updateQueue class for prevent error.



        //Queue to hold our processes
        Queue<Pro> queue = new Queue<Pro>();
        Pro[] queueArray;
       
        // Constructor
        public MainPage()
        {
            InitializeComponent();                                   
            sel = 0;            
            runState = new RunState();
        }

         private void bt1_Click(object sender, RoutedEventArgs e)
        {
            queue.Clear();
             continueSimulation = true;

             //initialize processes
            P1 = new Pro();
            P2 = new Pro();
            P3 = new Pro();
            P4 = new Pro();

            //Add processes to the queue
            queue.Enqueue(P1);
            queue.Enqueue(P2);
            queue.Enqueue(P3);
            queue.Enqueue(P4);

            initializeGui();
        }

         //loop through items in queue and decrement by 30
         private void bt2_Click(object sender, RoutedEventArgs e)
         {             
             if (continueSimulation)
             {
                 updateQueue();
                 updateGui();                 
             }

         }

         private void initializeGui()
         {             
             sel = 0;
             P1.ResetProcess(); 
             tb1.Text = "ProcessID : \n" + "      " + P1.GetProcessID() + "\nProcessSize : \n" + "       " + P1.GetProcessSize();

             P2.ResetProcess();
             tb2.Text = "ProcessID : \n" + "      " + P2.GetProcessID() + "\nProcessSize :\n" + "       " + P2.GetProcessSize();

             P3.ResetProcess();
             tb3.Text = "ProcessID : \n" + "      " + P3.GetProcessID() + "\nProcessSize :\n" + "       " + P3.GetProcessSize();

             P4.ResetProcess();
             tb4.Text = "ProcessID : \n" + "      " + P4.GetProcessID() + "\nProcessSize :\n" + "       " + P4.GetProcessSize();

             dq1.Text = "" + P1.GetProcessID();
             dq2.Text = "" + P2.GetProcessID();
             dq3.Text = "" + P3.GetProcessID();
             dq4.Text = "" + P4.GetProcessID();     
         }

         private void updateQueue()
         {             

             
                 //if there's a process running, put it back in the queue
                 if (runState.currentProcess != null && runState.currentProcess.Continue() && 
                     queue.Count > 0)
                 {
                     queue.Enqueue(runState.currentProcess);
                 }
                 else
                 {
                     runState.currentProcess = null;
                 }

                 //if there's still items in the queue, put the next one in the runstate
                 if (queue.Count > 0)
                 {
                     if (key == 0)   //key prevent situation that two processes do DivProcessSize() at a time when you click just one next button.
                     {

                      
                         runState.currentProcess = queue.Dequeue();   
                         
                         //do work on process and subtract processSize
                         runState.currentProcess.DivProcessSize();
                         key = 1;
                     }
                     if(!runState.currentProcess.Continue() && queue.Count > 0)
                     {
                         if (key == 0)
                         {
                            
                             runState.currentProcess = queue.Dequeue();
                             runState.currentProcess.DivProcessSize();
                             key = 1;
                         }
                     }
                     key = 0;

                     
                     if (queue.Count <= 0 && !runState.currentProcess.Continue())
                         endSimulation();
                     
                 }
                 else
                 {
                     endSimulation();
                 }         




         }

         private void endSimulation()
         {
             continueSimulation = false;
             run.Text = String.Empty;
             clearTables();
             queue.Clear();
             MessageBox.Show("End of simulation");
         }


         private void addProcessButton_Click(object sender, RoutedEventArgs e)
         {
             if (continueSimulation)
             {                 
                     if (!P1.Continue())
                     {
                         P1 = new Pro();
                         queue.Enqueue(P1);
                         updateGui();
                     }
                     else if(!P2.Continue())
                     {
                         P2 = new Pro();
                         queue.Enqueue(P2);
                         updateGui();
                     }
                     else if(!P3.Continue())
                     {
                         P3 = new Pro();
                         queue.Enqueue(P3);
                         updateGui();
                     }
                     else if (!P4.Continue())
                     {
                         P4 = new Pro();
                         queue.Enqueue(P4);
                         updateGui();
                     }
                     else
                     {
                         MessageBox.Show("Can't add new process to a full queue.");
                     }                        
             }
         }  

         private void updateGui()
         {
             if (continueSimulation)
             {
                 copyQueueToArray();
                 int pSize = 0;
                 int pid = 0;

                 //Process 1
                 if (queueArray[0] != null)
                 {
                     pid = queueArray[0].GetProcessID();
                     pSize = queueArray[0].GetProcessSize();
                 }

                 if (P1.Continue())
                 {
                     tb1.Text = "ProcessID : \n" + "      " + P1.GetProcessID() + "\nProcessSize : \n" + "       " + P1.GetProcessSize();
                 }
                 else
                     tb1.Text = "Empty";

                 if (pid == 0)
                     dq1.Text = "Empty";
                 else
                     dq1.Text = "" + pid;

                 //Process 2
                 if (queueArray[1] != null)
                 {
                     pid = queueArray[1].GetProcessID();
                     pSize = queueArray[1].GetProcessSize();
                 }
                 else
                 {
                     pid = 0;
                     pSize = 0;
                 }

                 if (P2.Continue())
                 {
                     tb2.Text = "ProcessID : \n" + "      " + P2.GetProcessID() + "\nProcessSize :\n" + "       " + P2.GetProcessSize();
                 }
                 else
                     tb2.Text = "Empty";

                 if (pid == 0)
                     dq2.Text = "Empty";
                 else
                     dq2.Text = "" + pid;

                 //Process 3
                 if (queueArray[2] != null)
                 {
                     pid = queueArray[2].GetProcessID();
                     pSize = queueArray[2].GetProcessSize();
                 }
                 else
                 {
                     pid = 0;
                     pSize = 0;
                 }

                 if (P3.Continue())
                 {
                     tb3.Text = "ProcessID : \n" + "      " + P3.GetProcessID() + "\nProcessSize :\n" + "       " + P3.GetProcessSize();
                 }
                 else
                     tb3.Text = "Empty";

                 if (pid == 0)
                     dq3.Text = "Empty";
                 else
                     dq3.Text = "" + pid;

                 //Process 4
                 if (queueArray[3] != null)
                 {
                     pid = queueArray[3].GetProcessID();
                     pSize = queueArray[3].GetProcessSize();
                 }
                 else
                 {
                     pid = 0;
                     pSize = 0;
                 }

                 if (P4.Continue())
                 {
                     tb4.Text = "ProcessID : \n" + "      " + P4.GetProcessID() + "\nProcessSize :\n" + "       " + P4.GetProcessSize();
                 }
                 else
                     tb4.Text = "Empty";

                 if (pid == 0)
                     dq4.Text = "Empty";
                 else
                     dq4.Text = "" + pid;

                 run.Text = runState.currentProcess.GetProcessID().ToString();
             }
         }

         private void copyQueueToArray()
         {
             //copy queue to array so we can traverse it
             queueArray = new Pro[arraySize];
             queue.CopyTo(queueArray, 0);
         }

         private void clearTables()
         {
             tb1.Text = "Empty";
             tb2.Text = "Empty";
             tb3.Text = "Empty";
             tb4.Text = "Empty";
             dq1.Text = "Empty";
             dq2.Text = "Empty";
             dq3.Text = "Empty";
             dq4.Text = "Empty";
         }
    }
}
