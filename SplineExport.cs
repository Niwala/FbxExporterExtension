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
        public float distance;
        public bool useCatmullRom;

        const int maxPointCount = 10000;
        const float minDistance = 0.001f;

        private void OnValidate()
        {
            distance = math.max(minDistance, distance);
        }

        public override Mesh GenerateFbxMesh()
        {
            Spline spline = GetComponent<SplineContainer>().Spline;
            List<Vector3> vertices = new List<Vector3>();

            if (!useCatmullRom)
            {
                float t = 0;
                int failSafe = 0;
                while (t < 1.0f && failSafe < maxPointCount)
                {
                    spline.GetPointAtLinearDistance(t, distance, out t);
                    failSafe++;
                    vertices.Add(spline.EvaluatePosition(t));
                }
            }
            else
            {
                for (int i = 0; i < spline.Count; i++)
                    vertices.Add(spline[i].Position);
            }

            return GenerateMeshForPoints(vertices.ToArray());
        }
    }
}