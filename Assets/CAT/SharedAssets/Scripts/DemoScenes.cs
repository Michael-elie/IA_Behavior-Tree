using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace NekoLegends
{
    public class DemoScenes : MonoBehaviour
    {
        [SerializeField] protected Light directionalLight;
        [SerializeField] protected List<CameraDOFData> CameraDOFDatas;
        [SerializeField] protected List<CameraData> CameraDatas;
        [SerializeField] protected Transform BGTransform;

        [SerializeField] protected List<Transform> TargetPositions; //for game objects
        [SerializeField] protected Button GlobalVolumnBtn;
        [SerializeField] protected Volume GlobalVolume;
        [SerializeField] protected Button LogoBtn;
        [SerializeField] public TextMeshProUGUI DescriptionText;
        [SerializeField] protected GameObject DemoUI;

        private int currentIndex = 0;
        private bool isAnimating = false;
        protected float transitionSpeed = 1.0f;
        protected int currentCameraIndex = 0;
        protected bool isTransitioning = false;
        private GameObject lightObject;
        private DepthOfField _depthOfField;
        protected Dictionary<Button, UnityAction> buttonActions = new Dictionary<Button, UnityAction>();

        public bool isShowOutlines;

        [System.Serializable]
        public struct CameraDOFData //manual camera dof settings
        {
            public Transform CameraAngle;
            public float FocusDistance; //manual settings
            public float Aperture;//manual settings
            public float BackgroundScale; // This remains a single float
        }


        #region Singleton
        public static DemoScenes Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(DemoScenes)) as DemoScenes;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static DemoScenes _instance;
        #endregion

        protected virtual void Start()
        {

            if (!directionalLight)
                directionalLight = GameObject.FindObjectsOfType<Light>().FirstOrDefault(light => light.type == UnityEngine.LightType.Directional);

            //Application.targetFrameRate = 500;

            GlobalVolume.profile.TryGet<DepthOfField>(out _depthOfField);

        }

        protected virtual void OnEnable()
        {
            if(GlobalVolumnBtn)
                GlobalVolumnBtn.onClick.AddListener(GlobalVolumnBtnClicked);
            LogoBtn.onClick.AddListener(LogoBtnClicked);


            foreach (var pair in buttonActions)
            {
                pair.Key.onClick.AddListener(pair.Value);
                //Debug.Log(pair.Key.name);
            }
        }

        protected virtual void OnDisable()
        {
            if (GlobalVolumnBtn)
                GlobalVolumnBtn.onClick.RemoveListener(GlobalVolumnBtnClicked);
            LogoBtn.onClick.RemoveListener(LogoBtnClicked);


            foreach (var pair in buttonActions)
            {
                pair.Key.onClick.RemoveListener(pair.Value);
            }
        }

        protected void LogoBtnClicked()
        {
            DemoUI.SetActive(!DemoUI.activeSelf);
        }


        protected void GlobalVolumnBtnClicked()
        {
            GlobalVolume.enabled = !GlobalVolume.enabled;
        }
        

        protected void FlyToNextCameraHandler()
        {
            if (isTransitioning) return;

            int nextCameraIndex = (currentCameraIndex + 1) % CameraDOFDatas.Count;
            CameraDOFData nextCameraData = CameraDOFDatas[nextCameraIndex];

           
            if (nextCameraData.FocusDistance != 0f || nextCameraData.Aperture != 0f)
            {
                SetDOF(nextCameraData.FocusDistance, nextCameraData.Aperture);
            }
            

            if (BGTransform)
            {
                float targetScale = (nextCameraData.BackgroundScale != 0) ? nextCameraData.BackgroundScale : 1f;
                SetBackgroundScale(targetScale);
            }

            StartCoroutine(TransitionToNextCameraAngle(CameraDOFDatas[currentCameraIndex].CameraAngle, nextCameraData.CameraAngle));
            currentCameraIndex = nextCameraIndex;
        }

        public void SetDOFImmediate(float in_focusDistance, float in_aperture)
        {

            _depthOfField.focusDistance.value = in_focusDistance;
            _depthOfField.aperture.value = in_aperture;
        }


        protected void SetDOF(float targetValue, float targetAperture)
        {
            float currentFocusDistance = _depthOfField.focusDistance.value;
            float currentAperture = _depthOfField.aperture.value;

            StartCoroutine(TransitionDOF(currentFocusDistance, currentAperture, targetValue, targetAperture));
        }

        protected void SetBackgroundScale(float targetScale)
        {
            float currentScale = BGTransform.transform.localScale.x; // Assuming uniform scale
            StartCoroutine(TransitionBackgroundScale(currentScale, targetScale));
        }

        protected void ToggleLight()
        {
            if(!lightObject)
                lightObject = GameObject.Find("Directional Light");

            if (lightObject)
            {
                directionalLight = lightObject.GetComponent<Light>();
                directionalLight.enabled = !directionalLight.enabled;
            }
            else
            {
                Debug.LogWarning("Directional Light not found!");
            }

        }

        protected IEnumerator TransitionToNextCameraAngle(Transform fromAngle, Transform toAngle)
        {
            isTransitioning = true;
            float timeElapsed = 0;

            Vector3 startPosition = fromAngle.position;
            Quaternion startRotation = fromAngle.rotation;

            Vector3 endPosition = toAngle.position;
            Quaternion endRotation = toAngle.rotation;

            while (timeElapsed < transitionSpeed)
            {
                float t = timeElapsed / transitionSpeed;
                Camera.main.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                Camera.main.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Make sure we end at the exact position
            Camera.main.transform.position = endPosition;
            Camera.main.transform.rotation = endRotation;

            isTransitioning = false;
        }

        protected IEnumerator TransitionDOF(float startValue, float startAperture, float endValue, float endAperture)
        {
            float timeElapsed = 0;

            while (timeElapsed < transitionSpeed)
            {
                float t = timeElapsed / transitionSpeed;

                _depthOfField.focusDistance.value = Mathf.Lerp(startValue, endValue, t);
                _depthOfField.aperture.value = Mathf.Lerp(startAperture, endAperture, t);

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Ensure we end with the exact values
            _depthOfField.focusDistance.value = endValue;
            _depthOfField.aperture.value = endAperture;
        }

        protected IEnumerator TransitionBackgroundScale(float startScale, float endScale)
        {
            float timeElapsed = 0;

            while (timeElapsed < transitionSpeed)
            {
                float t = timeElapsed / transitionSpeed;

                float currentScale = Mathf.Lerp(startScale, endScale, t);
                BGTransform.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Ensure we end with the exact scale
            BGTransform.transform.localScale = new Vector3(endScale, endScale, endScale);
        }

        protected void AnimToNextDestination(Transform in_itemToMove)
        {
            if (!isAnimating)
            {
                // Move to the next index (looping back to the start if we reach the end)
                int nextIndex = (currentIndex + 1) % TargetPositions.Count;

                // Start the animation
                StartCoroutine(MoveToTarget(in_itemToMove, TargetPositions[nextIndex].position));

                // Update the current index
                currentIndex = nextIndex;
            }
        }

        IEnumerator MoveToTarget(Transform itemToMove, Vector3 endPosition)
        {
            isAnimating = true;
            float duration = 1f; // Animation duration in seconds
            float elapsedTime = 0f;

            Vector3 startPosition = itemToMove.position;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                float smoothedT = Mathf.SmoothStep(0.0f, 1.0f, t);  // SmoothStep easing

                // Update the position of the GameObject
                itemToMove.position = Vector3.Lerp(startPosition, endPosition, smoothedT);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Make sure the GameObject ends up in the exact end position
            itemToMove.position = endPosition;

            isAnimating = false;
        }

        public void SetDescriptionText(string inText)
        {

            DescriptionText.SetText(inText);
        }

        protected void RegisterButtonAction(Button button, UnityAction action)
        {
            buttonActions[button] = action;
        }


        protected void HideObjects(List<GameObject> in_OBJ)
        {
            foreach (var item in in_OBJ)
            {
                item.SetActive(false);
            }
        }
    }

}