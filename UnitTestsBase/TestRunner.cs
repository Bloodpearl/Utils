using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace UnitTestsBase
{
    public class TestRunner
    {
        public enum TestResult
        {
            Failed = 0,
            Success = 1
        }


        public List<TestClass> ClassesToTest = new List<TestClass>();

        private bool catchExceptions = true;
        public bool CatchExceptions
        {
            get { return catchExceptions; }
            set { catchExceptions = value; }
        }
        private bool loudMode = true;
        public bool LoudMode
        {
            get { return loudMode; }
            set { loudMode = value; }
        }
        private int fails = 0;
        private int total = 0;
        private int warnings = 0;
        private int passed
        {
            get { return total - fails; }
        }
        
        public void Start()
        {
            bool success = false;
            success = RunTests();
            TestResult result = (TestResult)(success ? 1 : 0);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Tests complete with result: " + result.ToString());
            Console.WriteLine("Failed " + fails + " from " + total);
            Console.WriteLine("Warnings: " + warnings);
            Console.ReadKey();
        }

        private bool RunTests()
        {
            TestClass[] test = ClassesToTest.ToArray();
            bool error = false;

            foreach (TestClass t in test)
            {
                if (!t.TestEnabled)
                {
                    Console.WriteLine(t.GetType().ToString() + " - DISABLED");
                }
                if (!RunTest(t, t.GetType()))
                {
                    error = true;
                }
            }

            return !error;
        }


        private void Print(string message)
        {
            if (LoudMode)
                Console.WriteLine(message);
        }
        
        private bool RunTest(object classObject, Type Tclass)
        {
            Print("  Running " + Tclass.ToString());
            bool pass = true;

            foreach (MethodInfo method in Tclass.GetMethods())
            {
                string name = method.Name;
                if(!isTest(method)){
                    continue;
                }
                string message = null;
                total++;
                if (!runSingleTest(classObject, method, out message))
                {
                    pass = false;
                    fails++;
                }

                Print("   " + (pass ? TestResult.Success : TestResult.Failed) + " - " + name + (!pass ? "\n" + message : ""));
            }


            return pass;
        }

        private bool isTest(MethodInfo method)
        {
            string name = method.Name;
            bool soundLikeTest =
                name.Length > 5 &&
                name.Substring(0, 5) == "test_";
            bool returnType = method.ReturnType == typeof(bool);
            ParameterInfo[] fParams = method.GetParameters();
            bool testParams = fParams.Length == 1;
            if (testParams)
            {
                testParams = testParams && fParams[0].IsOut;
                Type paramType = fParams[0].ParameterType;
                testParams = testParams && paramType.Name == "String&";
            }
            
            if (soundLikeTest && !(testParams && returnType))
            {
                warnings++;
                string msg= "WARN: Function '" + name + "' have name like test, but";
                if(!testParams) {
                    msg += "\n   have wrong params - should be out string";
                }
                if(!returnType) {
                    msg += "\n   dont returns boolean value";
                }
                Console.WriteLine(msg);
            }
            return testParams && soundLikeTest && returnType;
        }

        private bool runSingleTest(object classObject, MethodInfo method, out string message)
        {
            bool pass = true;
            message = "";
            if (CatchExceptions)
            {
                try
                {
                    object[] args = new Object[] { message };
                    pass = (bool)method.Invoke(classObject, args);
                    message = (string)args[0];
                }
                catch (Exception ex)
                {
                    pass = false;
                    message += "\n" + ex.Message;
                }
            }
            else
            {
                pass = (bool)method.Invoke(classObject, new Object[] { message });
            }

            return pass;
        }


    }
}
