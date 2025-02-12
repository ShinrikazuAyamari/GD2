using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabObjectScript : MonoBehaviour
{
    public Transform grabDetect;
    public float rayDist;

    Vector2 mousePosition;

    //  Left hand stuff
    public Transform objectHolderLeft;
    bool holdingLeft;
    GameObject grabbedObjectLeft;
    float leftMass;

    //  Right hand stuff
    public Transform objectHolderRight;
    bool holdingRight;
    GameObject grabbedObjectRight;
    float rightMass;

    public float shootStrength;
    public float grabSpeed;

    // Update is called once per frame
    void Update()
    {
        mousePosition = mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            GrabAndRelease(-1, objectHolderLeft, ref holdingLeft, ref grabbedObjectLeft, ref leftMass);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GrabAndRelease(1, objectHolderRight, ref holdingRight, ref grabbedObjectRight, ref rightMass);
        }
    }

    private void FixedUpdate()
    {
        updateObject(ref grabbedObjectLeft, objectHolderLeft);
        updateObject(ref grabbedObjectRight, objectHolderRight);   
    }


    private void GrabAndRelease(int handNumb, Transform objectHolder, ref bool holding, ref GameObject grabbedObject, ref float mass)
    {
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (!holding)
        {
            RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, direction, rayDist);
            if (grabCheck.collider != null && grabCheck.collider.tag == "Object")
            {
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Rigidbody2D grabRigidbody = grabCheck.collider.gameObject.GetComponent<Rigidbody2D>();

                grabRigidbody.linearVelocity = Vector2.zero; // Sets the grabbed object's velocity to zero so it won't move when grabbed. 
                grabbedObject = grabCheck.collider.gameObject;
                holding = true;

                float objectMass = grabRigidbody.mass;
                Vector2 objectOffset;
                objectOffset.x = handNumb/1.5f + (objectMass / 2 * handNumb);
                objectOffset.y = 0.9f + objectMass / 2;
                objectHolder.transform.localPosition = objectOffset;

                mass = grabRigidbody.mass;
                grabRigidbody.mass = 0.1f;

            }

        }
        else
        {
            grabbedObject.GetComponent<Rigidbody2D>().mass = mass;
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
            Rigidbody2D grabbedRG = grabbedObject.GetComponent<Rigidbody2D>();

            Vector2 targetPos = objectHolder.position;
            grabbedRG.MovePosition(Vector2.MoveTowards(grabbedRG.position, targetPos, Time.deltaTime * grabSpeed));
            grabbedObject.transform.rotation = objectHolder.rotation;
        }
    }
}
