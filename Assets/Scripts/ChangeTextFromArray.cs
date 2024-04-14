using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextFromArray : MonoBehaviour
{
    public string newText;
    public TMP_Text[] targetTexts;

    private void Start()
    {
        newText = Javascripthooks.Instance.CompanyName;

        Debug.Log("newText : " + newText);
        UpdateText();
    }

    public void UpdateText()
    {
        foreach (TMP_Text t in targetTexts)
        {
            t.text = newText;
        }
    }
}
