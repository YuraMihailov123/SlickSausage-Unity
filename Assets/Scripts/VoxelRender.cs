using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class VoxelRender : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    public float scale = 1f;

    float adjScale;

    public VoxelData voxelData;

    private void Awake()
    {
        voxelData = new VoxelData();
        mesh = GetComponent<MeshFilter>().mesh;
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
                    continue;
                if (x == 0 && z == 0)
                    scale = 0.85f;
                else if (x == 0 && z == 1)
                    scale = 0.7f;
                else if (x == 0 && z == 2)
                    scale = 0.5f;
                else scale = 1f;
                adjScale = scale * 0.5f;
                if (z == 0)
                    distLine = adjScale;
                if (z > 0)
                    distLine += adjScale;

                //MakeCube(adjScale, new Vector3((float)x * (adjScale/0.5f), 0, (float)z * (adjScale/0.5f)));
                MakeCube(adjScale, new Vector3((float)(x-0.5f) * scale, 0, distLine));
                
                distLine += adjScale;
                Debug.Log(distLine);
            }
            distLine = 0;
        }
            //scale = z % 2 == 0 ? 0.5f : 1f;
        
    }

    void MakeCube(float cubeScale, Vector3 cubePos)
    {

        for(int i = 0; i < 6; i++)
        {
            MakeFace(i, cubeScale, cubePos);
        }
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
    }
}
