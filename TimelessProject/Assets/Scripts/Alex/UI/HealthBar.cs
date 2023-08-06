using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private FloatReference healthValue;


    private void Update()
    {
        healthSlider.value = healthValue.value;


        if (healthSlider.value >= 60)
        {
            healthSlider.image.color = Color.green;
        } else if (healthSlider.value < 60 && healthSlider.value >= 40)
        {
            healthSlider.image.color = Color.yellow;
        } else if (healthSlider.value < 40)
        {
            healthSlider.image.color = Color.red;
        }
    }
}
