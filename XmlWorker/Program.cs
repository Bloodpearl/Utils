using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlWorker.Tests;
using UnitTestsBase;

namespace XmlWorker.Tests
{
    public static class Program
    {
        private static TestRunner tests;

        public static void PrepareTests()
        {
            TestClass[] classes = {
                new DataNodeTest(),
                new DataLeafTest(),
                new DataBranchTest(),
                new DataTreeTest()
            };
            tests = new TestRunner();
            tests.LoudMode = true;
            tests.CatchExceptions = true;

            tests.ClassesToTest.AddRange(classes);
        }
        public static void Main()
        {
            PrepareTests();
            tests.Start();
        }
        /*
        static bool catchExceptions = true;
        static int fails;
        static int total;
        static int passed
        {
            get { return total - fails; }
        }

        public delegate bool TestFunction(out string name);


        static bool RunTest(TestFunction func)
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
            return tr == TestResult.Success;
        }
        enum TestResult
        {
            Failed = 0,
            Success = 1
        }

        static void Main()
        {
            bool success = false;
            success = RunTests();
            Console.WriteLine();
            Console.WriteLine("Tests complete with result: " + ((TestResult)(success ? 1 : 0)).ToString());
            Console.WriteLine("Mark: " + passed + "/" + total + " (Fails: " + fails + ")");
            Console.ReadKey();
        }

        static bool RunTests()
        {
            TestFunction[] test = {
                Tests.DataNode.RunTests,
                Tests.DataLeaf.RunTests,
                Tests.DataBranch.RunTests,
                Tests.DataTree.RunTests
                                  };

            bool success = true;
            int fails = 0;
            
            foreach( TestFunction t in test)
            {
                if (!RunTest(t))
                {
                    success = false;
                    fails++;
                }
                //if(success == 0) break;
            }

            Program.total = test.Length;
            Program.fails = fails;
             
            return success;
        }
        */
    }
}
