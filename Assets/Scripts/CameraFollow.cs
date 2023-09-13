using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    private float dx;
    private float dy;
    private float dz;


    // Start is called before the first frame update
    void Start()
    {
        dx = transform.position.x - target.position.x;
        dy = transform.position.y - target.position.y;
        dz = transform.position.z - target.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + new Vector3 (dx, dy, dz);
    }
}
