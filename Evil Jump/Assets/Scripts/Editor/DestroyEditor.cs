﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Destroy))]
public class DestroyEditor : Editor
{
    void OnSceneGUI(){
        Destroy t = target as Destroy;
        Vector3 pos = t.transform.position;
        
        Vector3[] verts = new Vector3[]
        {
            new Vector3(-2.813f, pos.y, pos.z), // - t.range
            new Vector3(2.813f, pos.y, pos.z ), // + t.range
            new Vector3(2.813f, pos.y + t.height/2, pos.z ), // - t.range
            new Vector3(-2.813f, pos.y + t.height/2, pos.z ) // + t.range

        };

        Handles.DrawSolidRectangleWithOutline(verts, new Color(0.5f, 0.5f, 0.5f, 0.1f), new Color(0, 0, 0, 1));
    }
}
