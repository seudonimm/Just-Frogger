using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] int rand;

    [SerializeField] List<GameObject> shapes;
    [SerializeField] List<GameObject> triangles;

    [SerializeField] GameObject spawnPoint, gameOverUI;

    [SerializeField] float timer, timerDefault, gameTimer;

    [SerializeField] TextMeshProUGUI gameTimerText, endGameTimeText, hiScoreTimeText, newScoreText;

    [SerializeField] PlayerController pc;

    [SerializeField] TextMeshProUGUI directions;

    [SerializeField] GameObject transitionPanel;

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDefault;

        gameOverUI.SetActive(false);

        newScoreText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!pc.lose)
        {
            gameTimer += Time.deltaTime;
            gameTimerText.text = gameTimer.ToString();
            TimedEvents();
        }

        if (pc.lose || !pc)
        {
            if (!gameOverUI.activeSelf)
            {
                gameOverUI.SetActive(true);
                endGameTimeText.text = gameTimerText.text;
            }

            if(Values.HiScore < gameTimer)
            {
                Values.HiScore = gameTimer;
                newScoreText.enabled = true;
            }
            hiScoreTimeText.text = Values.HiScore.ToString();

            if(Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                StartCoroutine(TransitionOut());
            }
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (directions.enabled)
            {

                StartCoroutine(FadeOut());
                //directions.enabled = false;
            }
            Instantiate(shapes[rand], spawnPoint.transform.position, spawnPoint.transform.rotation);
            timer = timerDefault;

            RandomNumber();
            //rand = RandomNumber();
        }
    }

    IEnumerator FadeOut()
    {
        directions.alpha = Mathf.Lerp(1, 0, Time.time);

        yield return new WaitForSeconds(1f);
        directions.enabled = false;


    }
    void TimedEvents()
    {
        if(!triangles[0].activeSelf && gameTimer > 10)
        {
            triangles[0].SetActive(true);
        }
        if (!triangles[1].activeSelf && gameTimer > 30)
        {
            triangles[1].SetActive(true);
        }
        if (!triangles[2].activeSelf && gameTimer > 60)
        {
            triangles[2].SetActive(true);
        }
        if (!triangles[3].activeSelf && gameTimer > 100)
        {
            triangles[3].SetActive(true);
        }

    }

    IEnumerator TransitionOut()
    {
        transitionPanel.GetComponent<FadeAlpha>().fadeOut = false;

        transitionPanel.GetComponent<FadeAlpha>().fadeIn = true;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Gameplay");

    }


    void RandomNumber()
    {
        rand = Random.Range(0, shapes.Capacity);

        //return rand;
    }
}
