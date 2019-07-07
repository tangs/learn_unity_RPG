using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;

    float timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay > 0.0f)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay <= 0.0f)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        dialogBox.SetActive(true);
        timerDisplay = displayTime;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    RubyController ruby = collision.gameObject.GetComponent<RubyController>();
    //    if (ruby != null)
    //    {
    //        dialogBox.SetActive(true);
    //        timerDisplay = 0.0f;
    //    }
    //}
}
