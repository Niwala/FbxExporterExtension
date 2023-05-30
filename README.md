# Fbx Exporter Extension
> ⚠️ This extension relies a lot on C# reflection as the FBX Exporter package does not expose its functions. So it could quickly become obsolete with new versions of the FB Exporter. (Based on version 4.1.3)

![Unity Objects](https://github.com/Niwala/FbxExporterExtension/blob/main/Media/UnityObjects.png)
![Houdini Objects](https://github.com/Niwala/FbxExporterExtension/blob/main/Media/HoudiniObjects.png)

Allows you to export points from Unity's Fbx exporter<br>
Convenient for retrieving data from third party software such as Houdini or Maya.

This plugin require the following packages:
 - FBX Exporter (com.unity.formats.fbx)
 - Splines (com.unity.splines)

&nbsp;

## Components
 - Transforms Export: Exports an object containing a point on each child position of this object.
 - Spline Export: Exports points placed on the first spline of a spline container.

&nbsp;

## Use
Add the Transforms Export or Spline Export components to the gameObjects of your scene.
*(The transform export must have gameObjects as children and the Spline Export must have a Spline in its SplineContainer component.)*

### Bezier Spline
You can export your Spline without resampling it. In this case, you'll need to treat it as a Bezier curve of order 4 in Houdini to get the right interpolation.<br>
![Export in Bezier Mode](https://github.com/Niwala/FbxExporterExtension/blob/main/Media/BezierCurve.png)

&nbsp;&nbsp;&nbsp;

# Curve Converter
Converts a Unity spline into a curve that can be used by the Houdini Engine plugin within Unity.

This plugin require the following packages:
 - Splines (com.unity.splines)
 - [Houdini Engine for Unity](https://www.sidefx.com/products/houdini-engine/plug-ins/unity-plug-in/)

&nbsp;

## Use
Place the Unity to Houdini Curve component on an object with a Unity spline in your scene. Then click on the "Update Houdini Curve" button to generate a curve compatible with the Houdini Engine plugin.
