using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



    public abstract class Node
    {
        public enum NodeState { SUCCESS, FAILURE, RUNNING };

        public NodeState state;

        public Node()
        {
            state = NodeState.RUNNING;
        }

        public abstract NodeState Evaluate();
        
    }
 

