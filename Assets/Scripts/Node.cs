using System.Collections.Generic;
using UnityEngine;

public enum NodesState
{
    FAILURE,
    SUCCES,
    RUNNING
}
public abstract class Node
{

    public List<Node> Children = new List<Node>(); //instanicer?

    public abstract NodesState Evaluate(GameObject contextGo);
}

public abstract class Composite : Node {
}

public class Sequence : Composite
{
    public override NodesState Evaluate(GameObject contextGo)
    {
        NodesState state = NodesState.SUCCES;
        foreach (Node child in Children) {
            state = child.Evaluate(contextGo);
            if (state == NodesState.FAILURE) return NodesState.FAILURE;
        }
        return state;
    }
}

public class Selector : Composite
{
    public override NodesState Evaluate(GameObject contextGo)
    {
        foreach (Node child in Children)
        {
            NodesState state = child.Evaluate(contextGo);
            if (state != NodesState.FAILURE) return state;
        }
        return NodesState.FAILURE;
    }
}

public abstract class Leaf : Node {
}

public class IsThirsty : Leaf
{
    public override NodesState Evaluate(GameObject contextGo)
    {
        return contextGo.activeSelf ? NodesState.SUCCES : NodesState.FAILURE;
    }
}

public class IsHungry : Leaf
{
    public override NodesState Evaluate(GameObject contextGo)
    {
        return contextGo.activeSelf ? NodesState.SUCCES : NodesState.FAILURE;
    }
}

