using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillButtontimer : MonoBehaviour
{
    public TMP_Text countdownText;
    public TMP_Text nextText;
    public Button button;
    public GameObject objectPrefab;
    private GameObject objectToDestroy;
    public float countdownTime = 20f;
    private bool isCountingDown = false;
    public int delayBeforeDestruction = 5;

    void Start()
    {
        
    }

    void Update()
    {
        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(countdownTime).ToString();
            if (countdownTime > 19){
                StartCoroutine(DestroyObjectAfterDelay());
                countdownText.gameObject.SetActive(true);
                nextText.gameObject.SetActive(false);
            }
            if (countdownTime <= 0)
            {
                countdownText.gameObject.SetActive(false);
                nextText.gameObject.SetActive(true);
                isCountingDown = false;
                button.interactable = true;
            }
        }
    }

    public void StartCountdown()
    {
        countdownTime = 20f;
        countdownText.gameObject.SetActive(true);
        nextText.gameObject.SetActive(false);
        isCountingDown = true;
        button.interactable = false;
        
        // Create the destroyable game object
        if (objectPrefab != null)
        {
            objectToDestroy = Instantiate(objectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    private IEnumerator DestroyObjectAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDestruction);
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}

