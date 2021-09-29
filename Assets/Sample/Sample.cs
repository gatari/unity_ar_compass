using UnityEngine;

namespace Sample
{
    public class Sample : MonoBehaviour
    {
        [SerializeField] private UnityARCompass.ARCompassIOS arCompassIOS;
        [SerializeField] private Transform compassObject;

        private void Update()
        {
            compassObject.rotation = arCompassIOS.TrueHeadingRotation;
        }
    }
}