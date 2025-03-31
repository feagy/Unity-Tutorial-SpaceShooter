using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if position of laser greater than 8
        //destroy it
        if (transform.position.y>8f)
        {
            //check if this object has a parent
            //destroy the parent too
            if(transform.parent!= null)
            {
                Destroy(transform.parent.gameObject); //
            }
            Destroy(this.gameObject);
        }
    }
}
