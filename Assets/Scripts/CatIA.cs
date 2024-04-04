using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIA : MonoBehaviour
{
    public bool IsAlive => true;
    // get nearestbed 

    private Node _rootNode;
   
    
    private void Awake()
    {
       _rootNode = new Selector();
       Node sequene = new Sequence();
       _rootNode.Children.Add(sequene);
        
    }

    void Update()
    {
        _rootNode.Evaluate(this.gameObject);
    }
}
