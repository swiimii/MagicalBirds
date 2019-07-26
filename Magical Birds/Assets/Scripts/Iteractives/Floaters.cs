using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaters : MonoBehaviour
{
    public Vector3 origin;
    protected virtual void Start() {
        origin = transform.position;
    }
    private void Update() {
        transform.position = origin + (Mathf.Sin(Time.fixedTime * 3) * Vector3.up)/25;
    }
}
