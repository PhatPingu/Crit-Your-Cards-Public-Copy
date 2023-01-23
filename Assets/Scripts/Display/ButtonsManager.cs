using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] BattleDataInitialization BattleDataInitialization;
    [SerializeField] GameManager GameManager;
    [SerializeField] GameStates GameStates;
    [SerializeField] GameObject feedbackPrompt;


    public void StartTheGame()
    {
        BattleDataInitialization.InitializeBattleData();
        GameManager.StartTheGame();
        gameObject.SetActive(false);
    }

    public void ReStartTheGameData()
    {
        BattleDataInitialization.ReInitializeBattleData();
        GameManager.StartTheGame();
        gameObject.SetActive(false);
        GameStates.currentGameState = GameStates.GameState.EnemyEndTurn;
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void AskFeedbackBeforeQuit()
    {
        feedbackPrompt.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToFeedbackForm()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdYldoDlER4dMlBHIbJ9PQSGamItzyr8B_oGfLNybkfZhGMxQ/viewform?usp=sf_link");
    }

}
