using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEnemyScript : MonoBehaviour
{
    //Will function as both a class for movement and abilities (since they'll be working closely together for this enemy)
    public bool isIdle = true, isRight = true;
    public int rotationIndex;
    public GameObject rangedAttackPrefab;
    public Vector3 rangedAttackOffset;
    public float dashSpeed = 2f;

    public float idleDuration = 1f;

    private float idleTime;


    // These contain two empties - [0] is lower position, [1] is upper position
    public GameObject[] leftMovementLocks;
    public GameObject[] rightMovementLocks;



    public void Update()
    {
       
        if(isIdle)
        {
            idleTime += Time.deltaTime;
            if (rotationIndex == 0 && idleTime > idleDuration)
            {
                rotationIndex = (rotationIndex + 1) % 3;
                idleTime = 0;
                StartCoroutine("MeleeAttack");
            }
            else if (rotationIndex == 1 && idleTime > idleDuration)
            {
                rotationIndex = (rotationIndex + 1) % 3;
                idleTime = 0;
                StartCoroutine("RangedAttack");
            }
            else if(rotationIndex == 2 && idleTime > idleDuration / 2)
            {
                rotationIndex = (rotationIndex + 1) % 3;
                idleTime = 0;
                // Do nothing. This is an idle frame.
            }

        }
    }    
    
    public int GetDirection()
    {
        return (int)(transform.localScale.x / Mathf.Abs(transform.localScale.x));
    }
    public void SetDirection(int direction)
    {
        int actualDirection = Mathf.Abs(direction) / direction;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * actualDirection,
                                            transform.localScale.y,
                                            transform.localScale.z);
    }


    public IEnumerator MeleeAttack()
    {
        isIdle = false;
        GetComponent<Animator>().SetBool("isMeleeAttack", true);
        yield return new WaitForSeconds(.8f);
        Vector3 distance;
        int direction;
        if(isRight)
        {
            distance = leftMovementLocks[0].transform.position;
            isRight = false;
            direction = 1;
        }
        else
        {
            distance = rightMovementLocks[0].transform.position;
            isRight = true;
            direction = -1;
        }

        while(Mathf.Abs(transform.position.x - distance.x) > .2f)
        {
            transform.position = Vector3.Lerp(transform.position, distance, dashSpeed * Time.deltaTime);
            yield return null;
        }
        
        yield return null;
        GetComponent<Animator>().SetBool("isMeleeAttack", false);
        SetDirection(direction);
        isIdle = true;
    }

    public IEnumerator RangedAttack()
    {
        isIdle = false;
        GetComponent<Animator>().SetBool("isRangedAttack", true);
        yield return new WaitForSeconds(.8f);
        Instantiate(rangedAttackPrefab, transform.position + rangedAttackOffset.x * GetDirection() * Vector3.right + rangedAttackOffset.y * Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        GetComponent<Animator>().SetBool("isRangedAttack", false);
        isIdle = true;

    }

}
