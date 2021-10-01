using System;
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

            // Set total and curr processor time to 0 
            Globals.iTotalTime = 0;
            Globals.iTotalProcessorTime = 0;

        // Load text file into a string for processing, split into an array of strings by comma or newline characters
        string file = File.ReadAllText(@"C:\Users\Scott\Desktop\test.txt");
            string[] saSplitText = file.Split(new string[] { ",", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Creates an array of ProcessDataObjects to store the processes
            ProcessDataObjects[] myProcessArray = new ProcessDataObjects[saSplitText.Length / 4];

            // Pass array of aplit strings to have the info used to create the data objects 
            // Which are returned as a sorted array of ProcessDataObjects
            myProcessArray = myProcess.SplitInputIntoObjects(saSplitText);
            myProcess.SimRun(myProcessArray);
        }
    }
}
