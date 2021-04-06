using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    #region GameControl Dialogue
    [Header("Character Components")]
    [SerializeField] private GameObject wanderer;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject unknownSoldier;
    #endregion

    Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        if(wanderer.gameObject.activeInHierarchy)
        {
            Vector3 currentPosition = wanderer.transform.position;
        }
        else if (archer.gameObject.activeInHierarchy)
        {
            Vector3 currentPosition = archer.transform.position;
        }
        else if(unknownSoldier.gameObject.activeInHierarchy)
        {
            Vector3 currentPosition = unknownSoldier.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wanderer.gameObject.activeInHierarchy)
        {
            currentPosition = wanderer.transform.position;
        }
        else if (archer.gameObject.activeInHierarchy)
        {
            currentPosition = archer.transform.position;
        }
        else if(unknownSoldier.gameObject.activeInHierarchy)
        {
            currentPosition = unknownSoldier.transform.position;
        }

        if(Input.GetKey(KeyCode.Alpha1))
        {
            wanderer.SetActive(true);
            archer.SetActive(false);
            unknownSoldier.SetActive(false);
            wanderer.transform.position = currentPosition;
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            archer.SetActive(true);
            wanderer.SetActive(false);
            unknownSoldier.SetActive(false);
            archer.transform.position = currentPosition;
        }
        else if(Input.GetKey(KeyCode.Alpha3))
        {
            unknownSoldier.SetActive(true);
            wanderer.SetActive(false);
            archer.SetActive(false);
            unknownSoldier.transform.position = currentPosition;
        }
    }
}