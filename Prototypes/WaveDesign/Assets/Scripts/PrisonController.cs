using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player && player.enemyGameObject != null)
        {
            if (ObjectManager.Instance.Enemies.Remove(player.enemyGameObject))
            {
                Destroy(player.enemyGameObject);
            }          
            player.enemyGameObject = null;
            player.animator.SetBool("RightHandUp", false);
            player.animator.SetBool("LeftHandUp", false);
            player.isChasing = false;
        }
    }
}
