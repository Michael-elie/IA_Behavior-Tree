using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Scripts.CatIA
{
    public class CatIA : MonoBehaviour
    {
        //public bool IsAlive => true;
        public  float CatFatigue;
        public  float CatHunger;
        public  float CatThirst;
        public List<Transform> WaterSources;
        public List<Transform> SleepSpot;
        public List<Transform> Birds;
        public List<Transform> Mates;
        public float drinkDuration = 5f;
        public float SleepDuration = 10f;
        public float EatDuration = 5f;
        public float Drinktimer= 0f;
        public float Sleeptimer = 0f;
        public float Eattimer = 0f;
        public float Lovetimer = 0f;
        public float ThirstThreshold;
        public float SleepThreshold;
        public float EatThreshold;
        public float LoveThreshold;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Node _rootNode;

        private void Start()
        {
          

        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            CatThirst = 0f;
            
            DrinkSequence drinkSequence = new DrinkSequence(this, _navMeshAgent, _animator, drinkDuration,
               ThirstThreshold,Drinktimer);

            EatSequence eatSequence =
                new EatSequence(this, _navMeshAgent, _animator, EatDuration, EatThreshold, Eattimer);
            
            
            SleepSequence sleepSequence =
                new SleepSequence(this, _navMeshAgent, _animator, SleepDuration, SleepThreshold,Sleeptimer);

            
            
            // selector principal 
            _rootNode = new Selector(new List<Node> { sleepSequence,drinkSequence,eatSequence, });

        }
        
        void Update() {
            CatThirst = Mathf.MoveTowards(CatThirst, 1f, Time.deltaTime * 0.05f);
            //CatFatigue = Mathf.MoveTowards(CatFatigue, 1f, Time.deltaTime * 0.02f);
            CatHunger = Mathf.MoveTowards(CatHunger, 1f, Time.deltaTime * 0.04f);
            
            _rootNode.Evaluate();
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bird"))
            {
                Debug.Log("aataque loiseau");
                other.GetComponent<Animator>().SetTrigger("Dead");
                other.GetComponent<CatIA>().enabled = false;
                other.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }
}