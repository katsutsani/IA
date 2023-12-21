using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : NodeEmile
    {
        public Sequence() : base() { }
        public Sequence(List<NodeEmile> children) : base(children) { }

        public override NodeState Evaluate()
        {
            for (int i = 0; i < children.Count; i++)
            {
                NodeEmile node = children[i];
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }

}