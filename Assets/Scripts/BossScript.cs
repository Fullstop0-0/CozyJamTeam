using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BossScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject player;
    private Transform playerTransform;
    private Rigidbody2D rb;
    public float detectionDistance = 4f;
    public float force;
    public Animator animator;
    public bool bossAggro = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance < detectionDistance)
        {
            moveToPlayer();
            animator.SetFloat("distance", Mathf.Abs(distance));
            bossAggro = true;
        } else
        {
            bossAggro = false;
        }
        
    }

    public void moveToPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;


        animator.SetFloat("speed", Mathf.Abs(moveSpeed));

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        
            other.GetComponent<PlayerHealth>().health -= 20;
            other.GetComponent<PlayerHealth>().UpdateHealthBar();
        }
      
    }

}
