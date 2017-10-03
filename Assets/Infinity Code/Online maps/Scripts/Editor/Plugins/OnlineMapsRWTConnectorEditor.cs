/*     INFINITY CODE 2013-2017      */
/*   http://www.infinity-code.com   */

using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (OnlineMapsRWTConnector))]
public class OnlineMapsRWTConnectorEditor : Editor 
{
    public override void OnInspectorGUI()
    {
#if !RWT && !RWT3
        if (GUILayout.Button("Enable Real World Terrain"))
        {
            if (EditorUtility.DisplayDialog("Enable Real World Terrain", "You have Real World Terrain in your project?", "Yes, I have Real World Terrain", "Cancel"))
            {
                Assembly assembly = typeof(OnlineMapsRWTConnectorEditor).Assembly;
                if (assembly.GetType("InfinityCode.RealWorldTerrain.Windows.RealWorldTerrainWindow") != null) OnlineMapsEditor.AddCompilerDirective("RWT3");
                else OnlineMapsEditor.AddCompilerDirective("RWT");
            }
        }
            
#else
        OnlineMapsRWTConnector connector = (OnlineMapsRWTConnector)target;

        connector.mode = (OnlineMapsRWTConnectorMode) EditorGUILayout.EnumPopup("Mode: ", connector.mode);

        if (connector.mode == OnlineMapsRWTConnectorMode.markerOnPosition)
        {
            connector.markerTexture = (Texture2D)EditorGUILayout.ObjectField("Marker Texture", connector.markerTexture, typeof (Texture2D), false);
            connector.markerLabel = EditorGUILayout.TextField("Marker Tooltip:", connector.markerLabel);
        }

        connector.positionMode = (OnlineMapsRWTConnectorPositionMode) EditorGUILayout.EnumPopup("Position mode: ", connector.positionMode);

        if (connector.positionMode == OnlineMapsRWTConnectorPositionMode.transform)
        {
            connector.targetTransform = (Transform) EditorGUILayout.ObjectField("Target Transform", connector.targetTransform, typeof (Transform), true);
        }
        else if (connector.positionMode == OnlineMapsRWTConnectorPositionMode.scenePosition)
        {
            connector.scenePosition = EditorGUILayout.Vector3Field("Position: ", connector.scenePosition);
        }
        else if (connector.positionMode == OnlineMapsRWTConnectorPositionMode.coordinates)
        {
            connector.coordinates = EditorGUILayout.Vector2Field("Coordinates: ", connector.coordinates);
        }
#endif
    }
}