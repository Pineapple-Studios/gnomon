using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static Component Instance { get; private set; }

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as Component;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}