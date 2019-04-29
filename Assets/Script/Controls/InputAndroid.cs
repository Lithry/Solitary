using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAndroid : IInput {
    private bool rClickEnd = true;
    private bool lClickEnd = true;

    public GameObject RightClic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rClickEnd = false;
            RaycastHit hit;
            //Debug.DrawRay(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), Vector3.forward, Color.red, 10f, false);
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), Vector3.forward, out hit))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    public bool RightClicEnded(){
        if (Input.GetMouseButtonUp(0))
            rClickEnd = true;
        return rClickEnd;
    }

    public GameObject LeftClic()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lClickEnd = false;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), Vector3.forward, out hit))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    public bool LeftClicEnded()
    {
        if (Input.GetMouseButtonUp(1))
            lClickEnd = true;
        return lClickEnd;
    }
}
