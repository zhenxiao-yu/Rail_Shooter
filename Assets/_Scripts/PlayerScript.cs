using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Camera cam; //Main Camera Ref

    void Start()
    {
        cam = GetComponent<Camera>(); //Get Camera Ref
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot On Left Mouse Click
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //cast ray based on position of mouse

            if (Physics.Raycast(ray, out hit, 50f)) //50f = max distance
            {
                if (hit.collider != null)
                {
                    IHitable hitable = hit.collider.GetComponent<IHitable>();

                    if(hitable != null)
                    {
                        hitable.Hit(hit);
                    }

                    Debug.Log(hit.collider.gameObject.name); //check collision target name
                }
            }
        }
    }
}
