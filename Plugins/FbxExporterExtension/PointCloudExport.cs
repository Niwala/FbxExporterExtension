using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace FbxExporterExtend
{
    public abstract class PointCloudExport : MonoBehaviour
    {
        public abstract Mesh GenerateFbxMesh();

        public virtual Mesh GenerateMeshForPoints(Vector3[] points)
        {
            int vertexCount = points.Length;
            int triCount = Mathf.CeilToInt(vertexCount / 3.0f) * 3;
            int[] triangles = new int[triCount];
            for (int i = 0; i < triCount; i += 3)
            {
                triangles[i + 0] = math.min(i, vertexCount - 1);
                triangles[i + 1] = math.min(i + 1, vertexCount - 1);
                triangles[i + 2] = math.min(i + 2, vertexCount - 1);
            }

            Mesh mesh = new Mesh();
            mesh.triangles = new int[0];
            mesh.vertices = points;
            mesh.triangles = triangles;
            mesh.name = gameObject.name;
            return mesh;
        }
    }
}