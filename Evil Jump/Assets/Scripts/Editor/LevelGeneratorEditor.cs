using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectSpawner))]
public class LevelGeneratorEditor : Editor
{
    void OnSceneGUI(){
        ObjectSpawner t = target as ObjectSpawner;
        Vector3 pos = t.platformSpawnPoint.position;
        Vector3[] verts = new Vector3[]
        {
            new Vector3(pos.x - t.platformRangeX/2 -0.6f, pos.y, pos.z), // - t.range
            new Vector3(pos.x + t.platformRangeX/2 +0.6f, pos.y, pos.z ), // + t.range
            new Vector3(pos.x + t.platformRangeX/2 +0.6f, pos.y + t.maxPlatformPosY, pos.z ), // - t.range
            new Vector3(pos.x - t.platformRangeX/2 -0.6f, pos.y + t.maxPlatformPosY, pos.z ) // + t.range

        };
        
        Handles.DrawSolidRectangleWithOutline(verts, new Color(0.5f, 0.5f, 0.5f, 0.1f), new Color(0, 0, 0, 1));
    }
}
