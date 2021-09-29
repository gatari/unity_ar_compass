using UnityEngine;

namespace UnityARCompass
{
    public class ARCompassIOS : MonoBehaviour, ICompass
    {
        private Camera _mainCamera;
        private double _lastCompassTimestamp;

        public Quaternion TrueHeadingRotation { get; private set; } = Quaternion.identity;

        #region Unity Callback

        private void Start()
        {
            Input.compass.enabled = true;
            Input.location.Start();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!(Input.compass.timestamp > _lastCompassTimestamp)) return;
            _lastCompassTimestamp = Input.compass.timestamp;

            var declination = -(Input.compass.trueHeading - Input.compass.magneticHeading);

            // iOSだとx軸右、y軸上、z軸画面手前
            UpdateRotation(
                new Vector3(Input.compass.rawVector.x, Input.compass.rawVector.y, -Input.compass.rawVector.z),
                declination);
        }

        #endregion

        private void UpdateRotation(Vector3 rawVector, float declination)
        {
            // compensate camera pose
            rawVector = _mainCamera.transform.rotation * rawVector;

            // projection onto xz plane
            var xzProjection =
                new Vector3(rawVector.x, 0, rawVector.z);

            var trueHeading = Quaternion.Euler(0, declination, 0) * xzProjection.normalized;

            // update global rotation
            TrueHeadingRotation =
                Quaternion.FromToRotation(Vector3.forward, trueHeading);
        }
    }
}