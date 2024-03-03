using UnityEngine;
using System.Collections;

public class PamuxIconGizmo : MonoBehaviour
{
#if UNITY_EDITOR
    public string iconName;

    void OnDrawGizmosSelected()
    {
        // Must be under Assets/Gizmos/icons - no subfolders

        Gizmos.DrawIcon(transform.position, $"icons/{this.iconName}.png", true);
    }
#endif
}