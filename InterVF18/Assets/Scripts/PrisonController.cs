using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonController : MonoBehaviour
{
    private bool hasDocuments;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player && player.enemyGameObject != null)
        {
            StateController enemyController = player.enemyGameObject.gameObject.GetComponent<StateController>();

            if (enemyController && enemyController.hasDocuments)
            {
                hasDocuments = true;
            }

            if (ObjectManager.Instance.Enemies.Remove(player.enemyGameObject))
            {
                Destroy(player.enemyGameObject);
            }          
            player.enemyGameObject = null;
            player.animator.SetBool("RightHandUp", false);
            player.animator.SetBool("LeftHandUp", false);
            player.isChasing = false;

            if (hasDocuments)
            {
                player.hasDocuments = true;
                player.documents = Instantiate(VisibilityManager.Instance.playerDocuments, player.gameObject.transform);
                player.documents.GetComponent<Collider>().enabled = false;
                player.animator.SetBool("RightHandUp", true);
            }
        }
    }
}
