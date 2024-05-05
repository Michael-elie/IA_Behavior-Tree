using System.Collections.Generic;


    public class Sequence : Composite
    {
      

        public override NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                }
            }
            return NodeState.SUCCESS;;
        }


        public Sequence(List<Node> children) : base(children)
        {
        }
    }
