using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;
    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("CoroutineRunner");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<CoroutineRunner>();
            }
            return _instance;
        }
    }

    public Coroutine Run(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }

    public void Stop(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
