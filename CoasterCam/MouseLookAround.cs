using UnityEngine;

namespace CoasterCam
{
    public class MouseLookAround : MonoBehaviour
    {
        public enum RotationAxes
        {
            MouseXAndY = 0, MouseX = 1, MouseY = 2
        }

        public RotationAxes Axes = RotationAxes.MouseXAndY;

        private float _sensitivityX = 10F;
        private float _sensitivityY = 10F;
        private float _minimumX = -135F;
        private float _maximumX = 135F;
        private float _minimumY = -60F;
        private float _maximumY = 60F;

        private float _rotationY;
        private IMouseTool _originalMouseTool;

        void Update()
        {
            // only move if right mouse button is pressed
            if(!Input.GetMouseButton(1))
            {
                return;
            }

            if (Axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _sensitivityX;

                _rotationY += Input.GetAxis("Mouse Y") * _sensitivityY;
                _rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

                transform.localEulerAngles = new Vector3(-_rotationY, rotationX, 0);
            }
            else if (Axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityX, 0);
            }
            else
            {
                _rotationY += Input.GetAxis("Mouse Y") * _sensitivityY;
                _rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

                transform.localEulerAngles = new Vector3(-_rotationY, transform.localEulerAngles.y, 0);
            }
        }

        private IMouseTool _disableMouseTool;

        void OnEnable()
        {
            _disableMouseTool = new DisableMouseInteractionMouseTool();
            _originalMouseTool = GameController.Instance.getActiveMouseTool();
            GameController.Instance.enableMouseTool(_disableMouseTool);
        }
        void OnDisable()
        {
            GameController.Instance.enableMouseTool(_originalMouseTool);
            GameController.Instance.removeMouseTool(_disableMouseTool);
        }
        private class DisableMouseInteractionMouseTool : AbstractMouseTool
        {
            public override bool canEscapeMouseTool() => false;

            public override GameController.GameFeature getDisallowedFeatures()
                =>  GameController.GameFeature.Picking
                    | GameController.GameFeature.Delete
                    | GameController.GameFeature.DragDelete;
    }
    }
}
