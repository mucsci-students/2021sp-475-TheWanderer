using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    private Archer archer;
    public GameObject playerObj;

    public float speed = 40f;
    private float destroyTimer = 0f;
    private int atk = 20;

    public AudioSource impact;

    // Start is called before the first frame update
    void Start()
    {
        archer = playerObj.GetComponent<Archer>();
    }

    // Update is called once per frame
    void Update()
    {
      

        destroyTimer += Time.deltaTime;
        transform.Translate(Mathf.Abs(speed) * Time.deltaTime,0f, 0f);

        if(destroyTimer >= 1f)
        {
            Destroy(this.gameObject);
        }


    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Land"))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (archer.powerUp)
        {
            atk = 35;
        }
        else
        {
            atk = 20;
        }

        impact.Play();
        if (other.gameObject.CompareTag("enemy"))
        {
            if(other.GetComponent<enemyScript>() != null)
            {
                other.gameObject.GetComponent<enemyScript>().takeDamage(atk);
                other.gameObject.GetComponent<enemyScript>().agroRange+=5;
                Destroy(this.gameObject);
            }
            else if(other.GetComponent<enemyScript2>() != null)
            {
                other.gameObject.GetComponent<enemyScript2>().takeDamage(atk);
                other.gameObject.GetComponent<enemyScript2>().agroRange+=5;
                Destroy(this.gameObject);
            }
            else if(other.GetComponent<bossScript>() != null)
            {
                other.GetComponent<bossScript>().takeDamage(atk+15);
                Destroy(this.gameObject);
            }
            else if(other.GetComponent<bossScript2>() != null)
            {
                other.GetComponent<bossScript2>().takeDamage(atk);
                Destroy(this.gameObject);
            }
            else if(other.gameObject.CompareTag("border"))
            {
                Destroy(this.gameObject);
            }
            else if(other.GetComponent<archerEnemy>() != null)
            {
                other.GetComponent<archerEnemy>().takeDamage(atk);
            }
            else if(other.GetComponent<soldierEnemy>() != null)
            {
                other.GetComponent<soldierEnemy>().takeDamage(atk);
                other.gameObject.GetComponent<soldierEnemy>().agroRange+=5;
            }
        }
       
    }
}
