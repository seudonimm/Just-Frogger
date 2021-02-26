using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject transitionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            StartCoroutine(Transition());
        }

    }

    IEnumerator Transition()
    {

        transitionPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Gameplay");


    }

}
