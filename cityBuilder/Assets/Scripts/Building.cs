using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRender;
    public Vector2Int Size = Vector2Int.one;
    private Color mainColor;

    private void Awake()
    {
        mainColor = MainRender.material.color;
    }
    public void SetTransparent(bool available)
    {
        GetScale();
        if (available)
        {
            MainRender.material.color = Color.green;
        }
        else
        {
            MainRender.material.color = Color.red;
        }

    }

    public void SetNormal()
    {
        MainRender.material.color = mainColor;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }

    public int GetScale()
    {
        GameObject cube = gameObject.transform.GetChild(0).gameObject;
        return (int)cube.transform.lossyScale.x;

    }
}