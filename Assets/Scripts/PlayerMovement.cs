using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private bool isFacingRight = true;
    //Item Checker
    public bool Meat = false;
    public bool Tomato = false;
    public bool Son = false;
    public bool door = false;
    // Dashing
    public bool dash = false;
    //private bool canDash = true;
   // private bool isDashing;
    //private float dashSpeed = 10f;
    //private float dashingTime = 0.2f;
    //private float dashingCooldown = 1f;
    // Checklist Text
    public TextMeshProUGUI meatText;
    public TextMeshProUGUI tomatoText;
    public TextMeshProUGUI successText;
    public List<string> items;
    //Animations
    public Animator animator;
    public Music music;
    public BossScript boss;
    public int itemTotal;
    

    [SerializeField] TrailRenderer tr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        items = new List<string>();
        meatText.text = $"Meat";
        tomatoText.text = $"Tomato Juice";
        music.StoreEnter();
        itemTotal = 0;
    }

    public void SetStrikethroughText()
    {
        if (Meat == true)
        {
           meatText.text = $"Meat X";
        }
        if (Tomato == true)
        {
            tomatoText.text = $"Tomato Juice X";
        }
        else
        {
            Debug.Log("Error");
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meat"))
        {
            Meat = true;
            string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
            Debug.Log("Found" + itemType);
            items.Add(itemType);
            itemTotal += 1;

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Tomato"))
        {
            Tomato = true;
            string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
            Debug.Log("Found" + itemType);
            items.Add(itemType);
            itemTotal += 1;

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Son"))
        {
            Son = true;
            string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
            Debug.Log("Found" + itemType);
            items.Add(itemType);
            SonFound();
            itemTotal += 1;

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Door"))
        {
            if (Tomato && Meat && Son == true)
            {
                door = true;
                successText.text = "I bought Everything I needed, and Found my Son.";
                SceneManager.LoadScene("GameWin");
            }
            else
            {
                successText.text = "I have forgotten something in the store.";
                door = false;
            }
        }
        else
        {
            Debug.Log("Why no work?");
        }

    }

    public void SonFound()
    {
        if(Son == true)
        {
            successText.text = "I finally found you my boy. Now lets get you home.";
        }
        else
        {
            Debug.Log("Its not working");
        }
    }


    // Update is called once per frame
    void Update()
    {
       // if ((isDashing)){
           // return;
       // }
    
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movment = new Vector2(moveX, moveY).normalized;

        rb.linearVelocity = movment * moveSpeed;

        animator.SetFloat("speed", Mathf.Abs(moveX));

        if(moveSpeed > 0){
            animator.SetBool("walk", true);
        }
        if(moveSpeed == 0)
        {
            animator.SetBool("walk", false);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("dash", true);
            moveSpeed = 15f;
            //StartCoroutine(Dash());
        }
        if(!Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("dash", false);
            moveSpeed = 5f;
        }

        if(boss.bossAggro == true)
        {
            music.BossAggro();
        } 
        else if (boss.bossAggro == false)
        {
            if (itemTotal == 0)
            {
                music.StoreEnter();
            } else if (itemTotal == 1)
            {
                music.OneItem();
            } else if (itemTotal == 2)
            {
                music.TwoItems();
            } else if (itemTotal == 3)
            {
                music.ThreeItems();
            }
        }

            SetStrikethroughText();
        flip();
        SonFound();
    }

    private void flip()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        if (isFacingRight && moveX <0 || !isFacingRight && moveX > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //private IEnumerator Dash()
    //{
       // canDash = false;
       // isDashing = true;
       // float originalGravity = rb.gravityScale;
       // rb.gravityScale = 0f;
       // rb.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
       // tr.emitting = true;
       // yield return new WaitForSeconds(dashingTime);
       // tr.emitting = false;
       // rb.gravityScale = originalGravity;
       // isDashing  = false;
       // yield return new WaitForSeconds(dashingCooldown);
       // canDash = true;

   // }

}

