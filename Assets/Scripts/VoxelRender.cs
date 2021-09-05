using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer), typeof(MeshCollider))]
public class VoxelRender : MonoBehaviour
{
    MeshCollider meshCollider;
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    public float scale = 1f;

    float adjScale;

    public VoxelData voxelData;

    private void Awake()
    {
        voxelData = new VoxelData();
        voxelData.GenerateData();
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        //meshCollider.convex = true;
        adjScale = scale * 0.5f;
    }

    private void Start()
    {
        
        GenerateVoxelMesh(voxelData);
        UpdateMesh();
    }

    void GenerateVoxelMesh(VoxelData data)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        float distLine = 0;

        for (int x = 0; x < data.Width; x++)
        {
            for (int z = 0; z < data.Depth; z++)
            {
                if (data.GetCell(x, z) == 0)
                {
                    continue;
                }
                /*if (x == 0 && z == 0)
                    scale = 0.5f;
                else if (x == 0 && z == 1)
                    scale = 0.75f;
                else if (x == 0 && z == 2)
                    scale = 0.85f;
                else scale = 1f;*/
                adjScale = scale * 0.5f;
                if (z == 0)
                    distLine = adjScale;
                if (z > 0)
                    distLine += adjScale;

                MakeCube(adjScale, new Vector3((float)(x-0.5f) * scale, 0, distLine));
                
                distLine += adjScale;
            }
            distLine = 0;
        }
        
    }

    void MakeCube(float cubeScale, Vector3 cubePos)
    {

        for(int i = 0; i < 6; i++)
        {
            MakeFace(i, cubeScale, cubePos);
        }
        CubeMeshData.ResetVertices();
    }

    void MakeFace(int dir,float faceScale, Vector3 facePos)
    {
        vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale, facePos));

        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4+1);
        triangles.Add(vCount - 4+2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4+2);
        triangles.Add(vCount - 4+3);
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
}
