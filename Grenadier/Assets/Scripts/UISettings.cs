
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UISettings:MonoBehaviour
    {
        [SerializeField] private Slider powerSlider;
        public Slider PowerSlider => powerSlider;


        [SerializeField] private Vector2 minMaxPower;

        [SerializeField] private InputField bouncesCountField;
        public InputField BouncesCountField => bouncesCountField;

        private void Start()
        {
            if (minMaxPower.x > minMaxPower.y)
            {
                minMaxPower.y = minMaxPower.x;
            }
            powerSlider.minValue = minMaxPower.x;
            powerSlider.maxValue = minMaxPower.y;
        }

        public void SetupStartValues(float power,int bouncesCount)
        {
            PowerSlider.value = power;
            BouncesCountField.text = bouncesCount.ToString();
        }
        
    }
