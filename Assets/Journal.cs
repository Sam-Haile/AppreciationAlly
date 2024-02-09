using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public Animator chromoAnim;
    public GameObject chromo;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (chromo.transform.position.y <= -1.75f)
        {
            Rigidbody rb = chromo.GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.mass = 0f;
            chromoAnim.SetTrigger("land");
        }
    }
}
