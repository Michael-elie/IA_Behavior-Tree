using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NekoLegends
{
    public class DemoCameraScene : DemoScenes
    {

        [SerializeField] private DemoCameraController _cameraController;
        [Space]
        [SerializeField] private Button ChangeCameraBtn;

        [SerializeField] private List<Transform> Targets;

        private int currentTargetIndex = 0; // To keep track of the current target index

        protected override void Start()
        {
            base.Start();
            // Optional: Initialize the camera's target if needed
            _cameraController.target = Targets[currentTargetIndex];
        }

        protected override void OnEnable()
        {
            if (ChangeCameraBtn)
                ChangeCameraBtn.onClick.AddListener(ChangeTarget); // Register the new button action

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            if (ChangeCameraBtn)
                ChangeCameraBtn.onClick.RemoveListener(ChangeTarget); // Remember to remove the listener to prevent memory leaks

            base.OnDisable();
        }

        private void ChangeTarget()
        {
            currentTargetIndex = (currentTargetIndex + 1) % Targets.Count; // Increment the index and wrap around if necessary
            _cameraController.target = Targets[currentTargetIndex]; // Set the new target
            _cameraController.AutoDOFTarget = Targets[currentTargetIndex];
        }
    }
}
