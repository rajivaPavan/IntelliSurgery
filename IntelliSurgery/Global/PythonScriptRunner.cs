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
            string python = configuration.GetValue<string>("PythonPath");
            string myPythonApp = configuration.GetValue<string>("PythonProgramPath");
            string modelFilePath = configuration.GetValue<string>("ModelsFilePath");

            int Age = patientMedicalData.Age;
            int Gender = patientMedicalData.Gender;
            int ASA = patientMedicalData.ASA;
            double BMI = patientMedicalData.BMI;
            int Complication = patientMedicalData.Complication;
            string Surgerytype = patientMedicalData.SurgeryType;

            string DiseasesList = "";
            foreach (string s in patientMedicalData.Diseases)
            {
                DiseasesList += s + ",";
            }
            
            // Create new process start info 
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // 1st arguments is pointer to itself
            myProcessStartInfo.Arguments = myPythonApp + " " + Age + " " + Gender + " " 
                                           + ASA + " " + BMI + " " + Complication + " " 
                                           + Surgerytype + " "+ DiseasesList+" "+modelFilePath;

            Process myProcess = new Process();
            // assign start information to  the process 
            myProcess.StartInfo = myProcessStartInfo;

            // start the process 
            myProcess.Start();

            // Read the standard output of the app we called.  
            // in order to avoid deadlock we will read output first 
            // and then wait for process terminate: 
            StreamReader myStreamReader = myProcess.StandardOutput;
            string modelOutput = myStreamReader.ReadLine();


            //if you need to read multiple lines, you might use: 
            //string myString = myStreamReader.ReadToEnd();

            // wait exit signal from the app we called and then close it. 
            myProcess.WaitForExit();
            myProcess.Close();
            Console.WriteLine(modelOutput);

            double predictedTimeDouble = double.Parse(modelOutput);

            TimeSpan predictedTime = TimeSpan.FromHours(predictedTimeDouble);
            return predictedTime;

        }

    }


}
