using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript2 : MonoBehaviour
{
    public GameObject gameObj;
    ProgressBar Health;
    public int HealthPoints = 100;
    Animator animator;
    public AudioSource damaged;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Health = gameObj.GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        Health.UpdateValue(HealthPoints);
        if(HealthPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void takeDamage(int damage)
    {
        damaged.Play();
        HealthPoints = HealthPoints - damage;
        animator.SetFloat("health", HealthPoints);
    }
}
