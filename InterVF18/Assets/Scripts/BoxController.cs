using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private bool hasDocuments;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player && player.documents != null)
        {
            Destroy(player.documents);

            player.documents = null;
            player.animator.SetBool("RightHandUp", false);
            player.hasDocuments = false;

            if (!hasDocuments)
            {
                GameObject documents = Instantiate(VisibilityManager.Instance.originalDocuments, gameObject.transform);
                documents.transform.localPosition = new Vector3(0.0222f, 0.0f, 0.02f);
                documents.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                hasDocuments = true;
            }
        }
    }
}
