using UnityEngine;

public class Disabler : MonoBehaviour
{
    private PoolManager poolManager;

    // Start is called before the first frame update
    void Start()
    {
        poolManager = PoolManager.SharedInstance;
    }
    
    // Disables object that get out of camera's fov
    void OnTriggerExit2D(Collider2D otherCol){
        GameObject other = otherCol.gameObject;

        if(!other.CompareTag("player") && !other.CompareTag("feet") && !other.CompareTag("dashPrediction")){

            if(other.CompareTag("platform")){

                // Checks if objects has collectable on it and if so, add to its container
                for (int i = 0; i < other.transform.childCount; i++){
                    GameObject child = other.transform.GetChild(i).gameObject;
                    
                    if(child.CompareTag("collectable")){
                        poolManager.AddToContainer(child, child.GetComponent<PoolObject>().type);  
                    }   
                }
            }

            if(transform.position.y > other.transform.position.y){
                Disable(other);
            }
        }
    }

    private void Disable(GameObject obj){
        if(obj.GetComponent<PoolObject>() != null){
            poolManager.DisableObject(obj, obj.GetComponent<PoolObject>().type);
        }
        else{
            obj.SetActive(false);
        }
    }
}
