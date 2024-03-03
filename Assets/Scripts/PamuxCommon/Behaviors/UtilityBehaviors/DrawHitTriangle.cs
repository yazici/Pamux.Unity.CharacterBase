// This script draws a debug line around mesh triangles 
// as you move the mouse over them.
using UnityEngine;
using System.Collections;

public class DrawHitTriangle : MonoBehaviour
{
  public new Camera camera;

  void Update()
  {
    RaycastHit hit;
    if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
    {
      return;
    }

    MeshCollider meshCollider = hit.collider as MeshCollider;
    if (meshCollider == null || meshCollider.sharedMesh == null)
    {
      Debug.Log("NOT MESH");
      return;
    }

    if (hit.triangleIndex == -1)
    {
      //Debug.Log("-1 HIT");
      return;
    }

    Mesh mesh = meshCollider.sharedMesh;
    Vector3[] vertices = mesh.vertices;
    int[] triangles = mesh.triangles;
    Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
    Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
    Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
    Transform hitTransform = hit.collider.transform;
    p0 = hitTransform.TransformPoint(p0);
    p1 = hitTransform.TransformPoint(p1);
    p2 = hitTransform.TransformPoint(p2);
    Debug.DrawLine(p0, p1);
    Debug.DrawLine(p1, p2);
    Debug.DrawLine(p2, p0);
  }
}