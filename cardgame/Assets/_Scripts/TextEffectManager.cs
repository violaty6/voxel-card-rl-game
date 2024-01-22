using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject TextPrefab;
    public void SpawnFeedbackText(Component sender , object[] data)
    {
        GameObject spawnedText = Instantiate(TextPrefab,(Vector3)data[0] + Vector3.up*3, Quaternion.identity);
        TextMeshProUGUI effectText = spawnedText.GetComponentInChildren<TextMeshProUGUI>();
        if ((float)data[1] < 0)
        {
            effectText.color = new Color32(255, 78, 69, 255);
            effectText.text =  data[1].ToString();
        }
        else
        {
            effectText.color = new Color32(125, 255, 69, 255);
            effectText.text = "+" + data[1];
        }
        spawnedText.transform.DOMoveY(spawnedText.transform.position.y + 2f,0.75f).OnComplete(() => Destroy(spawnedText.transform.root.gameObject));
    }
}
