# FbxExporterExtension
> ⚠️ This extension relies a lot on C# reflection as the FBX Exporter package does not expose its functions. So it could quickly become obsolete with new versions of the FB Exporter. (Based on version 4.1.3)

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

Then, using the GameObject > Export FBX function will automatically include meshes having the indicated points as vertices.
