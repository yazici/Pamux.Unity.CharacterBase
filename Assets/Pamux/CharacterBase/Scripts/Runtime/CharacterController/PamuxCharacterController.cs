using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

using Pamux.Lib.Unity.Commons;

using Pamux.Lib.Unity.CharacterBase.Authoring;

using Pamux.Lib.Unity.CharacterBase.Locomotion.Zones;
using Pamux.Lib.Unity.CharacterBase.Locomotion.Zones.Land;
using Pamux.Lib.Unity.CharacterBase.Locomotion.Zones.Water;

using Pamux.Lib.Unity.CharacterBase.Input;

using Pamux.Lib.Unity.CharacterBase.Utils;

namespace Pamux.Lib.Unity.CharacterBase
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(InteractingAgent))]

    [RequireComponent(typeof(LandLocomotionZoneAnimationEventHandler))]
    [RequireComponent(typeof(WaterLocomotionZoneAnimationEventHandler))]

    public class PamuxCharacterController : MonoBehaviour
    {
        private GameObject mainCameraParentGameObject;
        public GameObject MainCameraParentGameObject => this.mainCameraParentGameObject;

        public Vector3 CameraRotationInWorldSpace => this.mainCameraParentGameObject.transform.eulerAngles;

        private PamuxCharacterControllerCamera pccCamera;

        private GameObject playerFollowingCamera;
        private GameObject playerPOVCamera;

        private CharacterController characterController;

        private PlayerInput playerInput;
        public PlayerInput PlayerInput => playerInput;

        private PamuxCharacterDescription pcd;
        private InteractingAgent interactingAgent;

        private Animator animator;

        public CharacterController CharacterController => this.characterController;

        public Animator Animator => this.animator;
        public bool HasAnimator => this.animator != null;

        public bool IsCurrentDeviceMouse => this.playerInput.currentControlScheme == "KeyboardMouse";

        private LocomotionZoneBase currentLocomotionZone;
        public LocomotionZoneBase CurrentLocomotionZone { 
            get {
                return currentLocomotionZone;
            }

            set {
                this.currentLocomotionZone = value;
            } 
        }

        private LandLocomotion landLocomotion;
        public LandLocomotion LandLocomotion => this.landLocomotion;
        
        private WaterLocomotion waterLocomotion;
        public WaterLocomotion WaterLocomotion => this.waterLocomotion;

        private AnimatorParameters animatorParameters;
        public AnimatorParameters AnimatorParameters => this.animatorParameters;

        private ParsedInput parsedInput;
        public ParsedInput ParsedInput => this.parsedInput;

        public bool ConsumeInputs => !Singletons.AppLifecycleManager.IsPaused && Singletons.MouseCursorManager.IsOwned;
#region Unity API
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
        private void Awake()
        {
            SetupComponents();
            SetupLocomotionZoneHandlers();

            this.parsedInput.Initialize();
        }

        private void Start()
        {
            this.pccCamera.Start();

            this.landLocomotion.Start();
            this.waterLocomotion.Start();
        }

        private void Update()
        {
            this.currentLocomotionZone.Update();
            this.pccCamera.Update();
        }

        private void LateUpdate()
        {
            this.currentLocomotionZone.LateUpdate();
            this.pccCamera.LateUpdate();
        }

        private void OnEnable() {
             this.parsedInput.Enable();
        }

        private void OnDisable() {
            this.parsedInput.Disable();
        }
#endregion
/// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputValue.html
        public void OnSwitchTo1stPersonCamera(InputAction.CallbackContext context) {
            this.playerFollowingCamera.SetActive(false);
            this.playerPOVCamera.SetActive(true);
        }

        public void OnSwitchTo3rdPersonCamera(InputAction.CallbackContext context) {
            this.playerFollowingCamera.SetActive(true);
            this.playerPOVCamera.SetActive(false);
        }

        public void OnInteract(InputAction.CallbackContext context) {
            this.interactingAgent.OnInteract();
        }

        public void OnOpenInventory(InputAction.CallbackContext context) {
            Debug.Log("OnOpenInventory");
        }

        public void OnOpenRuntimeConsole(InputAction.CallbackContext context) {
            Debug.Log("OnOpenRuntimeConsole");
        }

        private void SetupComponents() {
            this.mainCameraParentGameObject = FindGameObjectWithMainCamera();


            this.parsedInput = new ParsedInput(this);

            this.playerInput = GetComponent<PlayerInput>();

            this.pccCamera = new PamuxCharacterControllerCamera(this);

            this.playerFollowingCamera = GameObject.FindGameObjectWithTag("PlayerFollowingCamera");
            this.playerPOVCamera = GameObject.FindGameObjectWithTag("PlayerPOVCamera");

            this.pcd = GetComponent<PamuxCharacterDescription>();
            this.interactingAgent = GetComponent<InteractingAgent>();

            this.characterController = GetComponent<CharacterController>();

            TryGetComponent(out this.animator);

            this.animatorParameters = new AnimatorParameters(this.animator);
        }

        private void SetupLocomotionZoneHandlers() {
            this.landLocomotion = new LandLocomotion(this, GetComponent<LandLocomotionZoneParameters>());
            this.waterLocomotion = new WaterLocomotion(this, GetComponent<WaterLocomotionZoneParameters>());

            this.currentLocomotionZone = this.landLocomotion;
        }

        private GameObject FindGameObjectWithMainCamera() {
            GameObject result;
            
            if (Camera.main != null) {
                result = Camera.main.gameObject;
                if (result != null) {
                    return result;
                }
            }
            
            result = GameObject.FindGameObjectWithTag("MainCamera");
            if (result != null) {
                return result;
            }

            var pamuxMainCameraTransform = this.transform.Find("PamuxMainCamera");
            if (pamuxMainCameraTransform != null) {
                result = pamuxMainCameraTransform.gameObject;
                if (result != null) {
                    return result;
                }
            }

            throw new Exception("Cannot discover a gameObject that owns the main camera.");
        }
    }
}