using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Fall : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            if (!waiting)
            {
                animator.SetTrigger("Fall");
                StartCoroutine(wait());
            }

        }
    }
    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(10.0f);
        animator.SetTrigger("Regen");
        waiting = false;

    }
}
