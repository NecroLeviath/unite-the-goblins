using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR;

[ExecuteInEditMode]
public class Repeater : MonoBehaviour
{
    [Space]
    [Header("Info")]
    [Space]
    [TextArea]
    public string Description = "Creates replicas. Specify the Template Game Object and it will be repeated as instructed by the X and Y values, with collision.";

    [Header("Settings")]
    public List<GameObject> Templates;
    public List<Material> Materials;
    public bool GenerateCollider = true;
    
    public Vector3 ColliderOffsets;

    [Header("Repeats")]
    [Range(1, 100)]
    public int Z = 1;
    [Range(1, 100)]
    public int Y = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "block.png");
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4[] Transforms = new Matrix4x4[Z * Y];

        for (int z = 0; z < Z; z++)
        {
            for (int y = 0; y < Y; y++)
            {
                Quaternion quat = Quaternion.Euler(-90, 0, 180); // Compensate for unity ignoring Y-Up in FBX and I use 3dsmax.
                Transforms[(z*Y) + y].SetTRS(new Vector3(0, y*3, z*3) + transform.position, quat, Vector3.one);
            }
        }

        Vector3 colliderSize = Vector3.zero;

        // Instantiate and get the total required collider volume
        foreach (GameObject go in Templates)
        {
            if (go && go.GetComponent<MeshFilter>() && go.GetComponent<MeshRenderer>())
            {
                var mesh = go.GetComponent<MeshFilter>();
                var meshRenderer = go.GetComponent<MeshRenderer>();

                if (mesh.sharedMesh.bounds.size.x > colliderSize.x)
                    colliderSize.x = mesh.sharedMesh.bounds.size.x;
                if (mesh.sharedMesh.bounds.size.y > colliderSize.y)
                    colliderSize.y = mesh.sharedMesh.bounds.size.y;
                if (mesh.sharedMesh.bounds.size.z > colliderSize.z)
                    colliderSize.z = mesh.sharedMesh.bounds.size.z;

                if (mesh)
                {
                    Material material = null;
                    int i = Templates.IndexOf(go);
                    if (Materials.Count <= i + 1 && Materials[i])
                    {
                        material = Materials[i];
                    }

                    if (meshRenderer.sharedMaterial && meshRenderer.sharedMaterial.enableInstancing)
                        material = meshRenderer.material;

                    if(material == null)
                        throw new Exception("Repeater failed to run: Material number " + i + " is not set or the Template Material at this index does not have EnableInstancing set to true!");
                    Graphics.DrawMeshInstanced(mesh.sharedMesh, 0, material, Transforms);
                }
            }
        }

        if (GenerateCollider)
        {
            if (colliderSize != Vector3.zero)
            {
                // Figure out the actual bounds.
                // Creative rotation because of me using 3dsmax.
                float y = colliderSize.y;
                float z = colliderSize.z;
                colliderSize.y = z * Y;
                colliderSize.z = y * Z;

                var boxCollider = GetComponent<BoxCollider>();
                if (boxCollider == null)
                    boxCollider = gameObject.AddComponent<BoxCollider>();
                boxCollider.size = colliderSize;
                boxCollider.center = boxCollider.size / 2;
                boxCollider.center += ColliderOffsets;
            }
        }
        else
        {
            var boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
                DestroyImmediate(boxCollider);
        }
    }
}
