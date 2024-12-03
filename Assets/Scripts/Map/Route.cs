using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{

    [SerializeField] Point[] points;
    Point[] oldPoints;

    private void Start() {
        SetOriginalPoints();
    }

    public void SetOriginalPoints() {
        oldPoints = points;
    }

    public void RestartPoints() {
        points = oldPoints;
    }

    public Point[] GetPoints() {
        return points;
    }

    public void SetPoints(Point[] newPoints) {
        points = newPoints;
    }
}
