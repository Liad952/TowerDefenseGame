using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 30f;
    public float panBorder = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    public Vector3 forward;
    public Vector3 back;
    public Vector3 right;
    public Vector3 left;

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.GameOver)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;
        
        if(!doMovement)
        {
            return;
        }

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder)
        {
            transform.Translate(forward* panSpeed*Time.deltaTime,Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorder)
        {
            transform.Translate(back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder)
        {
            transform.Translate(right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorder)
        {
            transform.Translate(left * panSpeed * Time.deltaTime, Space.World);
        }


        if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            Vector3 pos = transform.position;

            pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;

        }       

    }

}
