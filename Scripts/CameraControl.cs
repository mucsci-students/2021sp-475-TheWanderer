using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject wanderer;
    public GameObject archer;
    public GameObject soldier;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (wanderer.activeSelf)
        {
            transform.position = new Vector3(wanderer.transform.position.x, wanderer.transform.position.y+15, -10);
        }
        else if (archer.activeSelf)
        {
            transform.position = new Vector3(archer.transform.position.x, archer.transform.position.y+15, -10);
        }
        else if (soldier.activeSelf)
        {
            transform.position = new Vector3(soldier.transform.position.x, soldier.transform.position.y+15, -10);
        }
    }
}
