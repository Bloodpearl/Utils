using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlWorker.Tests;

namespace XmlWorker.Tests
{
    public static class Program
    {
        static bool catchExceptions = true;
        enum TestResult
        {
            Failed = 0,
            Success = 1
        }

        static void Main()
        {
            int success = 1;
            success = RunTests();
            Console.WriteLine("Tests complete with result: " + ((TestResult)success).ToString());
            Console.ReadKey();
        }

        static int RunTests()
        {
            TestFunction[] test = {
                Tests.DataNode.RunTests,
                Tests.DataLeaf.RunTests,
                Tests.DataBranch.RunTests,
                Tests.DataTree.RunTests
                                  };

            int success = 1;
            
            foreach( TestFunction t in test)
            {
                success = RunTest(t) == 1 && success == 1 ? 1 : 0;
                //if(success == 0) break;
            }
             
            return success;
        }

        public delegate bool TestFunction(out string name);
        static int RunTest(TestFunction func)
        {
            string name = "";
            string message = null;
            TestResult tr = TestResult.Success;
            if (catchExceptions)
            {
                try
                {
                    if (!func(out name))
                        tr = TestResult.Failed;
                }
                catch (Exception ex)
                {
                    tr = TestResult.Failed;
                    message = ex.Message;
                }
            }
            else
            {
                if (!func(out name))
                    tr = TestResult.Failed;
            }
            Console.WriteLine("Test " + name + " end with status: " + tr.ToString());
            if (message != null)
                Console.WriteLine("   " + message);
            return (int)tr;
        }
    }
}
