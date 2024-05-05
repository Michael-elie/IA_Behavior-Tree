using UnityEngine;

namespace Scripts.CatIA
{
    public class DrinkAction : Leaf
    {
        private Transform _catPosition;
        private Animator _animator;
        private float _drinkTimer;
        private float _drinkDuration;
        private CatIA _catIa;


        public DrinkAction(Transform catPosition, float drinkDuration, Animator animator,CatIA catIa,float drinkTimer)
        {
            _catPosition = catPosition;
            _drinkDuration = drinkDuration;
            _animator = animator;
            _drinkTimer = drinkTimer ;
            _catIa = catIa;
            
        }
        
        
        
        public override NodeState Evaluate()
        {
            _drinkTimer += Time.deltaTime;
            
            if (_drinkTimer >= _drinkDuration) {
                _catIa.CatThirst = 0f;
                Debug.Log("le chat a finit de boire");
                _animator.SetBool("IsDrinking", false);
                _drinkTimer = 0;
                return NodeState.SUCCESS;
            }
            else {
                Debug.Log("le chat boit");
               _animator.SetBool("IsDrinking", true);
                return NodeState.RUNNING;
            }
        }
    }
}