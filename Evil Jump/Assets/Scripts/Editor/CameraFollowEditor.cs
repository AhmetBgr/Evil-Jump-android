using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor
{
    void OnSceneGUI(){
        CameraFollow t = target as CameraFollow;
        Vector3 pos = t.transform.position;

        Handles.color = Color.yellow;

        Vector3 upperLinePA = new Vector3(-3f,pos.y + t.upperBorderY, 0f);
        Vector3 lowerLinePA = new Vector3(-3f,pos.y + t.lowerBorderY, 0f);

        Handles.DrawLine(upperLinePA, upperLinePA + Vector3.right*6f);
        Handles.DrawLine(lowerLinePA, lowerLinePA + Vector3.right*6f);
    }
}
