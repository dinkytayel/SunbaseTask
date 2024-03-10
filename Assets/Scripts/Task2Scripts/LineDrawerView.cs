using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawerView : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Start()
    {
        InputDataContainer.instance.drawingModel.OnLinePointAdded += DrawLinePoint;
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void DrawLinePoint(Vector3 point)
    {
       Vector3[] linePositions = InputDataContainer.instance.drawingModel.GetLinePositions();
        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
    }

    public void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }
}
