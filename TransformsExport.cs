using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformsExport : PointCloudExport
{
    public override Mesh GenerateFbxMesh()
    {
        Vector3[] positions = new Vector3[transform.childCount];
        Matrix4x4 w2l = transform.worldToLocalMatrix;

        for (int i = 0; i < transform.childCount; i++)
        {
            positions[i] = w2l.MultiplyPoint(transform.GetChild(i).position);
        }

        return GenerateMeshForPoints(positions);
    }
}
