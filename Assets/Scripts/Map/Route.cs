using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{

    [SerializeField] Point[] points;

    public Point[] GetPoints() {
        return points;
    }
}
