using UnityEngine;

namespace Sample
{
    public class Sample : MonoBehaviour
    {
        [SerializeField] private UnityARCompass.UnityARCompass compass;
        [SerializeField] private Transform compassObject;

        private void Update()
        {
            compassObject.rotation = compass.TrueHeadingRotation;
        }
    }
}