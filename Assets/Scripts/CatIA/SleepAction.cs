using UnityEngine;

namespace Scripts.CatIA
{
    public class SleepAction : Leaf
    {
        private Transform _catPosition;
        private Animator _animator;
        private float _sleepTimer;
        private float _sleepDuration;
        private CatIA _catIa;


        public SleepAction(Transform catPosition, float sleepDuration, Animator animator,CatIA catIa, float sleepTimer)
        {
            _catPosition = catPosition;
            _sleepDuration = sleepDuration;
            _animator = animator;
            _sleepTimer = sleepTimer;
            _catIa = catIa;
            
        }
        
        
        
        public override NodeState Evaluate()
        {
            _sleepTimer += Time.deltaTime;
            
            if (_sleepTimer >= _sleepDuration) {
                _catIa.CatFatigue = 0f;
               // Debug.Log("le chat a finit de dormir");
                _animator.SetBool("IsDrinking", false);
                _sleepTimer = 0;
                return NodeState.SUCCESS;
            }
            else {
                Debug.Log("le chat dort");
               _animator.SetBool("IsDrinking", true);
                return NodeState.RUNNING;
            }
           
           
           
        }
    }
}