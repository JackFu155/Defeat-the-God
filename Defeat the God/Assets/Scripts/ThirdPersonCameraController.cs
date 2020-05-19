using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    //Variables for controls
    public float RotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    //Variables for camera obstruction
    public Transform Obstruction;
    float zoomSpeed = 2f;

    //Is Called when script is first enabled
    void Start()
    {
        //Set default values
        Obstruction = Target;

        //Locks the cursor to the game window and makes it invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Is Called after physics updates
    void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }

    //Controls the camera movement
    void CamControl()
    {
        //Get mouse inputs
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        //Forces the camera to look at the focus point
        transform.LookAt(Target);

        //Turns the player and camera
        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    //Hides clipping walls/moves the camera to avoid them
    void ViewObstructed()
    {
        //Perform a raycast to determine if there is anything in the way of the camera
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            //If there is something in the way
            if(hit.collider.gameObject.tag != "Player")
            {
                //Disables the renderer without disabling shadows
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                //Moves the camera away from walls
                if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }
            else
            {
                //If obstruction is not the default value
                if (Obstruction.gameObject.tag != "Player")
                {
                    //Reenables the renderer once something stops obstructing the camera
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                    {
                        //Moves the camera back 
                        transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

}
