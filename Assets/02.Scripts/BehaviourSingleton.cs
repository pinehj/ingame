using UnityEngine;
using System.Collections;

public class BehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T i = null;

    public static T Instance
    {
        get
        {
            if (i == null)
            {
                i = FindFirstObjectByType(typeof(T)) as T;
                if (i == null)
                {

                }
            }
            return i;
        }
        set
        {
            i = value;
        }
    }
}