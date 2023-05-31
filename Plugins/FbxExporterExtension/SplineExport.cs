using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace FbxExporterExtend
{
    [RequireComponent(typeof(SplineContainer))]
    public class SplineExport : PointCloudExport
    {
        public ExportType exportType;

        [HideInInspector]
        public float resampleDistance = 0.25f;

        const float minDistance = 0.001f;

        private void OnValidate()
        {
            resampleDistance = math.max(minDistance, resampleDistance);
        }

        public override Mesh GenerateFbxMesh()
        {
            Spline spline = GetComponent<SplineContainer>().Spline;
            List<Vector3> vertices = new List<Vector3>();
            BezierKnot[] knots = spline.Knots.ToArray();

            switch (exportType)
            {
                case ExportType.Resample:
                    {
                        const int maxPointCount = 10000;

                        float t = 0;
                        int failSafe = 0;
                        while (t < 1.0f && failSafe < maxPointCount)
                        {
                            spline.GetPointAtLinearDistance(t, resampleDistance, out t);
                            failSafe++;
                            vertices.Add(spline.EvaluatePosition(t));
                        }

                        for (int i = 0; i < spline.Count; i++)
                            vertices.Add(spline[i].Position);
                    }
                    break;

                case ExportType.Bezier:
                    {
                        for (int i = 0; i < knots.Length; i++)
                        {
                            Vector3 pos = knots[i].Position;
                            Quaternion rot = knots[i].Rotation;
                            Matrix4x4 mat = Matrix4x4.TRS(pos, rot, Vector3.one);

                            //Tangent In
                            if (i != 0)
                            {
                                vertices.Add(mat.MultiplyPoint(knots[i].TangentIn));
                            }

                            //Point
                            vertices.Add(pos);

                            //Tangent Out
                            if (i != knots.Length - 1)
                            {
                                vertices.Add(mat.MultiplyPoint(knots[i].TangentOut));
                            }
                        }
                    }
                    break;
            }

            return GenerateMeshForPoints(vertices.ToArray());
        }

        public enum ExportType
        {
            Resample,
            Bezier,
        }
    }
}