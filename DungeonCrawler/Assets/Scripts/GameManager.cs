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

    public GameObject pauseMenu;

    public GameObject deathScreen;

    public PlayerController player;

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

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneBuildIndexToChangeTo);
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
        if (Input.GetButtonDown("Cancel"))
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

    private void Start()
    {
        StartCoroutine("EnemyAddDelay",1f);

        
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
    }

    void HudUpdate()
    {
        healthBar.fillAmount = player.currentHealth / player.maxHealth;

        var record = 0f;
        
        for (int i = 0; i < enemies.Count; i++)
        {
            var d = enemies[i].GetComponent<EnemyFOV>().DetectionLevel;

            if (d < record)
            {
                record = d;
            }
        }

        stealthBar.fillAmount = record / 100;
    }


}