using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator m_anim;
    private Rigidbody m_rb;
    private bool m_isAttacked;
    public float atkRate;
    private float m_curAtkRate;


    private void Awake()
    {   
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();
        m_curAtkRate = atkRate;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !m_isAttacked)
        {
            m_anim.SetBool(Const.ATTACK1_ANIM,true);
            m_isAttacked = true;
        }

        if(m_isAttacked)
        {
            m_curAtkRate -= Time.deltaTime;
            if (m_curAtkRate <= 0)
            {
                m_isAttacked = false;
                m_curAtkRate = atkRate;
            }
        }

    }
    public void ResetAtkAnim()
    {
        m_anim.SetBool(Const.ATTACK1_ANIM, false);
    }

}
