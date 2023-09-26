using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Slider powerSlider;
    [SerializeField] private Text powerNumber;
    public Slider PowerSlider => powerSlider;


    [SerializeField] private Vector2 minMaxPower;

    [SerializeField] private InputField bouncesCountField;
    public InputField BouncesCountField => bouncesCountField;

    private void OnEnable()
    {
        powerSlider.onValueChanged.AddListener(ChangeNumber);
    }

    private void Start()
    {
        if (minMaxPower.x > minMaxPower.y)
        {
            minMaxPower.y = minMaxPower.x;
        }

        powerSlider.minValue = minMaxPower.x;
        powerSlider.maxValue = minMaxPower.y;
    }


    private void OnDisable()
    {
        powerSlider.onValueChanged.RemoveListener(ChangeNumber);
    }

    private void ChangeNumber(float number)
    {
        powerNumber.text = ((int)number).ToString();
    }

    public void SetupStartValues(float power, int bouncesCount)
    {
        ChangeNumber(power);
        PowerSlider.value = power;
        BouncesCountField.text = bouncesCount.ToString();
    }
}