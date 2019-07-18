using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBirdController : MonoBehaviour
{
    public GameObject player;
    public GameObject questBubble;
    public GameObject questBubbleOverlay;
    public float outerDistanceThreshold, innerDistanceThreshold;
    [SerializeField] float minimumQuestFade = .2f;

    protected void Start()
    {
        //Change bubble and (overlay?) to transparent
        var transparent = new Color(1, 1, 1, minimumQuestFade);
        questBubble.GetComponent<SpriteRenderer>().color = transparent;
        if (questBubbleOverlay)
        {
            questBubbleOverlay.GetComponent<SpriteRenderer>().color = transparent;
        }

        // Track player if not assigned in hierarchy
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public void Update()
    {
        // Change bubble transparency according to distance of player from friend (this)
        float distance;
        if((distance = PlayerDistance()) < outerDistanceThreshold)
        {
            FadeQuestNotifier(distance);
        }       
    }

    public float PlayerDistance()
    {
        return Mathf.Abs(player.transform.position.x - transform.position.x);
    }

    public void FadeQuestNotifier(float distance)
    {
        // Set partial transparency to bubble and (overlay?) according to distance
        float magnitude = 1 - (distance - innerDistanceThreshold) / (outerDistanceThreshold - innerDistanceThreshold);
        magnitude = Mathf.Clamp(magnitude, minimumQuestFade, 1);

        var partialTransparency = new Color(1, 1, 1, magnitude);
        questBubble.GetComponent<SpriteRenderer>().color = partialTransparency;
        if (questBubbleOverlay)
        {
            questBubbleOverlay.GetComponent<SpriteRenderer>().color = partialTransparency;
        }
    }

    
}
