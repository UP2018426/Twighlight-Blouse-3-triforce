using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int sceneBuildIndexToChangeTo;

    public Image healthBar;

    public Image stealthBar;

    public bool isPaused = false;

    public bool isDead = false;

    public bool endscreen = false;

    public GameObject pauseMenu;

    public GameObject deathScreen;

    public GameObject endScreen;

    public PlayerController player;

    public float timer;

    private void Awake()
    {
        isPaused = false;
        isDead = false;
        Time.timeScale = 1f;
    }

    public void NextScene()
    {
        int sceneToLoad;
        sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !endscreen && !isDead)
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                Resume();
                isPaused = false;
            }
        }

        if(boss.GetComponent<boss>().currentHealth <= 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                endScreen.SetActive(true);
                endscreen = true;
                isPaused = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        //Debug.Log(player.health.currentHealth / player.health.maxHealth);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            HudUpdate();
            if (player.currentHealth <= 0)
            {
                Time.timeScale = 0f;
                isDead = true;
                Cursor.lockState = CursorLockMode.None;
                deathScreen.SetActive(true);
            }
        }
        
    }

    public List<GameObject> enemies = new List<GameObject>();

    GameObject boss;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartCoroutine("EnemyAddDelay",1f);
        }
    }

    IEnumerator EnemyAddDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("enemy").Length; i++)
        {
            enemies.Add(GameObject.FindGameObjectsWithTag("enemy")[i]);
        }

        int rand = Random.Range(0,enemies.Count);

        enemies[rand].GetComponent<EnemyNav>().holdKey = true;

        boss = GameObject.FindGameObjectWithTag("boss");
    }

    void HudUpdate()
    {
        healthBar.fillAmount = player.currentHealth / player.maxHealth;

        //int record = 1000;
        
        //for (int i = 0; i < enemies.Count; i++)
        //{
        //    float d = enemies[i].GetComponent<EnemyFOV>().DetectionLevel;
        //    //Debug.Log(d);
        //    if (d < record)
        //    {
        //        record = Mathf.RoundToInt(d);
        //    }
        //    Debug.Log(record);
        //}
        //stealthBar.fillAmount = record / 100f;//isues with changing how full the bar is
    }
}