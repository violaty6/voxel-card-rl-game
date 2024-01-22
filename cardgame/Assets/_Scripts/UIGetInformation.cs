using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGetInformation : MonoBehaviour
{
    private AIStateManager OwnerofUI;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider healthSlider;
    void Start()
    {
        OwnerofUI = transform.GetComponentInParent<AIStateManager>();
        StartCoroutine(WaitOneFrame());
    }
    public void AIGetInformation()
    {
        StartCoroutine(WaitOneFrame());
        nameText.text = OwnerofUI.currentCard.cardName;
        healthSlider.maxValue = OwnerofUI.maxHealth;
        healthSlider.value = OwnerofUI.currentHealth;
        if (OwnerofUI.isEnemy)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
        else
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        }
    }
    IEnumerator WaitOneFrame()
    {
        yield return 0;
        AIGetInformation();
    }
    public void UpdateUI()
    {
        if (OwnerofUI.currentHealth<=0)
        {
            transform.gameObject.SetActive(false); //WorldUI active false
        }
        else
        {
            transform.gameObject.SetActive(true); 
        }
        healthSlider.value = OwnerofUI.currentHealth;
    }
}
