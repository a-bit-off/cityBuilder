using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GridSpawnn gridSpawnn;
    public GameObject[] Cubes;
    private int length;

    private void Start()
    {
        CubeSpawn();
    }

    public void CubeSpawn()
    {
        for (int i = 0; i < 1000; i++)
        {
            int size = (int)Random.Range(1, 4);
            Vector3 pos = gridSpawnn.allPlaneSandGrassPositions[Random.Range(0, gridSpawnn.allPlaneSandGrassPositions.Count + 1)];

            if (ContainsList(pos, size))
            {
                length -= size;
                Instantiate(Cubes[size - 1], pos, transform.rotation);
            }
                
        }
    }

    private bool ContainsList(Vector3 pos, int size)
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                if (gridSpawnn.allPlaneSandGrassPositions.Contains(new Vector3(pos.x + x, pos.y, pos.z + z)) == false)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
