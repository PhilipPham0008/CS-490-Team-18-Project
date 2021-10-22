using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace OS_GroupProjectPhase1
{
    class Processor
    {
        // Object to use as lock to avoid multiple threads trying to access
        // the global counters at the same time
        private readonly object globalCounterLock = new object();

        // Bool variables for use to check if the stack is empty or
        // the pause key was hit
        private volatile bool isStackEmpty = false;
        private volatile bool pauseHit = false;

        /* Runs lock to enforce mutex on the data objects and allow only one process at a time to interact */
        public void SimRun()
        {
            // Create instance of ProcessDataObjects class
            ProcessDataObjects myDataObject = new ProcessDataObjects();
            int numRemain = myDataObject.ReturnLength();

            // While the processes aren't empty go through loop
            while (numRemain >= 1)
            {
                // If pause button has not been hit continue
                if (pauseHit == false)
                {
                    lock (globalCounterLock)
                    {
                        // if top of queue has "arrived" and the queue still has a process, process it
                        if (myDataObject.ReturnArrival() <= Globals.iTotalTime && myDataObject.ReturnLength() != 0)
                        {
                            // Print the queue
                            myDataObject.PrintQueue();

                            // Returns the Dequeued oldest element from the queues via the ReturnTop method
                            var simTuple = myDataObject.ReturnTop();

                            // Update to reflect amount of time passed to the globals,
                            // decrement num of processes remaining
                            Globals.iTotalTime += simTuple.Item3;
                            Globals.iTotalProcessorTime += simTuple.Item3;
                            numRemain = myDataObject.ReturnLength();
                        }
                        // If no processes have "arrived" increment global time as a wait cycle
                        else
                        {
                            if(isStackEmpty == true)
                            {
                                break;
                            }
                            Console.WriteLine("\nNO PROCESSES TO PROCESS, SPINNING WHEELS\n");
                            myDataObject.PrintQueue();
                            Globals.iTotalTime++;
                        }
                    }
                    Thread.Sleep(2000);
                }
            }
            // When all jobs are processed flip bool so the pause thread will break out of loop
            isStackEmpty = true;

            // Print out final times
            Console.WriteLine("\nFinal Processor Time: " + Globals.iTotalProcessorTime +
                    "               Final Global time passed: " + Globals.iTotalTime + "\n");
        }

        /* Loop that runs and is listening for keypresses to pause and resume the processing cycle */
        public void ProcessorPause()
        {
            // Loop checking for pause keypress and resume
            while (isStackEmpty == false)
            {
                // Read user keypress  
                ConsoleKeyInfo keyPress = new ConsoleKeyInfo();
                keyPress = Console.ReadKey(true);

                // If spacebar hit flip pauseHit bool to true to pause the processor loop  
                if (keyPress.Key == ConsoleKey.Spacebar)
                {
                    Console.WriteLine("Process paused, hit enter to resume.");
                    pauseHit = true;
                }

                keyPress = Console.ReadKey(true);

                // If enter hit flip pause bool to false to resume processor loop
                if (keyPress.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Resuming Process...\n");
                    pauseHit = false;
                }
            }
        }
    }
}