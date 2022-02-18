using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10); //размер сетки

    private Building[,] grid; //двумерный массив координат зданий
    private Building flyingBuilding;
    private Camera mainCamera;


    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
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

                flyingBuilding.transform.position = new Vector3(x, 0, y);
                flyingBuilding.SetTransparent(available);


                if (available && Input.GetMouseButtonDown(0))
                {
                    flyingBuilding.SetNormal();
                    flyingBuilding = null;
                }
            }

        }
    }
}
