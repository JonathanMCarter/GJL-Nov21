using System.Collections.Generic;
using UnityEngine;

namespace DeadTired.Save
{
    public class SaveHelper
    {
        public static float[] Vector2(Vector2 vec) => new float[2] { vec.x, vec.y };
        public static Vector2 Vector2(float[] vec) => new Vector2(vec[0], vec[1]);
        
        public static float[] Vector3(Vector3 vec) => new float[3] { vec.x, vec.y, vec.z };
        public static Vector3 Vector3(float[] vec) => new Vector3(vec[0], vec[1], vec[2]);
        
        public static void SaveInt(string key, int value) => PlayerPrefs.SetInt(key, value);
        
        public static void SaveVector2(string key, float[] value) => SaveFloatArray(key, value);

        public static Vector2 GetVector2FromSave(string key) => Vector2(GetValuesInArray(key, 2));

        public static void SaveVector3(string key, float[] value) => SaveFloatArray(key, value);

        public static Vector3 GetVector3FromSave(string key) => Vector3(GetValuesInArray(key, 3));


        private static void SaveFloatArray(string key, float[] value)
        {
            for (var i = 0; i < value.Length; i++)
                PlayerPrefs.SetFloat($"{key}-{i}", value[i]);
        }

        private static float[] GetValuesInArray(string key, int length)
        {
            var _array = new float[length];
            
            for (var i = 0; i < length; i++)
                _array[i] = PlayerPrefs.GetFloat($"{key}-{i}");

            return _array;
        }
    }
}