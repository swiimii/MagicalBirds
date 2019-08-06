using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatResourceController : EnemyResourceController
{
    public GameObject batContainer;

    public override IEnumerator Death()
    {
        transform.SetParent(null);
        Destroy(batContainer);
        return base.Death();
    }



}

