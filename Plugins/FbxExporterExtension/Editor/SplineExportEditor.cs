using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FbxExporterExtend
{
    [CustomEditor(typeof(SplineExport))]
    public class SplineExportEditor : Editor
    {
        private const string resampleMessage = "In resample, the spline will be bake in a series of points. The points will be spaced according to the \"Resample Distance\" value. A smaller value means more points.";
        private const string bezierMessage = "In bezier mode, only knots and their tangents will be exported. For correct import into houdini. You must therefore specify that this is a bezier curve of order 4..";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SplineExport exporter = target as SplineExport;
            
            switch (exporter.exportType)
            {
                case SplineExport.ExportType.Resample:
                    {
                        EditorGUI.BeginChangeCheck();
                        float resampleDist = EditorGUILayout.FloatField("Resample Distance", exporter.resampleDistance);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(target, "Change Resample Distance");
                            exporter.resampleDistance = resampleDist;
                            EditorUtility.SetDirty(target);
                        }

                        EditorGUILayout.HelpBox(resampleMessage, MessageType.Info, true);
                    }
                    break;


                case SplineExport.ExportType.Bezier:
                    {
                        EditorGUILayout.HelpBox(bezierMessage, MessageType.Info, true);
                    }
                    break;
            }
        }
    }
}