 using UnityEngine;
 
 public class BulletProjectile2 : MonoBehaviour
 {
     public float Bulletspeed = 10f;
     public Rigidbody Bullet;
 
     private Vector3 _moveposition;
 
     void Start()
     {
         Debug.Log("bullet spawned");
         Bullet = GetComponent<Rigidbody>();
 
         _moveposition = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
         //_moveposition.z = 0;
         _moveposition.Normalize();
     }
 
     private void Update()
     {
         var step = Bulletspeed * Time.deltaTime;
         Bullet.velocity = Vector3.MoveTowards(transform.position, _moveposition, step);
     }
 }
