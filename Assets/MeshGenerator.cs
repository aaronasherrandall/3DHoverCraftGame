using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    public float pNScale = .3f;
    public float nMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    private void OnValidate() 
    {
        CreateShape();
        UpdateMesh();
        
    }

    private void Update() 
    {
        
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        
        //loop through all of the vertices
        //i is local to for loop
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * pNScale, z * pNScale) * nMultiplier;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                
                vert++;
                tris += 6;

            }
            vert++;
        }

        

        
       
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // optionally, add a mesh collider (As suggested by Franku Kek via Youtube comments).
        // To use this, your MeshGenerator GameObject needs to have a mesh collider
        // component added to it.  Then, just re-enable the code below.
        
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        
    }

    private void OnDrawGizmos() 
    {
        if(vertices == null)
        {
            return;
        }


        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
        
    }



}
