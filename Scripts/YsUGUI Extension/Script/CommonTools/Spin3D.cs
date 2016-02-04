/*
 * Author:ImL1s
 * Email:ImL1s@outlook.com
 * Summary:
 */

using UnityEngine;
using System.Collections;

public class Spin3D : MonoBehaviour
{

    private Vector3 previousV3 = default(Vector3);
    private Vector3 orginal;

    void OnMouseDrag()
    {
        if (Input.mousePosition.x > orginal.x + 5f)
        {
            orginal = Input.mousePosition;
            transform.Rotate(Vector3.up * -5);
        }
        else if(Input.mousePosition.x < orginal.x -5f)
        {
            orginal = Input.mousePosition;
            transform.Rotate(Vector3.up * 5);
        }
    }

    void OnMouseUp()
    {
        previousV3 = new Vector3(Screen.height / 2, Screen.width / 2); 
    }

    void OnMouseDown()
    {
        orginal = Input.mousePosition;
    }
}
