using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDocument : MonoBehaviour {

    [SerializeField]
    Transform docHoldingPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Document")
        {
            StateController controller = GetComponent<StateController>();
            GameObject docCopy = Instantiate(other.gameObject);
            controller.hasDocuments = true;
            docCopy.transform.parent = docHoldingPoint;
            controller.documents = docCopy;
            docCopy.transform.localPosition = Vector3.zero;
            docCopy.GetComponent<Collider>().enabled = false;
            docCopy.transform.localScale = new Vector3(20f, 20f, 20f);
        }
    }
}
