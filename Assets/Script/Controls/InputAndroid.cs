using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAndroid : IInput {
    private bool clickEnd = true;
    public GameObject Clic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickEnd = false;
            RaycastHit hit;
            //Debug.DrawRay(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), Vector3.forward, Color.red, 10f, false);
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), Vector3.forward, out hit))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    public bool ClicEnded(){
        if (Input.GetMouseButtonUp(0))
            clickEnd = true;
        return clickEnd;
    }
}
