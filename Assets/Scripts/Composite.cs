using System.Collections.Generic;


   public abstract class Composite : Node
   {
       public List<Node> children;
   
       public Composite(List<Node> children)
       {
           this.children = children;
       }
   }
