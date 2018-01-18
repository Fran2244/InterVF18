using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAddToCollection : MonoBehaviour {
    enum ObjectType {Enemy, Detector, Guard };

    [SerializeField]
    ObjectType type;

    // Use this for initialization
    void Start () {
        switch (type)
        {
            case ObjectType.Enemy:
                ObjectManager.Instance.AddEnemy(gameObject);
                break;
            case ObjectType.Detector:
                ObjectManager.Instance.AddDetector(gameObject);
                break;
            case ObjectType.Guard:
                ObjectManager.Instance.AddGuard(gameObject);
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        switch (type)
        {
            case ObjectType.Detector:
                ObjectManager.Instance.RemoveDetector(gameObject);
                break;
            case ObjectType.Guard:
                ObjectManager.Instance.RemoveGuard(gameObject);
                break;
            default:
                break;
        }
    }
}
