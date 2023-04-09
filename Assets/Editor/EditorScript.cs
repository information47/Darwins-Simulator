using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(FieldOfView))]
public class EditorScript : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.ViewRadius);
        Vector3 ViewAngleA = fow.DirFromAngle(-fow.ViewAngle/2, false);
        Vector3 ViewAngleB = fow.DirFromAngle(fow.ViewAngle /2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + ViewAngleA * fow.ViewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + ViewAngleB * fow.ViewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            if (visibleTarget != null)
            {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
            }
        }
    }
}
