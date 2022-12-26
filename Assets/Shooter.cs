using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Camera cam = null;

    MouseCamera mouseCamera;

    private Vector3 mousePosition;
    private Vector3 direction;
    public Transform bulletPrefab;
    public Transform spawnBulletPosition;
    [SerializeField] private float bulletSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        mouseCamera = FindObjectOfType<MouseCamera>();
        cam = Camera.main;

    }

    [SerializeField] float range = 50f;

    public void Shoot()
    {

        Vector2 currentMousePosition = (mouseCamera.AnchorCursorToRead(mouseCamera.newPosition));
        Vector3 directionVector = (new Vector3(currentMousePosition.x, currentMousePosition.y, 0f) - spawnBulletPosition.transform.position);
        Vector3 aimDirection = spawnBulletPosition.transform.forward;
        Vector3 combinedDirectionVector = (directionVector + aimDirection).normalized;
        Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.LookRotation(combinedDirectionVector, Vector3.up));
        //bulletClone.transform.LookAt()

    }


    Vector3 pos;

    public void Shoot2()
    {
        Ray ray = cam.ScreenPointToRay(pos);
        print(ray);

    }

    public void Shoot3()
    {
        Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.identity);

    }

    private void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print(mousePosition);
        direction = mousePosition - bulletPrefab.transform.position;

    }

    void FixedUpdate()
    {

    }

    public void FireBullet()
    {
        Debug.Log("Fire Pressed");
        //Vector3 aimDir = (mouseCamera.currentPosition - spawnBulletPosition.position).normalized;
        //Vector3 currentMousePosition = new Vector3(mouseCamera.currentPosition.x, mousePosition.currentPosition.y, )
        //Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.forward));
        //print("aimDir" + aimDir);
        //print("currentPosition" + mouseCamera.currentPosition);
        
        //GameObject bulletClone = Instantiate(bulletPrefab, bulletPrefab.transform.position, Quaternion.identity);
        //bulletClone.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed; 
    }


}
