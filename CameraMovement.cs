using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float minFrontPosition = -10;
    [SerializeField]
    float maxFrontPosition = 10;
    [SerializeField]
    float minSidePosition = -10;
    [SerializeField]
    float maxSidePosition = 10;

    [SerializeField]
    float speed = 10f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = transform.rotation;
        Camera.main.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, 0, v);

        Vector3 currentPosition = transform.position + direction;
        currentPosition.z = Mathf.Clamp(currentPosition.z,minFrontPosition,maxFrontPosition);
        currentPosition.x = Mathf.Clamp(currentPosition.x,minSidePosition,maxSidePosition);

        transform.position = Vector3.Lerp(transform.position, currentPosition, speed*Time.deltaTime);

        
    }
}
