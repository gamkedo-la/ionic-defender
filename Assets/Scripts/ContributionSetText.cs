using UnityEngine;
using UnityEngine.UI;
public class ContributionSetText : MonoBehaviour
{
    public Text creditTextField;
    private void Awake()
    {
        creditTextField.text = Contributors.GetCreditsText();        
    }
}
