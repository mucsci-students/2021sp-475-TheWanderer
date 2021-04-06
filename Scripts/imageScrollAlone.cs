using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageScrollAlone : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    private Transform cameraTransform;
    private Vector3 lastPosition;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition = cameraTransform.position;
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        if(Mathf.Abs(transform.position.x - lastPosition.x) >= 200)
        {
            
            transform.position = new Vector3(cameraTransform.position.x+100, transform.position.y, 0);
        }
    }
}
