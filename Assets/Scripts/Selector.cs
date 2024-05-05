using System.Collections.Generic;


    public class Selector : Composite
    {
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        continue;
                    case NodeState.FAILURE:
                        continue;
                }
            }
            return NodeState.FAILURE;;
        }
    }
