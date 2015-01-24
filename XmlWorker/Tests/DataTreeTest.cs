using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestsBase;

namespace XmlWorker.Tests
{
    public class DataTreeTest : TestClass
    {
    }

    public class DataNodeTest : TestClass
    {

        public bool test_EqualTrue(out string message)
        {
            message = "";
            XmlWorker.DataNode a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            XmlWorker.DataNode b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            a.NodeValue = 874;
            b.NodeValue = 874;
            if (!a.Equal(b))
            {
                message = "wrong result of Equal - value";
                return false;
            }

            a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            a.NodeValue = b.NodeValue = new int[874, 123];
            if (!a.Equal(b))
            {
                message = "wrong result of Equal - reference";
                return false;
            }

            return true;
        }

        public bool test_EqualFalse(out string message)
        {
            message = "";
            XmlWorker.DataNode a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            XmlWorker.DataNode b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Leaf);
            a.NodeValue = 874;
            b.NodeValue = 144;
            if (a.Equal(b))
            {
                message = "wrong result of Equal - value";
                return false;
            }

            a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            a.NodeValue = new int[123, 874];
            b.NodeValue = new int[874, 123];
            if (a.Equal(b))
            {
                message = "wrong result of Equal - diffrent references on diffrent values";
                return false;
            }
            
            a = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            b = new XmlWorker.DataNode(XmlWorker.DataNode.NodeType.Branch);
            a.NodeValue = new int[874, 123];
            b.NodeValue = new int[874, 123];
            if (a.Equal(b))
            {
                message = "wrong result of Equal - diffrent references on same values";
                return false;
            }

            return true;
        }

        public bool test_GetValue_Obj(out string message)
        {
            message = "";
            DataNode node = new DataNode(DataNode.NodeType.Leaf);

            DataNodeTest obj = new DataNodeTest();
            node.NodeValue = obj;
            if (node.GetValue<DataNodeTest>() != obj)
            {
                message = "Wrong GetValue result (object)";
                return false;
            }

            return true;
        }

        public bool test_GetValue_Struct(out string message)
        {
            message = "";
            DataNode node = new DataNode(DataNode.NodeType.Leaf);

            int structValue = 5;
            node.NodeValue = structValue;
            if (node.GetValue<int>() != structValue)
            {
                message = "Wrong GetValue result (struct)";
                return false;
            }

            return true;
        }
    }

    public class DataBranchTest : TestClass
    {
    }

    public class DataLeafTest : TestClass
    {
        public bool test_Value_GetType(out string message)
        {
            message = "";

            int structValue = 5;
            DataLeaf leaf = new DataLeaf(structValue.GetType());
            try
            {
                leaf.Value = structValue;
                if ((int)leaf.Value != structValue)
                {
                    message = "Leaf returns wrong value (struct)";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = "Cannot put into leaf struct value\n" + ex.Message;
                return false;
            }

            DataLeafTest value = new DataLeafTest();
            leaf = new DataLeaf(value.GetType());
            try
            {
                leaf.Value = value;
                if ((DataLeafTest)leaf.Value != value)
                {
                    message = "Leaf returns wrong value (struct)";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = "Cannot put into leaf struct value\n" + ex.Message;
                return false;
            }

            return true;
        }

        public bool test_Value_TypeOF(out string message)
        {
            message = "";

            int structValue = 5;
            DataLeaf leaf = new DataLeaf(typeof(int));
            try
            {
                leaf.Value = structValue;
                if ((int)leaf.Value != structValue)
                {
                    message = "Leaf returns wrong value (struct)";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = "Cannot put into leaf struct value\n" + ex.Message;
                return false;
            }

            DataLeafTest value = new DataLeafTest();
            leaf = new DataLeaf(typeof(DataLeafTest));
            try
            {
                leaf.Value = value;
                if ((DataLeafTest)leaf.Value != value)
                {
                    message = "Leaf returns wrong value (struct)";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = "Cannot put into leaf struct value\n" + ex.Message;
                return false;
            }

            return true;
        }

        public bool test_Value_null(out string message)
        {
            message = "";
            
            DataLeaf leaf = new DataLeaf(typeof(DataLeafTest));
            try
            {
                leaf.Value = null;
                if (leaf.Value != null)
                {
                    message = "Leaf returns wrong value (struct)";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = "Cannot put into leaf struct value\n" + ex.Message;
                return false;
            }

            return true;
        }

        public bool test_Equal_True(out string message)
        {
            message = "";
            DataLeaf a = new DataLeaf(typeof(int));
            DataLeaf b = new DataLeaf(typeof(int));
            a.Value = 874;
            b.Value = 874;
            if (!a.Equal(b))
            {
                message = "wrong result of Equal - value '" + a.Value.ToString() + "' vs '" + b.Value.ToString()+"'";
                return false;
            }

            a = new DataLeaf(typeof(int[]));
            b = new DataLeaf(typeof(int[]));
            int [] array =  new int[]{874, 123};
            a.Value = array;
            b.Value = array;
            if (!a.Equal(b))
            {
                message = "wrong result of Equal - reference";
                return false;
            }

            return true;
        }

        public bool test_Equal_False(out string message)
        {
            message = "";
            DataLeaf a = new DataLeaf(typeof(int));
            DataLeaf b = new DataLeaf(typeof(int));
            a.Value = 874;
            b.Value = 144;
            if (a.Equal(b))
            {
                message = "wrong result of Equal - value";
                return false;
            }

            a = new DataLeaf(typeof(int[]));
            b = new DataLeaf(typeof(int[]));
            a.Value = new int[]{123, 874};
            b.Value = new int[]{874, 123};
            if (a.Equal(b))
            {
                message = "wrong result of Equal - diffrent references on diffrent values";
                return false;
            }

            a = new DataLeaf(typeof(int[]));
            b = new DataLeaf(typeof(int[]));
            a.Value = new int[]{874, 123};
            b.Value = new int[]{874, 123};
            if (a.Equal(b))
            {
                message = "wrong result of Equal - diffrent references on same values";
                return false;
            }

            return true;
        }

    }
}
