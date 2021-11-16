using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS490_testing
{
    /* Class to hold global variables */
    static class Globals
    {
        public static int iTotalTime;
        public static int iTotalProcessorTime;
        public static Processor myCPU;
        public static ProcessDataObjects myProcess;
        public static Thread tProcessorSim1;
        public static Thread tProcessorSim2;
    }

    class Program
    {

        static void Main(string[] args)
        {
            Globals.myProcess = new ProcessDataObjects();
            Globals.myCPU = new Processor();

            // create the GUI from the gui namespace
            gui.GUI newGUI = new gui.GUI();
            newGUI.startProcessorsEvent += startProcessors;

            // Run the GUI --> need all connections to the processor Threads connected before running the GUI
            //      as the GUI will run in the main thread



            // Set total and curr processor time to 0 
            Globals.iTotalTime = 0;
            Globals.iTotalProcessorTime = 0;

            // Block to ask for filepath, too lazy to type every time I want to test so hard coded my filepath,
            // just comment out the hardcoded path and uncomment the readline to change to user input
            Console.WriteLine("Please enter the filepath for your file");
            string iAmTooLazyToTypeThisEveryTime = Console.ReadLine();
            //string iAmTooLazyToTypeThisEveryTime = @"C:\Users\danie\source\Repos\CS490_testing\CS490_testing\test.txt";

            // Load text file into a string for processing, split into an array of strings by comma or newline characters
            string file = File.ReadAllText(iAmTooLazyToTypeThisEveryTime);
            string[] saSplitText = file.Split(new string[] { ",", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Create thread for Process Pause and start
            Thread tProcesserPause = new Thread(Globals.myCPU.ProcessorPause);
            tProcesserPause.Start();

            // Creates an array of ProcessDataObjects to store the processes
            ProcessDataObjects[] myProcessArray = new ProcessDataObjects[saSplitText.Length / 4];

            // Pass array of split strings to have the info used to create the data objects 
            // Which are sorted
            Globals.myProcess.SplitInputIntoObjects(saSplitText);

            // Start Processor Sim threads and start running
            Globals.tProcessorSim1 = new Thread(Globals.myCPU.SimRun);
            Globals.tProcessorSim2 = new Thread(Globals.myCPU.SimRun);
            //Globals.tProcessorSim1.Start();
            //Globals.tProcessorSim2.Start();
            Application.Run(newGUI);
        }

        static void startProcessors(int num)
        {
            
            Globals.tProcessorSim1.Start();
            Globals.tProcessorSim2.Start();
        }
    }
}
