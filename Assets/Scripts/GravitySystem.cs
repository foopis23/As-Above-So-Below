using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    private static GravitySystem _instance;
    public static GravitySystem Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<GravitySystem>();
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public Vector3 gravityScale;
}
