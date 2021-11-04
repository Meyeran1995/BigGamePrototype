using UnityEngine;

[RequireComponent(typeof(AkGameObj))]
public class ThemeController : MonoBehaviour
{
    [Header("Autostart")]
    [SerializeField] private bool playOnStart;
    [SerializeField] private int startIndex;
    private int currentThemeIndex = -1;
    [Header("Music")]
    [SerializeField] private MusicThemeSO[] themes;

    private void Start()
    {
        if(!playOnStart) return;
        
        themes[startIndex].StartTheme(gameObject);
        currentThemeIndex = startIndex;
    }

    public void PlayTheme(int newThemeIndex)
    {
        if(newThemeIndex < 0 || newThemeIndex == currentThemeIndex || newThemeIndex > themes.Length) return;
        
        if(currentThemeIndex >= 0)
            themes[currentThemeIndex].StopTheme();
        
        themes[newThemeIndex].StartTheme(gameObject);
    }

    public void PlayTheme(MusicThemeSO theme)
    {
        for (var index = 0; index < themes.Length; index++)
        {
            if(theme != themes[index]) continue;

            theme.StartTheme(gameObject);
            
            if(currentThemeIndex >= 0)
                themes[currentThemeIndex].StopTheme();
            
            currentThemeIndex = index;
            return;
        }
        
        Debug.LogWarning($"Theme {theme.name} could not be found among registered themes");
    }

    public void StopThemeMusic()
    {
        if(currentThemeIndex < 0) return;

        themes[currentThemeIndex].StopTheme();
        currentThemeIndex = -1;
    }
}
