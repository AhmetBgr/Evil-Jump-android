using UnityEngine;
using UnityEngine.UI;

public class Duration : MonoBehaviour
{
    public Image icon;
    
    public  Color green;
    public Color red;

    [HideInInspector] public Collectable currentCol;
    [HideInInspector] public Sprite currentIcon;
    [HideInInspector] public float duration;

    private Image durImage;
    private float t = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        durImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update(){

        // Updates duration ui for passive power ups when picked up 
        if(currentCol != null){
            currentCol.Duration();
            durImage.fillAmount -= 1.0f / currentCol.defDuration * Time.deltaTime;
            t += Time.deltaTime / currentCol.defDuration;
            durImage.color = Color.Lerp(green, red, t);
            if(currentCol.isdurationFinished){
                currentCol = null;
                Reset();
                gameObject.SetActive(false);
            }
        }
    }

    public void Reset(){  
        durImage.fillAmount = 1f;
        durImage.color = green;
        
        t = 0f;
    }
}
