using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 1f;
    public float distance = 5f;
    public CharacterController enemy;
    public float awarenessDistance = 20f;
    bool playerDetected = false;
    public LayerMask playerLayerMask;
    //RaycastHit hit;
    public List<Node> path;
    public Node startNode;


    void Update()
    {
        playerDetected = Physics.CheckSphere(gameObject.transform.position, awarenessDistance, playerLayerMask);
        if(startNode != null && startNode.next != null)
        {
            Vector3 movementVector = Vector3.right * (startNode.next.gridX - startNode.gridX) + Vector3.forward * (startNode.next.gridY - startNode.gridY);
            Debug.DrawRay(gameObject.transform.position, movementVector, Color.red);
            if (movementVector.magnitude > 1)
            {
                movementVector.Normalize();
            }
            if (movementVector.magnitude >= distance)
            {
                enemy.Move(movementVector * moveSpeed * Time.deltaTime);

            }
        }

        

    }

}
