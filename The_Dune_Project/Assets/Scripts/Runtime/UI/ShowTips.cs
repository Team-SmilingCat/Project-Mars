using System.Collections;
using System.Collections.Generic;
using Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowTips : MonoBehaviour
{
	[SerializeField] private Image background;
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private List<ToolTip> tips;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ActivateToolTip(int id)
    {
	    foreach (var tip in tips)
	    {
		    Debug.Log("check tip");
		    if (id == tip.id && tip.id > 0)
		    {
			    StartCoroutine(ShowWrittenText(tip));
		    }
	    }

	}

    private IEnumerator ShowWrittenText(ToolTip tip)
    {
	    text.text = tip.message;
	    text.gameObject.SetActive(true);
	    background.gameObject.SetActive(true);
	    yield return new WaitForSeconds(tip.durationTime);
	    Debug.Log("deactivating");
	    text.gameObject.SetActive(false);
	    background.gameObject.SetActive(false);
    }
}
