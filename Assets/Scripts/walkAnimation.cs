using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAnimation : MonoBehaviour
{
    [SerializeField] GameObject Diana = null;
    Animator m_Animator;
    bool isMoving;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

        isMoving = false;
    }

    void Update()
    {
        if (Diana.transform.hasChanged) {
            isMoving = true;
            Diana.transform.hasChanged = false;
        }
        else {
            isMoving = false;
        }

        if (!isMoving) {
            m_Animator.SetBool("isMoving", false);
        }
        if (isMoving) {
            m_Animator.SetBool("isMoving", true);
        }
    }
}
