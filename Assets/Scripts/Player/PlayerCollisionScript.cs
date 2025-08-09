using TMPro;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    private GameObject playerChatBubble;
    private RectTransform playerChatBubbleRect;
    private TextMeshPro playerChatBubbleText;
    private PlayerScript mainPlayerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        mainPlayerScript = GetComponent<PlayerScript>();
        playerChatBubble = transform.Find("Bubble").gameObject;
        playerChatBubbleRect = playerChatBubble.transform.Find("TextBubble").GetComponent<RectTransform>();
        playerChatBubbleText = playerChatBubble.transform.Find("TextBubble").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        chatBubbleDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Health"))
        {
            mainPlayerScript.playerHeal(10);
            Destroy(collision.gameObject);
        }

        if (collision.collider.CompareTag("Spike"))
        {
            mainPlayerScript.playerDamage(25);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sign"))
        {
            playerChatBubbleText.SetText("E");
            playerChatBubble.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerChatBubble.SetActive(false);
    }

    private void chatBubbleDirection()
    {
        if (mainPlayerScript.playerDirection.x > 0)
            playerChatBubbleRect.localScale = new Vector2(0.2f, 0.2f);
        else if (mainPlayerScript.playerDirection.x < 0)
            playerChatBubbleRect.localScale = new Vector2(-0.2f, 0.2f);
    }
}
