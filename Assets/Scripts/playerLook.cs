using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerLook : MonoBehaviour
{
    
    public float pickUpDistance = 5f;
    bool interact = false;
    public LayerMask itemLayerMask;
    public EquipItem interaction;


    // Update is called once per frame
    void Update()
    {
        interact = Input.GetKeyDown("e");

        Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpDistance, itemLayerMask);

        foreach (Collider collider in colliders)
        {
            if (interact)
            {
                if (interaction.equipItem(collider.gameObject))
                {
                    print("Picked up " + collider.name);
                }
                else
                {
                    print("Could not pick up " + collider.name);
                }
                interact = false;
            }
        }

        if(interact && colliders.Length == 0)
        {
            interaction.unequipItem();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, pickUpDistance);
    }
}
