using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator m_anim;
    private Rigidbody m_rb;
    public Joystick joystick;
    public float moveSpeed = 5f;

    public float atkRate;

    public float doubleTapThreshold = 0.3f;

    public int currentHealth; 
    private int maxHealth = 100;

    public bool isDead = false;


    private void Awake()
    {   
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }
    void Start()
    {
    }

    void Update()
    {
        if (isDead)
        {
            // Dừng hẳn player khi chết
            if (m_rb != null)
                m_rb.velocity = Vector3.zero;
            return;
        }

        if (joystick == null || m_rb == null) return;

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

    public void PunchRight()
    {
        m_anim.SetTrigger(Const.ATTACK1_ANIM);
    }

    public void PunchCombo()
    {
        StopAllCoroutines();
        StartCoroutine(DoDoublePunch());
    }

    private IEnumerator DoDoublePunch()
    {
        yield return new WaitForSeconds(0.05f);
        m_anim.SetTrigger(Const.ATTACK_COMBO_ANIM);
    }

    public void TakeDamage(int damage, bool isCombo)
    {
        currentHealth -= damage;

        if (isCombo)
            m_anim.SetTrigger("HeadHit");    // Đòn combo → HeadHit
        else
            m_anim.SetTrigger("StomachHit"); // Đòn thường → StomachHit
        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("player Die");
            m_anim.SetTrigger("Knockout");
            // Destroy(gameObject); // Xóa object nếu cần

            GameObject enemyObj = GameObject.FindWithTag("Enemy");
            if (enemyObj != null)
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.CheckDeadAndVictory(); // Gọi hàm riêng
                }
            }
        }
    }

    public void PlayVictory()
    {
        if (isDead) return; // Nếu player chết rồi thì không làm gì

        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Victory");
        }
    }
}
