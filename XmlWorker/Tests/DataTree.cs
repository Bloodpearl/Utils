using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlWorker.Tests
{
    public static class DataTree
    {
        public static bool RunTests(out string name)
        {
            string className = "DataTree";
            Program.TestFunction[] tests = {
                
            };
            throw new NotImplementedException();

            return Test.RunTests(className, out name, tests);
        }
    }

    public static class DataNode
    {
        public static bool RunTests(out string name)
        {
            string className = "DataNode";
            Program.TestFunction[] tests = {
                EqualTrue,EqualFalse
            };

            return Test.RunTests(className, out name, tests);
        }

        public static bool EqualTrue(out string name)
        {
            name = "EqualTrue";
            XmlWorker.DataNode a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            XmlWorker.DataNode b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            a.NodeValue = 874;
            b.NodeValue = 874;
            if (!a.Equal(b))
                return false;

            a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            a.NodeValue = b.NodeValue = new int[874, 123];
            if (!a.Equal(b))
                return false;

            return true;
        }

        public static bool EqualFalse(out string name)
        {
            name = "EqualFalse";
            XmlWorker.DataNode a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            XmlWorker.DataNode b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            a.NodeValue = 874;
            b.NodeValue = 144;
            if (a.Equal(b))
                return false;

            a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            a.NodeValue = new int[123, 874];
            b.NodeValue = new int[874, 123];
            if (a.Equal(b))
                return false;
            
            a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            a.NodeValue = new int[874, 123];
            b.NodeValue = new int[874, 123];
            if (a.Equal(b))
                 return false;

            return true;
        }
    }

    public static class DataBranch
    {
        public static bool RunTests(out string name)
        {
            string className = "DataBranch";
            Program.TestFunction[] tests = {
                
            };
            throw new NotImplementedException();

            return Test.RunTests(className, out name, tests);
        }
    }

    public static class DataLeaf
    {
        public static bool RunTests(out string name)
        {
            string className = "DataLeaf";
            Program.TestFunction[] tests = {
                
            };
            throw new NotImplementedException();

            return Test.RunTests(className, out name, tests);
        }
    }
}
