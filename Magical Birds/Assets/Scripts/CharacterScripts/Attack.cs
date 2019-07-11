using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    float hitboxDuration = 0;
    float spriteDuration = .25f;
    // Start is called before the first frame update
    protected void Start()
    {
        StartCoroutine("AttackFade");
    }

    protected IEnumerator AttackFade()
    {
        yield return new WaitForSeconds(hitboxDuration);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(spriteDuration - hitboxDuration);
        Destroy(gameObject);

    }
}
