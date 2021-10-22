using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OS_GroupProjectPhase1
{
    class ProcessDataObjects
    {
        // Elements of each data object
        public string sProcessName { get; set; }
        public int iArrivalTime { get; set; }
        public int iServiceTime { get; set; }
        public int iPriority { get; set; }

        /* Empty class constructor */
        public ProcessDataObjects() { }

        /* Used to split the text file into the appropraite elements for each process object and 
           Saves them into an array of ProcessDataObjects */
        public ProcessDataObjects[] SplitInputIntoObjects(string[] sInput)
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

                i++;
                j++;
            }
            // Calls SortQueue to sort by Priority
            SortArray(ProcessArray);

            // Return the array of ProcessDataObjects
            return ProcessArray;
        }

        /* Sorts the array of ProcessDataObjects by service time from low to high via selection sort,
           possibly will be changing to priority later, but current test file has all same priority */
        public ProcessDataObjects[] SortArray(ProcessDataObjects[] arrayToSort)
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

            // return the sorted array
            return arrayToSort;
        }

        public void SimRun(ProcessDataObjects[] myProcessArray)
        {
            while (myProcessArray.Length != 0)
            {
                // Block of writeline that prints current processor time, global time passed and
                // a formatted table of the remaining processes in the queue 
                Console.WriteLine("Current Processor Time: " + Globals.iTotalProcessorTime +
                    "               Current Global time passed: " + Globals.iTotalTime + "\n");

                Console.WriteLine("             Current Queue");
                Console.WriteLine("Arrival Time     Process Name        Service Time        Priority");
                for (int i = 0; i < myProcessArray.Length; i++)
                {
                    Console.WriteLine(myProcessArray[i].iArrivalTime + "               " +
                        myProcessArray[i].sProcessName + "           " + myProcessArray[i].iServiceTime +
                        "                   " + myProcessArray[i].iPriority);
                }

                // if top of queue has "arrived" process it
                if (myProcessArray[0].iArrivalTime <= Globals.iTotalTime)
                {
                    // Update to reflect amount of time passed to the globals
                    Globals.iTotalTime += myProcessArray[0].iServiceTime;
                    Globals.iTotalProcessorTime += myProcessArray[0].iServiceTime;

                    // remove data object from the array
                    myProcessArray = myProcessArray.Where((source, index) => index != 0).ToArray();
                }
                // If no processes have "arrived" increment global time as a wait cycle
                else
                {
                    Globals.iTotalTime++;
                }
            }

            // Print out final times
            Console.WriteLine("Final Processor Time: " + Globals.iTotalProcessorTime +
                    "               Final Global time passed: " + Globals.iTotalTime + "\n");
        }
    }
}
