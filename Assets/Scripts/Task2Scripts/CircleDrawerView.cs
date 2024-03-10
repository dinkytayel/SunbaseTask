using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawerView : MonoBehaviour
{
    private List<GameObject> circles = new List<GameObject>();
    private IDrawingModel model;
    public float intersectionRadius = 1f;
    public GameObject circlePrefab;

    private void Start()
    {
        model = InputDataContainer.instance.drawingModel;
        SpawnCircle(Random.Range(3, 5));
        model.CheckIntersactionOnMouseUp += CheckIntersectionWithCircles;
    }
    private void CheckIntersectionWithCircles()
    {
        foreach (GameObject circle in circles)
        {
            if (IsLineIntersectingCircle(model.GetLinePositions(), circle.transform.position))
            {
                CircleIntersected(circle);
            }
        }
    }

    private bool IsLineIntersectingCircle(Vector3[] linePositions, Vector2 circleCenter)
    {
        Debug.Log("Circle center = " + circleCenter);
        for (int i = 0; i < linePositions.Length - 1; i++)
        {
            Vector2 lineStart = linePositions[i];
            Vector2 lineEnd = linePositions[i + 1];

            float distance = DistanceFromPointToLine(circleCenter, lineStart, lineEnd);
            Debug.Log("Distance = " + distance + " and radius = " + intersectionRadius);
            if (distance < intersectionRadius)
            {
                return true;
            }
        }

        return false;
    }
    float DistanceFromPointToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        float a = point.x - lineStart.x;
        float b = point.y - lineStart.y;
        float c = lineEnd.x - lineStart.x;
        float d = lineEnd.y - lineStart.y;

        float dot = a * c + b * d;
        float lenSq = c * c + d * d;
        float param = dot / lenSq;

        float xx, yy;

        if (param < 0 || (lineStart.x == lineEnd.x && lineStart.y == lineEnd.y))
        {
            xx = lineStart.x;
            yy = lineStart.y;
        }
        else if (param > 1)
        {
            xx = lineEnd.x;
            yy = lineEnd.y;
        }
        else
        {
            xx = lineStart.x + param * c;
            yy = lineStart.y + param * d;
        }

        float dx = point.x - xx;
        float dy = point.y - yy;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }
        private void CircleIntersected(GameObject circle)
    {
        Debug.Log("Circle Intersected!");
        circles.Remove(circle);
        Destroy(circle);
    }

    public void SpawnCircle(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(0f, Screen.width);
            float y = Random.Range(0f, Screen.height);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f));
            Vector2 spawnPosition = new Vector2(worldPosition.x, worldPosition.y);
            Debug.Log("spawn pos = " + spawnPosition);
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circles.Add(circle);
        }
    }
}
