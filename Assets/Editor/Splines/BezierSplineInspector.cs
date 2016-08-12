using UnityEditor;
using UnityEngine;

/// <summary>
/// This class contains the custom inspector for BezierSplines.
/// </summary>
[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor
{
    //The steps per curve, used for drawing the bezierspline.
    private const int stepsPerCurve = 10;

    //The selected spline.
    private BezierSpline spline;

    //Refference to the handle position and rotation.
    private Transform handleTransform;
    private Quaternion handleRotation;

    //The visual size of the button.
    private const float handleSize = 0.04f;
    //The size of the button in regards to click detection.
    private const float pickSize = 0.06f;

    private static Color[] modeColors =
    {
        Color.white, //Free
        Color.yellow, //Aligned
        Color.cyan //Mirrored
    };

    //The index of the selected ControlPoint, starts at -1 to idicate that nothing is selected.
    private int selectedIndex = -1;

    //Scale factor used to scale the green direction lines.
    private const float directionScale = 0.5f;

    /// <summary>
    /// Used to set up custom inspector.
    /// </summary>
    public override void OnInspectorGUI()
    {
        //Stores the selected BezierSpline for easy access.
        spline = target as BezierSpline;

        //Creates a loop checkbox and checks if it's state changes.
        EditorGUI.BeginChangeCheck();
        bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
        if(EditorGUI.EndChangeCheck())
        {
            //Enables undo.
            Undo.RecordObject(spline, "Toggle Loop");
            EditorUtility.SetDirty(spline);

            //If checkbox changed state, set the loop bool on the selected spline.
            spline.Loop = loop;
        }

        //If the selected point's index is bigger then 0 but smaller than it's total ControlPointCount.
        if(selectedIndex >= 0 && selectedIndex < spline.ControlPointCount)
        {
            //Draw the inspector for the selected point.
            DrawSelectedPointInspector();
        }

        //Adds "Add Curve" button with undo enabled.
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");

            spline.AddCurve();

            EditorUtility.SetDirty(spline);
        }
    }

    /// <summary>
    /// Used to draw the part of the custom inspector pertaining to the selected point.
    /// Contains a Vector3Field where the position can be set without using the handle.
    /// Contains a dropdown where the ControlPointMode of the selected point can be changed.
    /// </summary>
    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");

        //Creates a Vector3Field, initialized with the coordinates of the selected ControlPoint.
        //Checks if the state of the vector field.
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
        if(EditorGUI.EndChangeCheck())
        {
            //If state changed, set the position of the selected ControlPoint to the value of the vector field.
            Undo.RecordObject(spline, "Move Point");

            Debug.Log(selectedIndex);

            EditorUtility.SetDirty(spline);
            spline.SetControlPoint(selectedIndex, point);
        }

        //Creates a dropdown of ControlPointModes.
        EditorGUI.BeginChangeCheck();
        BezierControlPointMode mode = (BezierControlPointMode)
            EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));
        if(EditorGUI.EndChangeCheck())
        {
            //Set ControlPointMode at selected index to the one selected in the dropdown.
            Undo.RecordObject(spline, "Change Point Mode");
            spline.SetControlPointMode(selectedIndex, mode);
            EditorUtility.SetDirty(spline);
        }
    }

    /// <summary>
    /// Called when an object of the type BezierSpline is selected in the scene view.
    /// Used to draw the handles and the spline itself.
    /// </summary>
    private void OnSceneGUI()
    {
        //Reference to the selected spline for easy access.
        spline = target as BezierSpline;

        //Sets the position of the base handle to the position of the spline object.
        //(Not the ControlPoints)
        handleTransform = spline.transform;

        //If pivotRotation is local, set handleRotation to handleTransform rotation.
        //If pivotRotation isn't local, set handleRotation to Quaternion.identity. (No rotation)
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        //Shows the first ControlPoint and keeps a reference to it's position.
        Vector3 p0 = ShowPoint(0);

        //Draws all points in the spline.
        for (int i = 1; i < spline.ControlPointCount; i += 3)
        {
            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i + 1);
            Vector3 p3 = ShowPoint(i + 2);

            //Draws the un-interpolated lines.
            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);

            //Draws the bezier curve.
            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
            
            //Saves the last point so it can be skipped next loop.
            p0 = p3;
        }

        ShowDirections();
    }

    /// <summary>
    /// Draws the green lines representing the direction of movement on individual point of the spline.
    /// </summary>
    private void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 point = spline.GetPoint(0f);

        Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);
        int steps = stepsPerCurve * spline.CurveCount;
        for (int i = 1; i < steps; i++)
        {
            point = spline.GetPoint(i / (float)steps);
            Handles.DrawLine(point, point + spline.GetDirection(i / (float)steps) * directionScale);
        }
    }

    /// <summary>
    /// Draws the point handles and allows the positions of the ControlPoints to be updated by moving the handles.
    /// </summary>
    /// <param name="index">The index of the point to display.</param>
    /// <returns>The position of the point at the index.</returns>
    private Vector3 ShowPoint(int index)
    {
        //point = selected ControlPoint position in world space.
        Vector3 point = handleTransform.TransformPoint(spline.GetControlPoint(index));

        //Gets the size of the handle.
        //The handle scales up when the scene camera zooms out, 
        //used to scale the ControlPoint handles for convenience.
        float size = HandleUtility.GetHandleSize(point);

        //Sets the color of the handle based on the mode of the ControlPoint.
        Handles.color = modeColors[(int)spline.GetControlPointMode(index)];

        //Draw the ControlPoint handles, returns true if handle is clicked.
        if(Handles.Button(point, handleRotation, handleSize, pickSize, Handles.DotCap))
        {
            //Set selectedIndex to the current index if clicked.
            selectedIndex = index;

            //Calls repaint to update the custom inspector with the newly selected ControlPoint.
            Repaint();
        }

        //If the ControlPoint is already selected.
        if (selectedIndex == index)
        {
            //Checks if the handles is moved.
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);

                //If handle is moved, update position of the ControlPoint.
                spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
            }
        }

        return point;
    }
}
