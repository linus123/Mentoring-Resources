using System;
using System.Diagnostics;

namespace CodeExamples.ObjectOrientedProgramming
{
    public interface ISystemLog
    {
        void LogInfo(string message);
        void LogError(string message);
    }

    public class ConsoleSystemLog : ISystemLog
    {
        public void LogInfo(string message)
        {
            var m = string.Format("{0} - INFO: {1:yy-MM-dd H:mm:ss zzz}", message, DateTime.Now);
            Console.WriteLine(m);
        }

        public void LogError(string message)
        {
            var m = string.Format("{0} - ERROR: {1:yy-MM-dd H:mm:ss zzz}", message, DateTime.Now);
            Console.WriteLine(m);
        }
    }

    public class NoOperationSystemLog : ISystemLog
    {
        public void LogInfo(string message)
        {
        }

        public void LogError(string message)
        {
        }
    }

    public class DebugSystemLog : ISystemLog
    {
        public void LogInfo(string message)
        {
            var m = string.Format("INFO: {0}", message);
            Debug.WriteLine(m);
        }

        public void LogError(string message)
        {
            var m = string.Format("ERROR: {0}", message);
            Debug.WriteLine(m);
        }
    }

    public class ComplicatedBusinessProgram
    {
        public void Main()
        {
            //var systemLog = new ISystemLog() <= Will NOT work!

            var consoleSystemLog = new ConsoleSystemLog();
            DoSomeComplicatedBusinessStuff(consoleSystemLog);
            var noOperationSystemLog = new NoOperationSystemLog();
            DoSomeComplicatedBusinessStuff(noOperationSystemLog);

            var debugSystemLog = new DebugSystemLog();
            DoSomeComplicatedBusinessStuff(debugSystemLog);
        }

        public void DoSomeComplicatedBusinessStuff(
            ISystemLog systemLog)
        {
            systemLog.LogInfo("Starting long running business process.");

            try
            {
                // some REALLY complicated stuff
            }
            catch (Exception e)
            {
                systemLog.LogError(e.Message);
            }

            systemLog.LogInfo("Long running business process completed.");
        }
    }
}