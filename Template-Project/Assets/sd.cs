using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sd : MonoBehaviour
{
    // Prefabs to spawn when this object breaks
    public GameObject piece1;
    public GameObject piece2;
    public GameObject piece3;
    public GameObject piece4;
    public float offset = 0.5f; // tweak as needed
    // Tags used to identify the tree stump and the axe. Set these in the Inspector
    public string stumpTag = "TreeStump";
    public string axeTag = "Axe";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // NOTE / Assumption:
    // The requirement said "colliding with a tree stump and an axe". A common and useful
    // interpretation is: when this object collides with either a tree stump OR an axe,
    // it should be destroyed and spawn pieces. If you truly need detection only when
    // BOTH are touching this object at the same time, I can change the logic to track
    // contacts and require both to be present.

    // Handle physics collisions
    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        if (other.CompareTag(axeTag))
        {
            SpawnPieces();
            Destroy(gameObject);
        }
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(axeTag))
        {
            SpawnPieces();
            Destroy(gameObject);
        }
    }

    
    private void SpawnPieces()
    {
        Vector3 pos = transform.position;

        if (piece1 == null || piece2 == null || piece3 == null || piece4 == null)
        {
            Debug.LogWarning($"{name}: One or more piece prefabs are not assigned on {GetType().Name}.");
        }
        
        InstantiateIfNotNull(piece1, pos + new Vector3(-offset, 0f, -offset));
        InstantiateIfNotNull(piece2, pos + new Vector3(offset, 0f, -offset));
        InstantiateIfNotNull(piece3, pos + new Vector3(-offset, 0f, offset));
        InstantiateIfNotNull(piece4, pos + new Vector3(offset, 0f, offset));
    }

    private void InstantiateIfNotNull(GameObject prefab, Vector3 position)
    {
        if (prefab != null)
            Instantiate(prefab, position, Random.rotation);
    }

}
