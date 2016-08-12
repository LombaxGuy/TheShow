using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor {

    private void OnSceneGUI()
    {
        //target is the selected object.
        Line line = target as Line;

        Transform handleTransform = line.transform;

        //Sets the rotation of the pivots to global space.
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        //Convert points to world space.
        Vector3 p0 = handleTransform.TransformPoint(line.p0);
        Vector3 p1 = handleTransform.TransformPoint(line.p1);

        //Draws the line between the points.
        Handles.color = Color.white;
        Handles.DrawLine(p0, p1);

        //Create position handles for the points in the line.
        Handles.DoPositionHandle(p0, handleRotation);
        Handles.DoPositionHandle(p1, handleRotation);

        //Checks if the handler has changed position.
        EditorGUI.BeginChangeCheck();
        p0 = Handles.DoPositionHandle(p0, handleRotation);

        //If the position was changed
        if (EditorGUI.EndChangeCheck())
        {
            //Records changes to enable undoing.
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);

            //Update the line point position in localspace.
            line.p0 = handleTransform.InverseTransformPoint(p0);
        }

        EditorGUI.BeginChangeCheck();
        p1 = Handles.DoPositionHandle(p1, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);

            line.p1 = handleTransform.InverseTransformPoint(p1);
        }
    }

}
