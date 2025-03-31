using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Animator _enemyAnimation;
    private Player player;
    private AudioSource _audioSource;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource=GetComponent<AudioSource>();
        _enemyAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.down*_speed*Time.deltaTime);

        if (transform.position.y < -5.4f)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randomX, 7.4f, 0);
            //Random.Range(minvalue,maxvalue)----->for ccreating a random value between min and max
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //damage player
        //destroy us

        //if other is laser
        //destroy laser
        //destroy us
        if(other.tag == "Player")
        {
            //Player player = other.GetComponent<Player>(); //other içindeki playerýn script componentine eriþme
            
            if(player != null )
            {
                player.Damage();                          //erroru önlemek için
            }
            _enemyAnimation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,1.5f);
           
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(player != null )
            {
                player.IncreaseScore();
            }
            _enemyAnimation.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,1.5f);

        }
    }
}
