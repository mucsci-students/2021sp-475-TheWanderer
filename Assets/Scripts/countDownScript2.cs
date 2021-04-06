using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countDownScript2 : MonoBehaviour
{
    public Text m_MyText;

    public GameObject Spawner;
    spawn spawner;

    public GameObject boss;
    bossScript bossEnemy;
    // Start is called before the first frame update
    void Start()
    {
        spawner = Spawner.GetComponent<spawn>();
        bossEnemy = boss.GetComponent<bossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_MyText.gameObject.CompareTag("number"))
        {
            m_MyText.text = "(Wave "+ (spawner.finalWave+1) + ")";
            if(spawner.finalWave == spawner.waves.Length)
            {
                m_MyText.text = "Level Completed!";
            }
        }
        else if(m_MyText.gameObject.CompareTag("timer"))
        {
            int a = (int) spawner.waveCountdown;
            m_MyText.text = a.ToString();
            if(spawner.finalWave == spawner.waves.Length)
            {
                Destroy(gameObject);
            }
        }
        else if(m_MyText.gameObject.CompareTag("bossTimer"))
        {
            int a = (int) bossEnemy.reviveTimer;
            m_MyText.text = "1.Defeat the boss remnants before revival: "+a.ToString() +"\n 2.Finish all remaining enemies";
        }
    }
}
