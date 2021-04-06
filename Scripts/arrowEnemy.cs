using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowEnemy : MonoBehaviour
{
    private archerEnemy archer;
    public GameObject playerObj;

    public float speed = 40f;
    private float destroyTimer = 0f;

    public AudioSource impact;

    // Start is called before the first frame update
    void Start()
    {
        archer = playerObj.GetComponent<archerEnemy>();
       
    }

    // Update is called once per frame
    void Update()
    { 
        destroyTimer += Time.deltaTime;
        transform.Translate(Mathf.Abs(speed) * Time.deltaTime,0f, 0f);

        if(destroyTimer >= 2.5f)
        {
            Destroy(this.gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        impact.Play();
        if (other.gameObject.CompareTag("wanderer"))
        {
            other.gameObject.GetComponent<Wanderer>().takeDamage(20);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("archer"))
        {
            other.gameObject.GetComponent<Archer>().takeDamage(20);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("unknownSoldier"))
        {
            other.gameObject.GetComponent<UnknownSoldier>().takeDamage(20);
            Destroy(this.gameObject);
        }
       
    }
}
