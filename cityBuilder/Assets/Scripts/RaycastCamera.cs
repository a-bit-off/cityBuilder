using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaycastCamera : MonoBehaviour
{
    public Toggle toggle;
    public Vector3 CameraPosition = new Vector3(0, 60, 0);

    public bool ActiveCameraMove = false;
    private void LateUpdate()
    {
        if(ActiveCameraMove)
            transform.position = Vector3.Lerp(transform.position, CameraPosition, 1f * Time.deltaTime);
    }
    private void Update()
    {
        CheckPlane();
    }
    public void SetActiveMove()
    {
        if (toggle.isOn)
            ActiveCameraMove = true;
        else
            ActiveCameraMove = false;
    }


    public void CheckPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.gameObject.transform.position, transform.forward * 100f, Color.yellow);
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag == "water" || hits[i].collider.gameObject.tag == "swamp" || hits[i].collider.gameObject.tag == "sand_grass")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CameraPosition = hits[i].collider.gameObject.transform.position;
                    CameraPosition.y = 60;
                }
            }
        }
    }
}

