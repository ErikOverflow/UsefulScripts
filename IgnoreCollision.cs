using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private Collider2D ignoredCollider;
    [SerializeField]
    private Collider2D myCollider;


    private void Start()
    {
        Physics2D.IgnoreCollision(myCollider, ignoredCollider);
    }
}
