using System;
using System.Threading;
using System.IO;

namespace OS_GroupProjectPhase1
{
    /* Class to hold global variables */
    static class Globals
    { 
        public static int iTotalTime;
        public static int iTotalProcessorTime;
    }
        
    
    class Program
    {
        static void Main(string[] args)
        {
            ProcessDataObjects myProcess = new ProcessDataObjects();
            Processor myCPU = new Processor();

            // Set total and curr processor time to 0 
            Globals.iTotalTime = 0;
            Globals.iTotalProcessorTime = 0;

            // Block to ask for filepath, too lazy to type every time I want to test so hard coded my filepath,
            // just comment out the hardcoded path and uncomment the readline to change to user input
            Console.WriteLine("Please enter the filepath for your file");
            //string iAmTooLazyToTypeThisEveryTime = Console.ReadLine();
            string iAmTooLazyToTypeThisEveryTime = @"C:\Users\Scott\Desktop\test.txt";

            // Load text file into a string for processing, split into an array of strings by comma or newline characters
            string file = File.ReadAllText(iAmTooLazyToTypeThisEveryTime);
            string[] saSplitText = file.Split(new string[] { ",", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Create thread for Process Pause and start
            Thread tProcesserPause = new Thread(myCPU.ProcessorPause);
            tProcesserPause.Start();

            // Creates an array of ProcessDataObjects to store the processes
            ProcessDataObjects[] myProcessArray = new ProcessDataObjects[saSplitText.Length / 4];

            // Pass array of split strings to have the info used to create the data objects 
            // Which are sorted
            myProcess.SplitInputIntoObjects(saSplitText);

            // Start Processor Sim threads and start running
            Thread tProcessorSim1 = new Thread(myCPU.SimRun);
            Thread tProcessorSim2 = new Thread(myCPU.SimRun);
            tProcessorSim1.Start();
            tProcessorSim2.Start();
        }
    }
}
