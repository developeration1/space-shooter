using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    float _speed = 3;
    [SerializeField]
    [Range(0, 2)]
    int _powerupID;
    [SerializeField]
    AudioClip _powerupAudio;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -6.5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        break;
                    case 2:
                        player.ActivateShields();
                        break;
                    default:
                        Debug.LogError("Not a valid powerup ID");
                        break;
                }
            }
            AudioSource.PlayClipAtPoint(_powerupAudio, transform.position);
            Destroy(gameObject);
        }
    }
}
