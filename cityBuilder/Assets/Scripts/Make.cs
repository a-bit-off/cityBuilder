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
    private RaycastHit hitDestroy;
    int b = 0;

    public Text textScale;
    public Button buttonDelete;

    private Building building;

    private void Start()
    {
        textScale.gameObject.SetActive(false);
        buttonDelete.gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.gameObject.transform.position, transform.forward * 100f, Color.yellow);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        if (Input.GetMouseButtonDown(0))
        {


            if (flag)
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


            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag == "cube")
                {
                    if (b == 0)
                    {
                        textScale.gameObject.SetActive(false);
                        buttonDelete.gameObject.SetActive(false);
                        b = 1;
                    }
                    else if (b == 1)
                    {
                        textScale.gameObject.SetActive(true);
                        buttonDelete.gameObject.SetActive(true);
                        b = 0;
                    }

                    int scale = (int)hits[i].collider.gameObject.transform.lossyScale.x;
                    hitDestroy = hits[i];
                    textScale.text = "Размер: " + scale + " X " + scale;
                }
            }
        }
    }

    public void MakeFit()
    {
        flag = true;
    }

    public void DestroyButton()
    {
        Destroy(hitDestroy.collider.gameObject.transform.parent.gameObject);
        textScale.gameObject.SetActive(false);
        buttonDelete.gameObject.SetActive(false);
    }
}
