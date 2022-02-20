using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Make : MonoBehaviour
{
    public GridSpawnn gridSpawn;
    public GameObject[] Plane;
    private Vector3 MakePosition;
    private bool flag = false;
    private bool flagInfo = false;

    public Text textScale;
    public Button buttonDelete;

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.gameObject.transform.position, transform.forward * 100f, Color.yellow);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        if (Input.GetMouseButtonDown(0) && flag)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                MakePosition = hits[i].collider.transform.position;
                if (hits[i].collider.gameObject.tag == "water")
                {
                    Destroy(hits[i].collider.gameObject);
                    Instantiate(Plane[0], MakePosition, transform.rotation);
                    gridSpawn.allPlaneWaterPositions.Remove(MakePosition);
                    gridSpawn.allPlaneSwampPositions.Add(MakePosition);

                }
                else if (hits[i].collider.gameObject.tag == "swamp")
                {
                    Destroy(hits[i].collider.gameObject);
                    Instantiate(Plane[1], MakePosition, transform.rotation);
                    gridSpawn.allPlaneSwampPositions.Remove(MakePosition);
                    gridSpawn.allPlaneSandGrassPositions.Add(MakePosition);
                }
            }
            flag = false;
        }

        if (Input.GetMouseButtonDown(0) && flagInfo)
        {
            flagInfo = false;
        }
    }

    public void MakeFit()
    {
        flag = true;
    }
    public void GetInformation()
    {
        flagInfo = true;
    }
}
