using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace IntelliSurgery.Global
{
    public class PythonScript
    {
        private readonly IConfiguration configuration;

        public PythonScript(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TimeSpan PredictTime()
        {
            // full path of python interpreter
            //stirng python = @"..........."
            string python = configuration.GetValue<string>("PythonPath");

            // python app to call 
            //stirng myPythonApp = @"..........."
            string myPythonApp = configuration.GetValue<string>("PythonProgramPath");

            // dummy parameters to send Python script 
            int x = 50;
            int y = 60;

            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments  
            // 1st arguments is pointer to itself,  
            // 2nd and 3rd are actual arguments we want to send 
            myProcessStartInfo.Arguments = myPythonApp + " " + x + " " + y;

            Process myProcess = new Process();
            // assign start information to  the process 
            myProcess.StartInfo = myProcessStartInfo;

            //Console.WriteLine("Calling Python script with arguments {0} and {1}", x, y);
            // start the process 
            myProcess.Start();

            // Read the standard output of the app we called.  
            // in order to avoid deadlock we will read output first 
            // and then wait for process terminate: 
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();
            Console.WriteLine(myString);

            /*if you need to read multiple lines, you might use: 
                string myString = myStreamReader.ReadToEnd() */

            // wait exit signal from the app we called and then close it. 
            myProcess.WaitForExit();
            myProcess.Close();

            // write the output we got from python app 
            Console.WriteLine("Value received from script: " + myString);

            return TimeSpan.Zero;

        }

    }

    
}
