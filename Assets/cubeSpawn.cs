using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawn : MonoBehaviour
{
    public GameObject cubePrefab;

    public void SpawnCube()
    {
        Instantiate(cubePrefab, transform);
    }
}
