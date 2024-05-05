using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.CatIA
{
    public class SleepSequence : Sequence
    {
        private CatIA _catIa;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        
        public SleepSequence(CatIA catIa, NavMeshAgent navMeshAgent, Animator animator,float sleepDuration, float sleepTreshold, float sleepTimer) 
            : base(new List<Node>())
        {
            _catIa = catIa;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            
          MoveToSleepSpotAction moveToSleepSpotAction = new MoveToSleepSpotAction(_catIa.transform, _navMeshAgent, _animator, _catIa);
              
              
           // children.Add(new CheckFatigueAction(_catIa,sleepTreshold));
            children.Add(moveToSleepSpotAction);
            children.Add(new SleepAction(_catIa.transform,sleepDuration, _animator, _catIa,sleepTimer));

        }
    }
}