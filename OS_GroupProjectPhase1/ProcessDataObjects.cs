using System;
using System.Collections.Generic;
using System.Text;

namespace OS_GroupProjectPhase1
{
    /* Class to hold data elements from the file, and return them to the processor class */
    class ProcessDataObjects
    {
        // Elements of each data object
        private string sProcessName { get; set; }
        private int iArrivalTime { get; set; }
        private int iServiceTime { get; set; }
        private int iPriority { get; set; }

        // Set of private static queues for the data
        private static Queue<int> qArrival = new Queue<int>();
        private static Queue<string> qName = new Queue<string>();
        private static Queue<int> qService = new Queue<int>();
        private static Queue<int> qPriority = new Queue<int>();

        /* Empty class constructor */
        public ProcessDataObjects() { }

        /* Used to split the text file into the appropraite elements for each process object and 
           Saves them into an array of ProcessDataObjects which get sorted and then sent to be added to the queue
           I know there must be a better/easier way to do this, but I don't know it ATM, but this is heinous */
        public void SplitInputIntoObjects(string[] sInput)
        {
            // The four elements used per object along with counters and the array of ProcessDataObjects
            int iThisArrival = 0;
            int iThisServiceTime = 0;
            int iThisPriority = 0;
            int i = 0;
            int j = 0;
            ProcessDataObjects[] ProcessArray = new ProcessDataObjects[sInput.Length / 4];

            // while loop that takes 4 elements per loop to assign to the object
            while (i < sInput.Length)
            {
                ProcessArray[j] = new ProcessDataObjects();

                // Try catch block to check the array pos and format to proper data type
                try
                {
                    iThisArrival = int.Parse(sInput[i]);
                    ProcessArray[j].iArrivalTime = iThisArrival;
                }
                catch
                {
                    Console.WriteLine("Unable to parse array position #" + i);
                }

                try
                {
                    ProcessArray[j].sProcessName = sInput[++i];
                }
                catch
                {
                    Console.WriteLine("Unable to parse array position #" + i);
                }

                try
                {
                    iThisServiceTime = int.Parse(sInput[++i]);
                    ProcessArray[j].iServiceTime = iThisServiceTime;
                }
                catch
                {
                    Console.WriteLine("Unable to parse array position #" + i);
                }

                try
                {
                    iThisPriority = int.Parse(sInput[++i]);
                    ProcessArray[j].iPriority = iThisPriority;
                }
                catch
                {
                    Console.WriteLine("Unable to parse array position #" + i);
                }

                // increment counters
                i++;
                j++;
            }

            // Calls SortQueue to sort by Priority
            SortArray(ProcessArray);
        }

        /* Sorts the array of ProcessDataObjects by arrival time from low to high via selection sort,
           possibly will be changing to priority later, but current test file has all same priority */
        private void SortArray(ProcessDataObjects[] arrayToSort)
        {
            int j, min;
            for (int i = 0; i < arrayToSort.Length; i++)
            {
                min = i;
                for (j = 0; j < arrayToSort.Length; j++)
                {
                    if (arrayToSort[j].iArrivalTime > arrayToSort[min].iArrivalTime)
                    {
                        min = j;
                        ProcessDataObjects Temp = arrayToSort[j];
                        arrayToSort[j] = arrayToSort[i];
                        arrayToSort[i] = Temp;
                    }
                }
            }

            // Calls FinalizeQueue to enque the now sorted array elements 
            FinalizeQueue(arrayToSort);
        }

        /* Enqueue the elements from the now sorted array into a queue format */
        private void FinalizeQueue(ProcessDataObjects[] arrayToEnque)
        {
            for (int i = 0; i < arrayToEnque.Length; i++)
            {
                qArrival.Enqueue(arrayToEnque[i].iArrivalTime);
                qName.Enqueue(arrayToEnque[i].sProcessName);
                qService.Enqueue(arrayToEnque[i].iServiceTime);
                qPriority.Enqueue(arrayToEnque[i].iPriority);
            }
        }

        /* Returns a Tuple containing in order, the arrival time, process name, service time,
           and priority for element 0, usues a lock to enforce mutex so multiple threads aren't 
           futzing with the queues at the same time */
        public Tuple<int, string, int, int> ReturnTop()
        {
            lock (qArrival)
            {
                return Tuple.Create(qArrival.Dequeue(), qName.Dequeue(), qService.Dequeue(), qPriority.Dequeue());
            }
        }

        /* Method to print all remaining elements in our queues */
        public void PrintQueue()
        {
            int arv, time, pri;
            string nme;


            lock (qArrival)
            {
                // Block of writeline that prints current processor time, global time passed and
                // a formatted table of the remaining processes in the queue 
                Console.WriteLine("Current Processor Time: " + Globals.iTotalProcessorTime +
                    "               Current Global time passed: " + Globals.iTotalTime + "\n");

                Console.WriteLine("                         Current Queue");
                Console.WriteLine("Arrival Time     Process Name        Service Time        Priority");
                for (int i = 0; i < qArrival.Count; i++)
                {
                    // Store values at top of queue to re-enqueue 
                    arv = qArrival.Peek();
                    nme = qName.Peek();
                    time = qService.Peek();
                    pri = qPriority.Peek();

                    // Write them to console via dequeue 
                    Console.WriteLine(qArrival.Dequeue() + "               " +
                        qName.Dequeue() + "           " + qService.Dequeue() +
                        "                   " + qPriority.Dequeue() + "\n");

                    // Re-enqueue them
                    qArrival.Enqueue(arv);
                    qName.Enqueue(nme);
                    qService.Enqueue(time);
                    qPriority.Enqueue(pri);

                }
                Console.WriteLine("\n\nPRESS SPACEBAR TO PAUSE\n ");
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        /* Returns remaining number of processes left to run */
        public int ReturnLength()
        {
            return qArrival.Count;
        }

        /* Returns arrival time of top of queue */
        public int ReturnArrival()
        {
            lock (qArrival)
            {
                if (ReturnLength() != 0)
                {
                    return qArrival.Peek();
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
