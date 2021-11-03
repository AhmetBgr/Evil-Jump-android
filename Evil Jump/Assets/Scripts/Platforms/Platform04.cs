using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform04 : MonoBehaviour
{
    public bool hasFreeze = false;

    public float period3;

    private GameObject leftP;
    private GameObject rightP;

    private float nextActionTime = 0.0f;
    private float nextActionTime2 = 0.0f;

    public float period = 1.5f;
    public float period2 = 1.5f;

    private bool late = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f,transform.position.y);
        leftP = transform.GetChild(1).gameObject;
        rightP = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //SwitchStatus(leftP);
        //SwitchStatus2(rightP);
        /*if(!late){
            StartCoroutine(LateCall());
            
        }
        else{
            SwitchStatus2(rightP);
        }*/

        //StartCoroutine(SwitchStatus(leftP));

        /*time += Time.deltaTime;
        if(!hasFreeze && time > period3){
            time = 0f;
            Teleport();
        }*/




        if(!hasFreeze){
            int rng = Random.Range(1,3);
            //transform.GetChild(rng).gameObject.SetActive(false);

            if(rightP.gameObject.activeSelf == true){
                Invoke("SwitchActive",1.5f);
            }

            if(leftP.gameObject.activeSelf == true){
                Invoke("SwitchActive2",1.5f);
            }
        }
        
    }
    private void Teleport(){
        if(transform.position.x > 0){
            Vector3 leftSidePos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transform.position = leftSidePos;
        }
        else{
            Vector3 rightSidePos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transform.position = rightSidePos;
        }
    }
    
    IEnumerator LateCall(){
        yield return new WaitForSeconds(0.5f);
        SwitchStatus2(rightP);
        //late = true;
    }

    void SwitchStatus(GameObject platform){
        
        if (Time.time > nextActionTime ) {
            nextActionTime += period;
            
            if(platform.activeSelf){
                platform.SetActive(false);
            }
            else{
                platform.SetActive(true);
            }
            
        }

    }
    void SwitchStatus2(GameObject platform){
        if(!late){
            period2 = 2f;
        }
  
        if (Time.time > nextActionTime2 ) {
            nextActionTime2 += period2;
            
            if(platform.activeSelf){
                platform.SetActive(false);
            }
            else{
                platform.SetActive(true);
            }
            late = true;
            period2 = 1.5f;
        }

    }
    /*IEnumerator SwitchStatus(GameObject platform){
        

            
        yield return new WaitForSeconds(1.5f);

        if(platform.activeSelf){
            platform.SetActive(false);
        }
        else{
            platform.SetActive(true);
        }
        

    }*/

    void SwitchActive(){
        if(!hasFreeze){
            leftP.gameObject.SetActive(true);
            rightP.gameObject.SetActive(false);
            //Invoke("SwitchDeactive",0.8f);
        }
        
    }

    void SwitchActive2(){
        if(!hasFreeze){
            rightP.gameObject.SetActive(true);
            leftP.gameObject.SetActive(false);
            //Invoke("SwitchDeactive2",0.8f);
        }
        
    }

    void SwitchDeactive(){
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void SwitchDeactive2(){
        transform.GetChild(1).gameObject.SetActive(false);
    }



}
