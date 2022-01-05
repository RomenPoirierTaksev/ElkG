using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerLook : MonoBehaviour
{
    bool pickedUp = false;
    public float pickUpDistance = 5f;
    public Collider itemCheck;
    GameObject currentItem = null;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && pickedUp)
        {
            GameEvents.instance.itemDrop();
            pickedUp = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        
        print("Pick up " + collision.transform.name);
        if (Input.GetKeyDown("e") && collision.tag.Equals("Equipable"))
        {
            GameEvents.instance.itemPickUp();
            currentItem = collision.gameObject;
            print("Picked up " + collision.transform.name);
            pickedUp = true;
        }
    }

    public GameObject getCurrentItem()
    {
        return currentItem;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, pickUpDistance);
    }
}
