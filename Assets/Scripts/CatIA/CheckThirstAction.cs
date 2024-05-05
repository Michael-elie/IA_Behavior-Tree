using Scripts;
using UnityEngine;


namespace Scripts.CatIA
{
    public class CheckThirstAction : Leaf
    {
        private CatIA _catIa;
        private float _thirstThreshold;


        public CheckThirstAction(CatIA catIa, float thirstThreshold)
        {
            _catIa = catIa;
            _thirstThreshold = thirstThreshold;
        }
        
        public override NodeState Evaluate()
        {
            if (_catIa.CatThirst >= _thirstThreshold)
            {
                Debug.Log("le chat a soif");
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
    }
}