using IronPython.Hosting;
using System;
using System.IO;

namespace IntelliSurgery.Global
{
    public class PythonScript
    {
        public TimeSpan PredictTime()
        {
            var engine = Python.CreateEngine();
            //reading code from file
            var source = engine.CreateScriptSourceFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonSampleIronPython.py"));
            var scope = engine.CreateScope();
            //executing script in scope
            source.Execute(scope);
            var classCalculator = scope.GetVariable("calculator");
            //initializing class
            var calculatorInstance = engine.Operations.CreateInstance(classCalculator);

            //https://www.dotnetlovers.com/Article/216/executing-python-script-from-C-Sharp

            TimeSpan res = new TimeSpan(0,0,0);
            return res;
        }
    }

    
}
