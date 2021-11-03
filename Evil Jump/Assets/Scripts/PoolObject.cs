using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public float height;
    public PoolObjectType type;
    public GameObject[] childrenToEnable;
    // Start is called before the first frame update
    void OnEnable()
    {
        // enable children
        foreach (var child in childrenToEnable)
        {
            child.SetActive(true);
        }
    }

}
