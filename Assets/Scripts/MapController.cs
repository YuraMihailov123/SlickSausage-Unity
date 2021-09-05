using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    #region Singleton
    private static MapController _instance;
    public static MapController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Init();
    }

    #endregion

    [SerializeField]
    private GameObject voxelMesh;

    Transform mapRoot;
       

    private void Init()
    {
        voxelMesh = Resources.Load<GameObject>("Prefabs/VoxelMesh");

        mapRoot = transform.Find("Map").transform;

        GenerateMap(5);
    }

    private void GenerateMap(int countToSpawn)
    {
        int xPos = 0;
        int yPos = 0;
        for(int i = 0; i < countToSpawn; i++)
        {
            var tmp = mapRoot.AddChild(voxelMesh);
            var voxelData = tmp.GetComponent<VoxelRender>().voxelData;
            tmp.transform.localPosition = new Vector3(xPos, yPos, 0);
            tmp.transform.eulerAngles = new Vector3(0, -90, -90);
            yPos += Random.Range(0, 4);
            xPos += voxelData.Depth + voxelData.Depth/2;
        }
    }

    private void CreateWall()
    {

    }

}
