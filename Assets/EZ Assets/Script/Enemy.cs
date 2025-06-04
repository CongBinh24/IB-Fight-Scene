using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    private Animator m_anim;
    private bool isAttacking = false;

    public Transform player;         // Kéo thả player vào Inspector
    public float moveSpeed = 2f;     // Tốc độ di chuyển
    public float attackRange = 0.8f;   // Khoảng cách để tấn công
    public float attackDelay = 1.2f; // Độ trễ giữa các đòn tấn công

    private bool isDead = false;


    void Start()
    {
        m_anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        if (player == null) return;

        if (CheckDeadAndVictory())
        {
            // Nếu player chết hoặc enemy chết thì không làm gì nữa
            return;
        }

        // Tính khoảng cách
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // Di chuyển về phía player
            MoveToPlayer();
        }
        else
        {
            // Gần đủ để tấn công
            if (!isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }

            // Dừng animation đi bộ
            m_anim.SetBool("Walking", false);
        }
    }
    public bool CheckDeadAndVictory()
    {
        if (isDead)
        {
            m_anim.SetBool("Walking", false);
            return true; // không làm gì nữa
        }

        if (player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null && playerScript.isDead)
            {
                m_anim.SetBool("Walking", false);
                m_anim.SetTrigger("Victory");
                return true; // không di chuyển / tấn công nữa
            }
        }
        return false;
    }
    void MoveToPlayer()
    {
        // Hướng di chuyển
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        // Di chuyển
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Quay mặt về player
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
        }

        // Bật animation "walk"
        m_anim.SetBool("Walking", true);
    }

    IEnumerator AttackRoutine()
    {
        if (isDead) yield break;

        isAttacking = true;

        // Chọn đòn tấn công ngẫu nhiên
        bool doCombo = Random.value > 0.5f;
        if (doCombo)
            PunchCombo();
        else
            PunchRight();

        yield return new WaitForSeconds(attackDelay - 0.3f);

        isAttacking = false;
    }

    private void PunchRight()
    {
        m_anim.SetTrigger("Attacking 1");
    }

    private void PunchCombo()
    {
        m_anim.SetTrigger("Attacking 2");
    }

    public void TakeDamage(int damage, bool isCombo)
    {
        currentHealth -= damage;

        if (isCombo)
            m_anim.SetTrigger("HeadHit");
        else
            m_anim.SetTrigger("StomachHit");

        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Enemy Die");
            m_anim.SetTrigger("Knockout"); 
            m_anim.SetBool("Walking", false);
            // Destroy(gameObject); // Xóa object nếu cần

            if (player != null)
            {
                Player playerScript = player.GetComponent<Player>();
                if (playerScript != null)
                {
                    playerScript.PlayVictory();
                }
            }
        }
    }



}

