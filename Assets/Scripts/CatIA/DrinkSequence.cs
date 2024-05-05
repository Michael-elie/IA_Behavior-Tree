using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.CatIA
{
    public class DrinkSequence : Sequence
    {
        private CatIA _catIa;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        public DrinkSequence(CatIA catIa, NavMeshAgent navMeshAgent, Animator animator,float drinkDuration, float thirstTreshold, float drinkTimer) 
            : base(new List<Node>())
        {
            _catIa = catIa;
            _navMeshAgent = navMeshAgent;
            _animator = animator;

            // FindWaterAction findWaterAction = new FindWaterAction(_catIa.transform, _catIa);
          MoveToWaterAction moveToWaterAction = new MoveToWaterAction(_catIa.transform, _navMeshAgent,_animator,_catIa);
          
          
            children.Add(new CheckThirstAction(_catIa,thirstTreshold));
           // children.Add(findWaterAction);
            children.Add(moveToWaterAction);
            children.Add(new DrinkAction(_catIa.transform,drinkDuration, _animator, _catIa,drinkTimer));

        }
    }
}