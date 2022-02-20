﻿using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    private Vector2Int GridSize = new Vector2Int(100, 100); //размер сетки

    private Building[,] grid; //двумерный массив координат зданий
    private Building flyingBuilding;
    private Camera mainCamera;

    public GridSpawnn gridSpawnn;


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

}
