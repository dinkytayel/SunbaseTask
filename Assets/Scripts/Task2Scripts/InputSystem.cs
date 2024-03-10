using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InputSystem : MonoBehaviour
{
    // Start is called before the first frame update
    InputDataContainer inputDataContainer;
    Vector3 currentPos;
    Vector3 prevPos;
    Vector3 mousePos;
    float distance;
    float minDis = 0.1f;
    void Awake()
    {
        inputDataContainer = new InputDataContainer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           InputDataContainer.instance.drawingModel.ClearLine();
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                mousePos = Input.mousePosition;
                mousePos.z = 10;

                currentPos = Camera.main.ScreenToWorldPoint(mousePos);
                distance = Vector3.Distance(currentPos, prevPos);
                if (distance >= minDis)
                {
                    prevPos = currentPos;
                    InputDataContainer.instance.drawingModel.AddLinePoint(currentPos);                 
                }

               // InputDataContainer.instance.drawingModel.OnLinePointAdded?.Invoke(Input.mousePosition);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            InputDataContainer.instance.drawingModel.CheckIntersaction();
        }
    }  
}
