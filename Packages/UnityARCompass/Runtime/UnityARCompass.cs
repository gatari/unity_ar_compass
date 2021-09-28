// #undef  UNITY_EDITOR
using UnityEngine;

namespace UnityARCompass
{
    public class UnityARCompass : MonoBehaviour
    {
        private Camera _mainCamera;
        private double _lastCompassTimestamp = 0;
        private Quaternion _rotation = Quaternion.identity;

#if UNITY_EDITOR
        [SerializeField] private float trueHeadingDebug = 0;
#endif

        #region Unity Callback

        private void Start()
        {
#if !UNITY_EDITOR
            Input.compass.enabled = true;
            Input.location.Start();
#endif
            _mainCamera = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR
            UpdateRotation(trueHeadingDebug);
#else
            if (Input.compass.timestamp > _lastCompassTimestamp)
            {
                UpdateRotation(Input.compass.trueHeading);
            }
#endif
        }

        #endregion

        public Quaternion TrueHeadingRotation => _rotation;

        private void UpdateRotation(float trueHeading)
        {
            // generate vector from true heading and screen space (camera) pose
            var screenSpaceTrueHeadingVector = Quaternion.AngleAxis(trueHeading, _mainCamera.transform.forward) *
                                               _mainCamera.transform.up;

            // projection onto xz plane
            var xzProjection =
                new Vector3(screenSpaceTrueHeadingVector.x, 0, screenSpaceTrueHeadingVector.z);

            // update rotation
            _rotation = Quaternion.FromToRotation(Vector3.forward, xzProjection.normalized);
        }
    }
}