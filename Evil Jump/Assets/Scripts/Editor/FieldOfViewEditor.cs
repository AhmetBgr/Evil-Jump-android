using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (ShootingSystem))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI() {
		ShootingSystem fow = (ShootingSystem)target;
		Handles.color = Color.green;
		
		//Handles.DrawRectangle(0, new Vector3(fow.transform.position.x, fow.transform.position.y, 0f), Quaternion.identity, fow.viewRadius);
	

		//Handles.DrawWireArc (fow.transform.position, Vector3.forward, Vector3.right, 360, fow.viewRadius);
		
		Vector2 pointA = new Vector2(-3f, fow.center.transform.position.y +fow.viewDistance);
		Vector2 pointB = new Vector2(-3f, fow.center.transform.position.y -fow.viewDistance);

		Handles.DrawLine (pointA, pointA + Vector2.right * 6f);
		Handles.DrawLine (pointB, pointB + Vector2.right * 6f);

		float x;
		if(fow.transform.position.x > 0f){
			x = 270f;
		}
		else{
			x = 90f;
		}
		Vector3 viewAngleA = fow.DirFromAngle ((-fow.viewAngle / 2) + x, true);
		Vector3 viewAngleB = fow.DirFromAngle ((fow.viewAngle / 2) + x, true);

		//Vector3 viewAngleC = fow.DirFromAngle ((-fow.viewAngle / 2) +270, true);
		//Vector3 viewAngleD = fow.DirFromAngle ((fow.viewAngle / 2) +270, true);

		Handles.DrawLine (fow.center.transform.position, fow.center.transform.position + viewAngleA * 7f);
		Handles.DrawLine (fow.center.transform.position, fow.center.transform.position + viewAngleB * 7f);
		//Handles.DrawLine (fow.center.transform.position, fow.center.transform.position + viewAngleC * 7f);
		//Handles.DrawLine (fow.center.transform.position, fow.center.transform.position + viewAngleD * 7f);

		Handles.color = Color.red;
		if(fow.isInSight){
			Handles.DrawLine (fow.transform.position, fow.player.transform.position);
		}
	}

}
