using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autodesk.Fbx;
using UnityEditor.Formats.Fbx.Exporter;
using UnityEditor;
using UnityEngine.Splines;
using System.Reflection;
using System.Linq;
using System;

namespace FbxExporterExtend
{
    /// <summary>
    /// Class heavily relying on C# reflection to add custom export components on FBXExporter internal functions.
    /// </summary>
    public static class FbxExporterExtensions
    {
        const string registerCallbackMethodName = "RegisterMeshCallback";
        const string exportMeshMethodName = "ExportMesh";
        const string callbackTypeFullname = "UnityEditor.Formats.Fbx.Exporter.GetMeshForComponent`1";
        private static MethodInfo exportMeshMethod;

        [InitializeOnLoadMethod]
        public static void Export()
        {
            //Get exportMesh method
            exportMeshMethod = typeof(ModelExporter).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == exportMeshMethodName && x.GetParameters()[0].ParameterType == typeof(Mesh));

            //Register callbacks
            GenerateMeshForComponent<PointCloudExport>(nameof(GetMeshFromPointCloud));
        }

        private static void GenerateMeshForComponent<T>(string methodName) where T : MonoBehaviour
        {
            Type callbackType = Assembly.GetAssembly(typeof(ModelExporter)).GetType(callbackTypeFullname);
            callbackType = callbackType.MakeGenericType(typeof(T));

            MethodInfo callbackMethod = typeof(FbxExporterExtensions).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            Delegate callback = Delegate.CreateDelegate(callbackType, callbackMethod);

            MethodInfo registerMethod = typeof(ModelExporter).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).FirstOrDefault(x => x.Name == registerCallbackMethodName && x.IsGenericMethod);
            MethodInfo generic = registerMethod.MakeGenericMethod(typeof(T));
            generic.Invoke(null, new object[] { callback, true });
        }


        private static bool GetMeshFromPointCloud(ModelExporter exporter, PointCloudExport component, FbxNode fbxNode)
        {
            return (bool)exportMeshMethod.Invoke(exporter, new object[] { component.GenerateFbxMesh(), fbxNode, new Material[0] });
        }
    }
}