using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator m_anim;
    private Rigidbody m_rb;
    public Joystick joystick;
    public float moveSpeed = 5f;

    private bool m_isAttacked;
    public float atkRate;
    private float m_curAtkRate;


    private float m_lastTapTime = 0f;
    public float doubleTapThreshold = 0.3f;
    private void Awake()
    {   
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();
        m_curAtkRate = atkRate;
    }
    void Start()
    {
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude > 0.1f)
        {
            direction = direction.normalized;

            // Di chuyển player
            Vector3 moveVelocity = direction * moveSpeed;
            m_rb.velocity = new Vector3(moveVelocity.x, m_rb.velocity.y, moveVelocity.z);

            // Quay player hướng di chuyển
            transform.forward = direction;
            m_anim.SetBool(Const.WALK_ANIM, true);
        }
        else
        {
            // Dừng player
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
            m_rb.angularVelocity = Vector3.zero;
            // Tắt animation đi bộ
            m_anim.SetBool(Const.WALK_ANIM, false);
        }
    }

    public void Punch()
    {
        float timeSinceLastTap = Time.time - m_lastTapTime;

        if (timeSinceLastTap <= doubleTapThreshold)
        {
            StopAllCoroutines();
            StartCoroutine(DoDoublePunch());
            m_lastTapTime = 0f;
        }
        else
        {
            AttackRight();
            m_lastTapTime = Time.time;
        }
    }

    private IEnumerator DoDoublePunch()
    {
        m_anim.SetTrigger(Const.ATTACK1_ANIM);
        yield return new WaitForSeconds(0.05f); // delay 1 chút
        m_anim.SetTrigger(Const.ATTACK_COMBO_ANIM);
    }

    private void AttackRight()
    {
        m_anim.SetTrigger(Const.ATTACK1_ANIM);
    }

    //private void AttackLeft()
    //{
    //    m_anim.SetTrigger(Const.ATTACK_COMBO_ANIM);
    //}
}
