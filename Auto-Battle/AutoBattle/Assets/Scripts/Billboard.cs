using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    public Transform anchor;
    private Vector3 offset = new Vector3(0, 0, 2);

    private void Update()
    {
        transform.position = anchor.position + offset;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
