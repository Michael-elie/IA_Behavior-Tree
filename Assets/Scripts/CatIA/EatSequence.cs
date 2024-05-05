using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.CatIA
{
    public class EatSequence : Sequence
    {
        private CatIA _catIa;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        public EatSequence(CatIA catIa, NavMeshAgent navMeshAgent, Animator animator,float eatDuration, float eatTreshold, float eatTimer) 
            : base(new List<Node>())
        {
            _catIa = catIa;
            _navMeshAgent = navMeshAgent;
            _animator = animator;

           
         MoveToBirdAction moveToBirdAction = new MoveToBirdAction(_catIa.transform, _navMeshAgent,_animator,_catIa);
          
          
            children.Add(new CheckHungerAction(_catIa,eatTreshold));
            children.Add(moveToBirdAction);
            children.Add(new EatAction(_catIa.transform,eatDuration, _animator, _catIa,eatTimer));

        }
    }
}