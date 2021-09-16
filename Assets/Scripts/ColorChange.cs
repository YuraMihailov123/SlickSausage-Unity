using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField]
    List<Material> materials;
    void Start()
    {
        materials = new List<Material>(Resources.LoadAll<Material>("Materials"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
