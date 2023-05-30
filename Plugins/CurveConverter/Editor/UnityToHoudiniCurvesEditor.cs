using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HoudiniEngineUnity;

namespace UnityToHoudiniCurveConverter
{
    [CustomEditor(typeof(UnityToHoudiniCurve))]
    public class UnityToHoudiniCurvesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UnityToHoudiniCurve u2h = target as UnityToHoudiniCurve;

            if (GUILayout.Button("Update Houdini Curve"))
            {
                u2h.CheckComponents();
                u2h.BuildCurve();
                HEU_EditorUtility.RebuildAssets(new HEU_HoudiniAssetRoot[] { u2h.root });
            }
        }
    }
}