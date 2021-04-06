using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{

    private bossScript boss;
    public GameObject playerObj;

    public float speed = 40f;
    private float destroyTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;
        transform.Translate(Mathf.Abs(speed) * Time.deltaTime,0f, 0f);

        if(destroyTimer >= 1.5f)
        {
            Destroy(this.gameObject);
        }

        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wanderer") || other.gameObject.CompareTag("archer") || other.gameObject.CompareTag("unknownSoldier"))
        {
            if(other.GetComponent<Wanderer>() != null)
            {
                other.gameObject.GetComponent<Wanderer>().takeDamage(5);
                Destroy(this.gameObject);
            }
            else if(other.GetComponent<Archer>() != null)
            {
                other.gameObject.GetComponent<Archer>().takeDamage(5);
                Destroy(this.gameObject);
            }
            else if(other.GetComponent<UnknownSoldier>() != null)
            {
                other.GetComponent<UnknownSoldier>().takeDamage(5);
                Destroy(this.gameObject);
            }
        }
        else if(other.gameObject.CompareTag("Land"))
        {
            Destroy(this.gameObject);
        }
        else if(other.gameObject.CompareTag("enemy"))
        {

        }
       
    }
}
