using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    bool ShouldQuitGame=>Input.GetKeyUp(KeyCode.Escape);
     void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldQuitGame)
        {
            QuitGame();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Confined;
        }
    }

    public void InCombat(bool inCombat)
    {
        if (inCombat)
        {
            MusicManager.Instance.PlayCombatMusic();
        }
        else
        {
            MusicManager.Instance.PlayPatrolMusic();
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //handle WebGL
        Application.Quit();
#endif
    }
}
