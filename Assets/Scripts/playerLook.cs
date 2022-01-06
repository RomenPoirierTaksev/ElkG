using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class playerLook : MonoBehaviour
{
    
    public float pickUpDistance = 5f;
    bool interact = false;
    public LayerMask itemLayerMask;
    public EquipItem interaction;
    public GameObject itemInteractPanel;
    TextMeshProUGUI itemInteractText;

    private void Awake()
    {
        itemInteractText = itemInteractPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        interact = Input.GetKeyDown("e");

        Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpDistance, itemLayerMask);

        foreach (Collider collider in colliders)
        {
            itemInteractPanel.SetActive(true);
            itemInteractText.text = "Pick up " + collider.name;

            if (interact)
            {
                interact = false;
                if (interaction.equipItem(collider.gameObject))
                {
                    itemInteractText.text = "Picked up " + collider.name;
                }
            }
        }

        if(colliders.Length == 0)
        {
            itemInteractPanel.SetActive(false);
            itemInteractText.text = "";
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
