using UnityEngine;

public class AddMeshCollider : MonoBehaviour
{
    void Start()
    {
        // Get all the child GameObjects of the parent GameObject
        Transform[] children = GetComponentsInChildren<Transform>();

        // Iterate through each child GameObject
        foreach (Transform child in children)
        {
            // Check if the child GameObject has a mesh
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.mesh != null)
            {
                // Add a mesh collider to the child GameObject
                MeshCollider collider = child.gameObject.AddComponent<MeshCollider>();
                collider.sharedMesh = meshFilter.mesh;
                collider.convex = true;
            }
        }
    }
}
