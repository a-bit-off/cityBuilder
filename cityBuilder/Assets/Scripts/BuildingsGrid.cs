using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    private Vector2Int GridSize = new Vector2Int(100, 100); //размер сетки

    private Building[,] grid; //двумерный массив координат зданий
    private Building flyingBuilding;
    private Camera mainCamera;
    

    public GridSpawnn gridSpawnn;
    public Building[] Cubes;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
        //CubeSpawn();

    }

    public void StartPlacinngBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }


    private void Update()
    {
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 wordPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(wordPosition.x);
                int y = Mathf.RoundToInt(wordPosition.z);

                bool available = true;

                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) available = false;
                
                if (available && IsPlaceTaken(x, y)) available = false;
                if (gridSpawnn.IsPositionEmpty(new Vector3(x, 0, y), flyingBuilding.GetScale())) available = false;


                flyingBuilding.transform.position = new Vector3(x, 0, y);
                flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyinngBuilding(x, y);
                }
            }

        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            { 
                if (grid[placeX + x, placeY + y] != null) return true;
            }
        }
        return false;
    }

    private void PlaceFlyinngBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }

        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }

    public void InputMenu(int value)
    {
        if(value == 0)
        {
            StartPlacinngBuilding(Cubes[0]);
        }
        if (value == 1)
        {
            StartPlacinngBuilding(Cubes[1]);
        }
        if (value == 2)
        {
            StartPlacinngBuilding(Cubes[2]);
        }
    }
    //public void CubeSpawn()
    //{
    //    for (int i = 0; i < 100; i++)
    //    {
    //        int size = (int)Random.Range(1, 4);
    //        //Vector3 pos = gridSpawnn.allPlaneSandGrassPositions[Random.Range(0, gridSpawnn.allPlaneSandGrassPositions.Count + 1)];
    //        Vector3 pos = new Vector3(Random.Range(0, 101), 0, Random.Range(0, 101));
    //        // ContainsList(pos, size) && 
    //        if (!(IsPlaceTaken((int)pos.x, (int)pos.z)))
    //        {
    //            flyingBuilding = Instantiate(Cubes[size-1]);
    //            PlaceFlyinngBuilding((int)pos.x, (int)pos.z);
    //            Instantiate(Cubes[size - 1], pos, transform.rotation);
    //        }

    //    }
    //}

    //private bool ContainsList(Vector3 pos, int size)
    //{
    //    for (int x = 0; x < size; x++)
    //    {
    //        for (int z = 0; z < size; z++)
    //        {
    //            if (gridSpawnn.allPlaneSandGrassPositions.Contains(new Vector3(pos.x + x, pos.y, pos.z + z)) == false)
    //            {
    //                return false;
    //            }
    //        }
    //    }
    //    return true;
    //}

}
