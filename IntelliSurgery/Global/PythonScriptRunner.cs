using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using IntelliSurgery.DTOs;
using Newtonsoft.Json;

namespace IntelliSurgery.Global
{
    public class PythonScript
    {
        private readonly IConfiguration configuration;

        public PythonScript(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TimeSpan PredictTime(PatientMedicalData patientMedicalData)
        {
            // full path of python interpreter
            //stirng python = @"..........."
            string python = configuration.GetValue<string>("PythonPath");

            // python app to call 
            //stirng myPythonApp = @"..........."
            string myPythonApp = configuration.GetValue<string>("PythonProgramPath");


            //json string to be sent to Python script
            string patientDataJson = JsonConvert.SerializeObject(patientMedicalData);
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(patientDataJson);
            int Age = jsonData.Age;
            int Gender = jsonData.Gender;
            int ASA = jsonData.ASA;
            double BMI = jsonData.BMI;
            int Complication = jsonData.Complication;
            string Surgerytype = jsonData.Surgerytype;
            //list Diseases = jsonData.Diseases;
            Console.WriteLine(BMI);
            Console.WriteLine(patientDataJson);
            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // 1st arguments is pointer to itself
            myProcessStartInfo.Arguments = myPythonApp + " " + Age + " " + Gender + " " + ASA + " " + BMI + " " + Complication + " " + Surgerytype;

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
            string modelOutput = myStreamReader.ReadLine();


            //if you need to read multiple lines, you might use: 
            //string myString = myStreamReader.ReadToEnd();
            //Console.WriteLine(myString);

            // wait exit signal from the app we called and then close it. 
            myProcess.WaitForExit();
            myProcess.Close();

            double predictedTimeDouble = double.Parse(modelOutput);

            TimeSpan predictedTime = TimeSpan.FromHours(predictedTimeDouble);
            return predictedTime;

        }

    }


}
