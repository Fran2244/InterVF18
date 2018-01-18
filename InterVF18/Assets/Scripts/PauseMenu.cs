using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isPaused = false;
    }

    bool isPaused;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
            anim.SetBool("isShown", isPaused);
            Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        }
    }

}
