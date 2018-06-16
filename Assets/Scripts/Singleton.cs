using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T GetInstance()
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(T)) as T;

            return instance;
        }
    }
}