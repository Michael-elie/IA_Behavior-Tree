using Scripts;
using UnityEngine;


namespace Scripts.CatIA
{
    public class CheckFatigueAction : Leaf
    {
        private CatIA _catIa;
        private float _fatigueThreshold;


        public CheckFatigueAction (CatIA catIa, float fatigueThreshold)
        {
            _catIa = catIa;
            _fatigueThreshold = fatigueThreshold;
        }
        
        public override NodeState Evaluate()
        {
            if (_catIa.CatFatigue >= _fatigueThreshold)
            {
                Debug.Log("le chat est fatigué");
                return NodeState.SUCCESS;
             
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
    }
}