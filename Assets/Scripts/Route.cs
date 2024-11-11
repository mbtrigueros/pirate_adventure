using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{

    [SerializeField] Point[] points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Point[] GetPoints() {
        return points;
    }

    public Section GetSection() {
        return transform.root.gameObject.GetComponent<Section>();
    }
}
