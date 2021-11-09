using UnityEngine;

[RequireComponent(typeof(AkGameObj))]
public class ThemeController : MonoBehaviour
{
    [Header("Autostart")]
    [SerializeField] private bool playOnStart;
    [SerializeField] private int startIndex;
    private int currentThemeIndex = -1;
    [Header("Music")]
    [SerializeField] private EventEmitterSO[] themes;

    private void Start()
    {
        if(!playOnStart) return;
        
        themes[startIndex].StartEmitting(gameObject);
        currentThemeIndex = startIndex;
    }

    public void PlayTheme(int newThemeIndex)
    {
        if(newThemeIndex < 0 || newThemeIndex == currentThemeIndex || newThemeIndex > themes.Length) return;
        
        if(currentThemeIndex >= 0)
            themes[currentThemeIndex].StopEmitting();
        
        themes[newThemeIndex].StartEmitting(gameObject);
        currentThemeIndex = newThemeIndex;
    }

    public void PlayTheme(EventEmitterSO theme)
    {
        for (var index = 0; index < themes.Length; index++)
        {
            if(theme != themes[index]) continue;

            theme.StartEmitting(gameObject);
            
            if(currentThemeIndex >= 0)
                themes[currentThemeIndex].StopEmitting();
            
            currentThemeIndex = index;
            return;
        }
        
        Debug.LogWarning($"Theme {theme.name} could not be found among registered themes");
    }

    public void StopThemeMusic()
    {
        if(currentThemeIndex < 0) return;

        themes[currentThemeIndex].StopEmitting();
        currentThemeIndex = -1;
    }
}
