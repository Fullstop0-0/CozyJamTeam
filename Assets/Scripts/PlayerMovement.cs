using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public bool Meat = false;
    public bool Wine = false;
    public bool Tomato = false;
    public bool Son = false;
    public TextMeshProUGUI meatText;
    public TextMeshProUGUI wineText;
    public TextMeshProUGUI tomatoText;
    public TextMeshProUGUI successText;
    public List<string> items;
    public Animator animator;
    public bool door = false;
    public bool dash = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        items = new List<string>();
        meatText.text = $"Meat";
        wineText.text = $"Tomato Juice";
        tomatoText.text = $"Wine";
    }

    public void SetStrikethroughText()
    {
        if (Meat == true)
        {
           meatText.text = $"Meat X";
        }
        if (Tomato == true)
        {
            wineText.text = $"Tomato Juice X";
        }
        if(Wine == true)
        {
            tomatoText.text = $"Wine X";
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

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Wine"))
        {
            Wine = true;
            string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
            Debug.Log("Found" + itemType);
            items.Add(itemType);

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Tomato"))
        {
            Tomato = true;
            string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
            Debug.Log("Found" + itemType);
            items.Add(itemType);

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Son"))
        {
            Son = true;
            string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
            Debug.Log("Found" + itemType);
            items.Add(itemType);
            SonFound();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Door"))
        {
            if (Tomato && Meat && Wine && Son == true)
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movment = new Vector2(moveX, moveY).normalized;

        rb.linearVelocity = movment * moveSpeed;

        animator.SetFloat("speed", Mathf.Abs(moveX));

        if(moveX != 0){
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if (Input.GetButtonDown("Shift"))
        {
            dash = true;
            moveSpeed = 20f;
            animator.SetBool("dash", true);
        }
        else
        {
            dash = false;
        }

        SetStrikethroughText();

        SonFound();
    }
}
