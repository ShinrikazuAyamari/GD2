using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabObjectScript : MonoBehaviour
{
    public Transform grabDetect;
    public float rayDist;

    int mouseNumb;

    Vector2 mousePosition;

    //  Left hand stuff
    public Transform objectHolderLeft;
    bool holdingLeft;
    GameObject grabbedObjectLeft;

    //  Right hand stuff

    public Transform objectHolderRight;
    bool holdingRight;
    GameObject grabbedObjectRight;

    public float shootStrength;
    public float grabSpeed;

    // Update is called once per frame
    void Update()
    {
        mousePosition = mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            GrabAndRelease(0, objectHolderLeft, ref holdingLeft, ref grabbedObjectLeft);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GrabAndRelease(1, objectHolderRight, ref holdingRight, ref grabbedObjectRight);
        }

        updateObject(ref grabbedObjectLeft, objectHolderLeft);
        updateObject(ref grabbedObjectRight, objectHolderRight);
    }


    private void GrabAndRelease(int handNumb, Transform objectHolder, ref bool holding, ref GameObject grabbedObject)
    {
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        // This code is gonna need:

        //Grabbedobject (left or right)
        //Holding
        //Which input

        if (!holding)
        {
            RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, direction, rayDist);
            if (grabCheck.collider != null && grabCheck.collider.tag == "Object")
            {
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                Rigidbody2D grabRigidbody = grabCheck.collider.gameObject.GetComponent<Rigidbody2D>();
                grabRigidbody.linearVelocity = Vector2.zero; // Sets the grabbed object's velocity to zero so it won't move when grabbed. 
                grabRigidbody.simulated = false;     // Disable simulation so the objects hurt box won't be enabled while walking around with it.
                grabbedObject = grabCheck.collider.gameObject;
                holding = true;
            }

        }
        else
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
            grabbedObject.GetComponent<Rigidbody2D>().linearVelocity = transform.up * shootStrength;
            grabbedObject = null;
            holding = false;
        }
    }

    private void updateObject(ref GameObject grabbedObject, Transform objectHolder)
    {
        if (grabbedObject != null)
        {
            if (Vector2.Distance(grabbedObject.transform.position, objectHolder.transform.position) > 0.1f)
            {
                grabbedObject.transform.position = Vector2.MoveTowards(grabbedObject.transform.position, objectHolder.transform.position, Time.deltaTime * grabSpeed);
            }
            else
            {
                grabbedObject.transform.position = objectHolder.position;
                grabbedObject.transform.rotation = objectHolder.rotation;
                grabbedObject.transform.parent = objectHolder;
            }
        }
    }
}
