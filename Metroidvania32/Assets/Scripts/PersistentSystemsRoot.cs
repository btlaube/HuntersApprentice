using UnityEngine;
using System.Collections;

public class PersistentSystemsRoot : MonoBehaviour
{

    private bool _initialized;
    public static PersistentSystemsRoot Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        BootSequence();
    }

    private void BootSequence()
    {
        if (_initialized) return;

        _initialized = true;

        GameManager.Instance.OnBootSequence();
    }

}
