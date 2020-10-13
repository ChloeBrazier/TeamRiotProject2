using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Weapon>())
        {
            StopCoroutine("MoveWeapon");
            Destroy(collision.gameObject, 0.7f);
        }
    }
}
