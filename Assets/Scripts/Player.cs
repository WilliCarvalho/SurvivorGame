using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveX = Input.GetAxis("Horizontal") * velocity * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * velocity * Time.deltaTime;

        if (moveX != 0 || moveZ != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else if(moveX == 0 && moveZ == 0)
        {
            animator.SetBool("isWalking", false);
        }
        transform.Translate(moveX, 0, moveZ);
    }
}
