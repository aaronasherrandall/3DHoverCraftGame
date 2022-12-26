using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    private float lifetime = 4f;

    private void Awake() 
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float speed = 20f;
        bulletRigidBody.velocity = transform.forward * speed;
        StartCoroutine(DestroyAfterLifetime());
    }

    IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
