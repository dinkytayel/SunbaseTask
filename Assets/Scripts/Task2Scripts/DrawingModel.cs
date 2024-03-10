using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrawingModel
{
    event Action<Vector3> OnLinePointAdded;
    event Action CheckIntersactionOnMouseUp;
    void AddLinePoint(Vector3 point);
    Vector3[] GetLinePositions();
    void ClearLine();
    void CheckIntersaction();
}
public class DrawingModel : IDrawingModel
{
    public event Action<Vector3> OnLinePointAdded;
    public event Action CheckIntersactionOnMouseUp;
    private List<Vector3> linePositions = new List<Vector3>();

    public void AddLinePoint(Vector3 point)
    {
        linePositions.Add(point);
        OnLinePointAdded?.Invoke(point);
    }
    public void CheckIntersaction()
    {
        CheckIntersactionOnMouseUp?.Invoke();
    }
    public Vector3[] GetLinePositions()
    {
        return linePositions.ToArray();
    }

    public void ClearLine()
    {
        linePositions.Clear();
    }
}
