using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_cannon : MonoBehaviour
{
    public Transform firing_point;
    public GameObject toCopy;
    public float speed = 1.0f;
    public float firingFrequency = 1.0f; // In HZ 

    private float lastFiringtTime = 0;
    private float secondsBetweenShots = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        secondsBetweenShots = 1 / firingFrequency;
        if ((Time.realtimeSinceStartup - lastFiringtTime) >= secondsBetweenShots )
        {
            lastFiringtTime = Time.realtimeSinceStartup;
            FireCannon();
        }
    }

    private void FireCannon()
    {
        Vector3 pos = firing_point.position;
        Quaternion rot = firing_point.rotation;
     
        InstantiateIfNotNull(toCopy, pos, rot);
    }

    private void InstantiateIfNotNull(GameObject prefab, Vector3 position, Quaternion rot)
    {
        if (prefab != null)
        {
            GameObject projectile = Instantiate(prefab, position, rot);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 newVel = firing_point.forward;
            float dummy = 0;
            //rot.ToAngleAxis(out dummy, out newVel);
            //Debug.Log(dummy);
            newVel = newVel.normalized * speed;
            rb.velocity = newVel;
        }
        else
        {
            Debug.Log("No GameObject defined to copy");
        }
    }
}
