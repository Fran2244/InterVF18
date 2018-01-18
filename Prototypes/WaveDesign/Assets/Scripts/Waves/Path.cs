using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
    [SerializeField]
    Transform[] path;

    public Transform[] GetPath()
    {
        return path;
    }
}
