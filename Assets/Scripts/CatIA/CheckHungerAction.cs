using Scripts;
using UnityEngine;


namespace Scripts.CatIA
{
    public class CheckHungerAction : Leaf
    {
        private CatIA _catIa;
        private float _hungerThreshold;


        public CheckHungerAction(CatIA catIa, float hungerThreshold)
        {
            _catIa = catIa;
            _hungerThreshold = hungerThreshold;
        }
        
        public override NodeState Evaluate()
        {
            if (_catIa.CatHunger >= _hungerThreshold)
            {
                Debug.Log("le chat a faim");
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
    }
}