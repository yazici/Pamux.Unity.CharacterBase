
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
// [RequireComponent(typeof(SkinnedMeshRenderer))]
 [RequireComponent(typeof(Material))]
[ExecuteInEditMode]
public class PamuxMeshGizmo : MonoBehaviour
{
#if UNITY_EDITOR
    public Material lineMaterial;
    public MeshFilter meshFilter;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private bool showMeshOutline;


    void OnDrawGizmos() {
        // if (gameObject.Equals(Selection.activeGameObject)) {
		// 	Gizmos.color = Color.yellow;
        // } else {
        //     Gizmos.color = Color.yellow;
        // }
        // if (Application.isEditor && meshFilter.sharedMesh != null && showMeshOutline) {
        //     // CalculateVertexColors();
        //     // GL.wireframe = true;
        //     //LineMaterial.SetPass(0);
            
        //     // GL.wireframe = false;
        // }
        // Graphics.DrawMeshNow(meshFilter.sharedMesh, transform.position, transform.rotation);

        
            // set first shader pass of the material
            lineMaterial.SetPass(0);
            // draw mesh at the origin
            Graphics.DrawMeshNow(meshFilter.sharedMesh, transform.position, transform.rotation);

    }
#endif
}