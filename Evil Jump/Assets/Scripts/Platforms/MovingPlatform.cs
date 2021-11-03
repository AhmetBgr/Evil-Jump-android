using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : PlatformDef
{   
    public Freezer freezer;
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void  OnTriggerEnter2D(Collider2D other){
        base.OnTriggerEnter2D(other);
    }

    void OnDisable(){

    }

}
