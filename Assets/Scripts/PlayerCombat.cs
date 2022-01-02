using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject rightHand;
    public Animator anim;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Swing");
            //Debug.Log("swung");

            Weapon weapon = null;

            foreach (Transform child in rightHand.transform)
            {
                weapon = child.GetComponent<Weapon>();
            }

            if(weapon != null)
            {
                Transform transform = Camera.main.GetComponent<Transform>();
                int layerMask = 1 << 11;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, weapon.getRange(), layerMask))
                {

                    Debug.Log("enemy hit");
                    EnemyCombat enemy = (EnemyCombat)hit.transform.gameObject.GetComponent(typeof(EnemyCombat));
                    enemy.TakeDamage(weapon.getDamage());
                }

            }
            

        }
    }
}
