using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerUpNumber;
    [SerializeField]
    private AudioClip _powerupSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down* _speed*Time.deltaTime);
        if(transform.position.y<-5.8f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_powerupSound,transform.position);
            Player player=other.GetComponent<Player>();
            
            if (player!=null)
            {
                
                switch (_powerUpNumber) { 
                   case 0:
                        player.TripleShotActive();
                        break;
                   case 1:
                        player.SpeedBoostActive();
                        break; 
                   case 2:
                        player.ShieldPowerupActive();
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
