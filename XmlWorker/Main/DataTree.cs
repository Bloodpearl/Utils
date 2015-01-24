using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlWorker
{
    public class DataTree
    {
        public DataNode.NodeType RootType;
        public DataNode Root;
        public DataTree(DataNode.NodeType type = DataNode.NodeType.Branch)
        {
            this.RootType = type;
            if (type == DataNode.NodeType.Branch)
            {
                this.Root = new DataBranch();
            }
            else
            {
                this.Root = new DataLeaf(typeof(string));
            }

        }
    }

    public class DataNode
    {
        public enum NodeType
        {
            Leaf = 0,
            Branch = 1
        }

        public NodeType Type;
        public object NodeValue;
        
        public DataNode(NodeType Type)
        {
            this.Type = Type;
        }

        public override string ToString()
        {
            return "DataNode - " + Type.ToString();
        }
        public T GetValue<T>()
        {
            return (T)this.NodeValue;
        }

        public bool Equal(DataNode node)
        {
            return this.Type == node.Type
                && this.NodeValue.Equals(node.NodeValue);
        }
    }

    public class DataBranch: DataNode
    {
        public Dictionary<string, DataNode> Value{
            get
            {
                return (Dictionary<string, DataNode>)base.NodeValue;
            }
            set
            {
                base.NodeValue = value;
            }
        }
        public DataNode this[string key]
        {
            get
            {
                return Value[key];
            }
            set
            {
                if (Value.ContainsKey(key))
                {
                    Value[key] = value;
                }
                else
                {
                    Value.Add(key, value);
                }
            }
        }

        public Dictionary<string, DataNode>.KeyCollection Keys
        {
            get
            {
                return this.Value.Keys; //.ToList<string>().ToArray();
            }
        }

        public DataBranch()
            : base(NodeType.Branch)
        {
            base.NodeValue = new Dictionary<string, DataNode>();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public bool Equal(DataBranch node)
        {
            if(this.Type != node.Type)
                return false;
            Dictionary<string, DataNode>.KeyCollection a = this.Keys;
            Dictionary<string, DataNode>.KeyCollection b = node.Keys;
            if (a.Count != b.Count)
                return false;
            foreach (string key in a)
            {
                if (!b.Contains(key))
                {
                    return false;
                }
                else if (!this[key].Equals(node[key]))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class DataLeaf : DataNode
    {

        public Type DataType;

        public object Value
        {
            get { return base.NodeValue; }
            set
            {
                if (value != null && value.GetType() != DataType)
                    throw new InvalidCastException("Cannot store tyoe '" + value.GetType().Name + "' in '" + DataType.Name + "' leaf!");
                base.NodeValue = value;
            }
        }

        public DataLeaf(Type DataType)
            : base(NodeType.Leaf)
        {
            this.DataType = DataType;
        }

        public override string ToString()
        {
            return "DataNode - " + base.Type.ToString() + ": " + Value.ToString();
        }

        public bool Equal(DataLeaf node)
        {
            /**/
            return this.Type == node.Type
                && this.DataType == node.DataType
                && this.Value.Equals(node.Value);
            /**/
            /*
            bool result;
            result = this.Type == node.Type;
            result = result && this.DataType == node.DataType;
            result = result && this.Value.Equals(node.Value);
            return result;
            /**/
        }
    }
}
