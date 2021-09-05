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
    [SerializeField]
    int xPos = 0;
    public int yPos = 0;

    

    private void Init()
    {
        voxelMesh = Resources.Load<GameObject>("Prefabs/VoxelMesh");
        xPos = 0;
        yPos = 0;
        mapRoot = transform.Find("Map").transform;

        GenerateMap(7);
    }

    public void ResetParams()
    {
        xPos = 0;
        yPos = 0;
        GenerateMap(7);
    }

    public void GenerateMap(int countToSpawn)
    {
        for(int i = 0; i < countToSpawn; i++)
        {
            CreateWall();
        }
    }

    public void CheckIfNeedToAddNewWall()
    {
        if(xPos-CameraController.Instance.player.transform.localPosition.x < 20)
        {
            CreateWall();
        }
    }

    private void CreateWall()
    {
        var tmp = mapRoot.AddChild(voxelMesh);
        var voxelData = tmp.GetComponent<VoxelRender>().voxelData;
        tmp.transform.localPosition = new Vector3(xPos, yPos, 0);
        tmp.transform.eulerAngles = new Vector3(0, -90, -90);
        yPos += Random.Range(0, 4);
        xPos += voxelData.Depth + 1;
    }

}
