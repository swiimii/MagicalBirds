using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBirdController : MonoBehaviour
{
    public GameObject player;
    public GameObject questPrefab;
    public float outerDistanceThreshold, innerDistanceThreshold;
    private readonly float minimumQuestFade = .2f;

    protected void Start()
    {
        var color = questPrefab.GetComponent<SpriteRenderer>().color;
        questPrefab.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);

        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public void Update()
    {
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
        float magnitude = (distance - innerDistanceThreshold) / (outerDistanceThreshold - innerDistanceThreshold);
        var color = questPrefab.GetComponent<SpriteRenderer>().color;
        questPrefab.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1 - magnitude);
    }

    
}
