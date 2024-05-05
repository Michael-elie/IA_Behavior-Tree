using UnityEngine;

namespace Scripts.CatIA
{
    public class EatAction : Leaf
    {
        private Transform _catPosition;
        private Animator _animator;
        private float _eatTimer;
        private float _eatDuration;
        private CatIA _catIa;


        public EatAction(Transform catPosition, float eatDuration, Animator animator,CatIA catIa,float eatTimer)
        {
            _catPosition = catPosition;
           _eatDuration = eatDuration;
            _animator = animator;
            _eatTimer = eatTimer ;
            _catIa = catIa;
            
        }
        
        
        
        public override NodeState Evaluate()
        {
            _eatTimer += Time.deltaTime;
            
            if (_eatTimer >= _eatDuration) {
                _catIa.CatHunger = 0f;
                Debug.Log("le chat a finit de manger");
                _animator.SetBool("IsDrinking", false);
                _eatTimer = 0;
                return NodeState.SUCCESS;
            }
            else {
                Debug.Log("le chat mange");
               _animator.SetBool("IsDrinking", true);
                return NodeState.RUNNING;
            }
        }
    }
}