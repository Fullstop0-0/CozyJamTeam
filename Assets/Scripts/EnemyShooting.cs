using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public GameObject player;
    public Animator animator;

    private float timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if(distance < 4)
        {
            animator.SetFloat("distance", Mathf.Abs(distance));

            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }

    }

    void shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);

        Vector2 direction = (player.transform.position - bulletPos.position).normalized;

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * 10f;
        }
    }
}
