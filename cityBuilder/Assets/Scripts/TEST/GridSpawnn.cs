using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSpawnn : MonoBehaviour
{
    public GameObject[] Plane;
    public List<Vector3> allPlaneWaterPositions = new List<Vector3> { }; //динамический список занятых позиций
    public List<Vector3> allPlaneSwampPositions = new List<Vector3> { }; //динамический список занятых позиций
    public List<Vector3> allPlaneSandGrassPositions = new List<Vector3> { }; //динамический список занятых позиций



    private void Awake()
    {
        GridSpawner();
    }

    private void GridSpawner()
    {
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                float num = Random.Range(0f, 100f);
                Vector3 pos = new Vector3(x, 0, y);
                if (num < 90) //SandGrass
                {
                    Instantiate(Plane[0], pos, transform.rotation);
                }
                else if (num >= 90 && num < 95) //swamp
                {
                    Instantiate(Plane[1], pos, transform.rotation);
                    allPlaneSwampPositions.Add(pos);
                }
                else if (num >= 95 && num <= 100) //water
                {
                    Instantiate(Plane[2], pos, transform.rotation);
                    allPlaneWaterPositions.Add(pos);
                }
            }
        }
    }

    public bool IsPositionEmpty(Vector3 targetPos) //функция будет возвращать false или true в зависимости свободно мсето для расположения КУБА или нет
    {
        int x = (int)targetPos.x;
        int z = (int)targetPos.z;

        for (int i = 0; i < 3; i++)
        {
            targetPos.x += i;
            for(int j = 0; j < 3; j++)
            {
                targetPos.z += j;
                foreach (Vector3 pos in allPlaneWaterPositions)//перебирает все доступные позиции, если обнаружено совапдение то возвращает FALSE
                {
                    if (pos.x == targetPos.x && pos.z == targetPos.z)
                        return true;
                }
                foreach (Vector3 pos in allPlaneSwampPositions)//перебирает все доступные позиции, если обнаружено совапдение то возвращает FALSE
                {
                    if (pos.x == targetPos.x && pos.z == targetPos.z)
                        return true;
                }
                targetPos.z = z;
            }
            targetPos.x = x;
        }
        return false;
    }
}
