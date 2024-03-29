using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class UI_FPS : MonoBehaviour
{
    [SerializeField] private InitializationSystem _initializationSystem;
    [SerializeField] private float period = 0.1f;
    private float count;
    private float timer;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            count = 1f / Time.unscaledDeltaTime;
            GetComponent<UnityEngine.UI.Text>().text = "FPS: " + (Mathf.Round(100 * count) / 100f).ToString();
            timer = Time.unscaledTime + period;
        }
    }
}
