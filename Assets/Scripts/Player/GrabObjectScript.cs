using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabObjectScript : MonoBehaviour
{
    public Transform grabDetect;
    public Transform objectHolder;
    public float rayDist;
    bool holding;

    Vector2 mousePosition;

    GameObject grabbedObject;
    public float shootStrength;

    public float grabSpeed;

    // Update is called once per frame
    void Update()
    {
        mousePosition = mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (Input.GetMouseButtonDown(0))
        {
            if(!holding)
            {
                RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, direction, rayDist);
                if (grabCheck.collider != null && grabCheck.collider.tag == "Object")
                {
                    //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Sets the grabbed object's velocity to zero so it won't move when grabbed. 
                    grabbedObject = grabCheck.collider.gameObject;
                    holding = true;
                }

            }
            else
            {
                grabbedObject.transform.parent = null;
                //grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                grabbedObject.GetComponent<Rigidbody2D>().linearVelocity = transform.up * shootStrength;
                grabbedObject = null;
                holding = false;
            }
        }

        if (grabbedObject != null)
        {
            if (Vector2.Distance(grabbedObject.transform.position, objectHolder.transform.position) > 0.1f)
            {
                grabbedObject.transform.position = Vector2.MoveTowards(grabbedObject.transform.position, objectHolder.transform.position, Time.deltaTime * grabSpeed);
            }
            else
            {
                grabbedObject.transform.position = objectHolder.position;
                grabbedObject.transform.parent = objectHolder;
            }
        }
    }
}
