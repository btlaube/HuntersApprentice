using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{

    [SerializeField] private Image fillBarCurrent;

    
    public void UpdateFillBar(float currentValue, float maxValue)
    {
        fillBarCurrent.fillAmount = currentValue / maxValue;
    }

}
