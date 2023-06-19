using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    public Animator anim;
    private bool isOpened = false;

    private void Start()
    {
        anim.SetBool("isOpened", false);
    }

    private void Update()
    {
        StartCoroutine(AnimationPlay());
    }

    public IEnumerator AnimationPlay()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!isOpened)
            {
                anim.SetBool("isOpened", true);
                isOpened = true;
                anim.Play("open");
            } 
            else 
            {
                anim.SetBool("isOpened", false);
                isOpened = false;
                anim.Play("close");
            }
            yield return new WaitForSeconds(0.3f);
        } 
    }
}
