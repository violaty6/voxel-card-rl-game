using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGen : MonoBehaviour
{
    [SerializeField]List<GameObject> areaTypes;
    public void GenerateArea(Component sender , object[] data)
    {
        AIPlayerBase ai = sender.GetComponent<AIPlayerBase>() as AIPlayerBase;
        if (ai != null && data[0] is int)
        {
          GameObject instedArea = Instantiate(areaTypes[(int)data[0]],ai.transform.position,Quaternion.identity);
          instedArea.GetComponent<Area>().sourceAI = ai;
        }
        else
        {
            Debug.LogWarning("The sender is not of type Card.");
        }
    }
}
