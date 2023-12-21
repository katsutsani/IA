using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING, 
        SUCCESS, 
        FAILURE,
    }

    public class NodeEmile
    {
        protected NodeState state;

        public NodeEmile parent; 

        protected List<NodeEmile> children = new List<NodeEmile>();

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public NodeEmile() 
        { 
            parent = null;   
        }

        public NodeEmile(List<NodeEmile> children)
        {
            foreach (NodeEmile child in children)
            {
                Attach(child);
            }
        }

        public void Attach(NodeEmile node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null; 
            if (dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            NodeEmile node = parent;

            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if(dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            NodeEmile node = parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if(cleared)
                {
                    return true; 
                }
                node = node.parent;
            }

            return false;
        }
    }
}
