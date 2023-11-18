using UnityEngine;
using Cinemachine;

namespace V10
{
    public class CinemachinePOVExtension : CinemachineExtension
    {


        public static CinemachinePOVExtension Instance { get; private set; }


        [Header("Mouse Settings")]
        [SerializeField] private float mouseSensitivity = 100f;


        private Transform playerBody;

        private Vector3 startingRotation;


        protected override void Awake()
        {
            Instance = this;

            base.Awake();
        }

        private void Start()
        {
            startingRotation = transform.localRotation.eulerAngles;
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            playerBody = playerTransform;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    Vector2 inputVector = GameInput.Instance.GetLookVector();

                    float mouseX = inputVector.x * mouseSensitivity * Time.deltaTime;
                    float mouseY = inputVector.y * mouseSensitivity * Time.deltaTime;

                    startingRotation.x += mouseX;
                    startingRotation.y -= mouseY;

                    startingRotation.y = Mathf.Clamp(startingRotation.y, -90f, 90f);

                    state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);

                    if (playerBody != null)
                    {
                        playerBody.Rotate(Vector3.up, mouseX);
                    }
                }
            }
        }


    }
}
