using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private Animator m_anim;
    private bool isAttacking = false;

    public Transform player;         
    public float moveSpeed = 2f;     // Tốc độ di chuyển
    public float attackRange = 0.8f; // Khoảng cách để tấn công
    public float attackDelay = 1.2f; // Độ trễ giữa các đòn tấn công

    public bool isDead = false;

    public EnemyHealthBar healthBar;

    public float speed;
    public int power;

    private Dialog _dialog;
    public void SetStats(int health, float speed, int power)
    {
        this.maxHealth = health;
        this.currentHealth = health;
        this.speed = speed;
        this.power = power;
    }
    void Start()
    {
        _dialog = FindObjectOfType<Dialog>();
        m_anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        m_anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (isDead) return;

        if (player == null) return;

        if (CheckDeadAndVictory())
        {
            return;
        }

        // Tính khoảng cách
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveToPlayer();
        }
        else
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
            m_anim.SetBool("Walking", false);
        }
    }
    public bool CheckDeadAndVictory()
    {
        if (isDead)
        {
            m_anim.SetBool("Walking", false);
            return true;
        }

        if (player != null)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null && playerScript.isDead)
            {
                m_anim.SetBool("Walking", false);
                return true;
            }
        }
        return false;
    }
    void MoveToPlayer()
    {

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * moveSpeed * Time.deltaTime;


        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
        }
        m_anim.SetBool("Walking", true);
    }

    IEnumerator AttackRoutine()
    {
        if (isDead) yield break;

        isAttacking = true;

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
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth);

        if (isCombo)
            m_anim.SetTrigger("HeadHit");
        else
            m_anim.SetTrigger("StomachHit");

        // Kiểm tra chết
        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Enemy died!");
            m_anim.SetTrigger("Knockout");
            m_anim.SetBool("Walking", false);
            _dialog.ShowWin();
            Destroy(gameObject, 1f);
        }
    }
}

