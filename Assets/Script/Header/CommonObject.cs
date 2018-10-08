using UnityEngine;

namespace CommonObjectName
{
    static class Mgr
    {
        public const string MGRS = "Mgrs";
    }

    static class SurroundingsOfCharacter
    {
        public const string FOLLOWING_CAMERA = "FollowingCamera";
    }
}

namespace CommonObjectValue
{
    static class DefaultPosition
    {
        public static Vector3 FOLLOWING_CAMERA { get { return new Vector3(0.0f, 2.12f, -3.48f); } }
    }

    static class DefaultRotation
    {
        public static Quaternion FOLLOWING_CAMERA { get { return new Quaternion(9.0f, 0.0f, 0.0f, 0.0f); } }
    }
}