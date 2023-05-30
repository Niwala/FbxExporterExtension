using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;
using HoudiniEngineUnity;

namespace UnityToHoudiniCurveConverter
{
    public class UnityToHoudiniCurve : MonoBehaviour
    {
        public SplineContainer spline;

        [HideInInspector]
        public HEU_HoudiniAsset houdiniCurve;

        [HideInInspector]
        public HEU_HoudiniAssetRoot root;

        public void CheckComponents()
        {
            root = GetComponentInChildren<HEU_HoudiniAssetRoot>();

            if (root == null)
            {
                GameObject rootObj = HEU_HAPIUtility.CreateNewCurveAsset("Houdini Asset Root", transform, null, false);
                root = rootObj.GetComponent<HEU_HoudiniAssetRoot>();
            }

            houdiniCurve = root.HoudiniAsset;
        }

        public void BuildCurve()
        {
            int curveID = 0;
            HEU_Curve curve = houdiniCurve.Curves[curveID];
            BezierKnot[] knots = spline.Splines[curveID].Knots.ToArray();

            curve.ClearCurveNodeData();

            for (int i = 0; i < knots.Length; i++)
            {
                float3 pos = knots[i].Position;
                Quaternion rot = knots[i].Rotation;
                Matrix4x4 transform = Matrix4x4.TRS(pos, rot, Vector3.one);

                //Tangent In
                if (i != 0)
                {
                    curve.AddCurvePointToEnd(transform.MultiplyPoint(knots[i].TangentIn));
                }

                //Point
                curve.AddCurvePointToEnd(pos);

                //Tangent Out
                if (i != knots.Length - 1)
                {
                    curve.AddCurvePointToEnd(transform.MultiplyPoint(knots[i].TangentOut));
                }
            }

            curve.InputCurveInfo.curveType = HAPI_CurveType.HAPI_CURVETYPE_BEZIER;
            curve.InputCurveInfo.order = 4;
            curve.InputCurveInfo.closed = spline.Splines[curveID].Closed;
        }
    }
}