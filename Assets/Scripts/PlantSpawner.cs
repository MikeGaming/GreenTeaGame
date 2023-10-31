using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    [SerializeField] GameObject plantGroup;
    [SerializeField] int worldWidth = 10;
    [SerializeField] int worldHeight = 5;

    void Start()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                GameObject block = Instantiate(plantGroup, Vector3.zero, plantGroup.transform.rotation) as GameObject;
                block.transform.parent = transform;
                block.transform.localPosition = new Vector3(x * 4, 0, z * 1.75f);
            }
        }
    }
}
