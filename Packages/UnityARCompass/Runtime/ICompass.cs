using UnityEngine;

namespace UnityARCompass
{
    public interface ICompass
    {
        Quaternion TrueHeadingRotation { get; }
    }
}