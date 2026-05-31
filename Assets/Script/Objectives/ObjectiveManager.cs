using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [Header("Objectives")]
    public bool tabletCollected;
    public bool fossilCollected;
    public bool meteorCollected;

    [Header("Win UI")]
    public GameObject winPanel;

    public ObjectiveUI objectiveUI;
    public PlayerMovement playerMovement;
    public PlayerInteraction playerInteraction;
    public PhotoSystem photoSystem;

    public void CompleteObjective(string objectiveID)
    {
        switch (objectiveID)
        {
            case "Tablet":
                tabletCollected = true;
                break;

            case "Fossil":
                fossilCollected = true;
                break;

            case "Meteor":
                meteorCollected = true;
                break;
        }
        objectiveUI.CompleteObjectiveUI(objectiveID);
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (
            tabletCollected &&
            fossilCollected &&
            meteorCollected
        )
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN");

        winPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;

        playerMovement.enabled = false;
        playerInteraction.enabled = false;
        photoSystem.enabled = false;
    }
}