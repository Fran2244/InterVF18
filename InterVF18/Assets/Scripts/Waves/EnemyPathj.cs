using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathj : MonoBehaviour {
    [SerializeField]
    Transform[] path;

    public Transform[] GetPath()
    {
        return path;
    }
}
