using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Vector3Extensions
    {
        public static Vector3 WithoutXParameter(this Vector3 vector)
        {
            vector.x = 0;
            return vector;
        }

        public static Vector3 WithoutYParameter(this Vector3 vector)
        {
            vector.y = 0;
            return vector;
        }

        public static Vector3 WithoutZParameter(this Vector3 vector)
        {
            vector.z = 0;
            return vector;
        }

        public static float3 WithoutXParameter(this float3 vector)
        {
            vector.x = 0;
            return vector;
        }

        public static float3 WithoutYParameter(this float3 vector)
        {
            vector.y = 0;
            return vector;
        }

        public static float3 WithoutZParameter(this float3 vector)
        {
            vector.z = 0;
            return vector;
        }
    }
}