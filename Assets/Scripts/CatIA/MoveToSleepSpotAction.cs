using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.CatIA
{
    public class MoveToSleepSpotAction : Leaf
    {
        private Transform _catPosition;
        private NavMeshAgent _navMeshAgent;
        private Transform _target;
        private float _targetDistance = 1f;
        private Animator _animator;
        private CatIA _catIA;

        public MoveToSleepSpotAction(Transform catPosition, NavMeshAgent navMeshAgent, Animator animator, CatIA catIa)
        {
            _catPosition = catPosition;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _catIA = catIa;

        }
        
        
        public override NodeState Evaluate()
        {
            float minDistance = float.MaxValue;


            foreach (Transform sleepSpot in _catIA.SleepSpot) {
                float distance = Vector3.Distance(_catPosition.position, sleepSpot.position);
                if (distance < minDistance) {
                    
                    minDistance = distance;
                    _target = sleepSpot;
                   
                }
            }
            
            if (Vector3.Distance(_catPosition.position, _target.position) > _targetDistance) {
                _navMeshAgent.SetDestination(_target.position);
               Debug.Log("le chat se dirige vers une cachette");
                _animator.SetBool("IsWalking",true);
                _animator.SetBool("IsDrinking", false);
                return NodeState.RUNNING;
            }
            else
            {
                _navMeshAgent.ResetPath();
                _animator.SetBool("IsWalking",false);
                return NodeState.SUCCESS;
            }

        }
    }
}