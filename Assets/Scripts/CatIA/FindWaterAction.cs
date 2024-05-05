using UnityEngine;

namespace Scripts.CatIA
{
    public class FindWaterAction : Leaf
    {
        public Transform _closestWaterSource;
        private Transform _catPosition;
        private CatIA _catIA;
        

        public FindWaterAction(Transform catPosition, CatIA catIa) {
            _catPosition = catPosition;
            _catIA = catIa;
         
        }
        
        public override NodeState Evaluate() {
            _closestWaterSource = null;
            float minDistance = float.MaxValue;


            foreach (Transform watersource in _catIA.WaterSources) {
                float distance = Vector3.Distance(_catPosition.position, watersource.position);
                if (distance < minDistance) {
                    
                    minDistance = distance;
                    _closestWaterSource = watersource;
                    Debug.Log(_closestWaterSource);
                }
            }

            if (_closestWaterSource != null) { return NodeState.SUCCESS; }
            else { return NodeState.FAILURE; }
        }
    }
    
    
}