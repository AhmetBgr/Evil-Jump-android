using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnEnable(){
        int childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
