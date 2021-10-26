using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{
    public bool isInPuzzleArea = false;
    private bool delay = false;
    [SerializeField] private AK.Wwise.Event explorationTheme;
    [SerializeField] private AK.Wwise.Event puzzleTheme;

    // Start is called before the first frame update
    void Start()
    {
        explorationTheme.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(isInPuzzleArea && !delay)
        {
            delay = true;
            explorationTheme.Stop(gameObject);
            puzzleTheme.Post(gameObject);
        }else if (!isInPuzzleArea && delay)
        {
            delay = false;
            puzzleTheme.Stop(gameObject);
            explorationTheme.Post(gameObject);
        }        
    }
}
