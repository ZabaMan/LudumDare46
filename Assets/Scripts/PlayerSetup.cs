using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSetup : MonoBehaviour
{
    private AddPlayer addPlayer;

    public Image playerImage;
    public Text honorific;
    public InputField[] inputFields;
    private int currentInput = 0;
    public Text[] chosenControls;
    public string[] recordedControls;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(inputFields[0].gameObject);
        inputFields[0].ActivateInputField();
    }

    public void SetPlayerColorAndHonorific(Color playerColor, string honorific)
    {
        playerImage.color = playerColor;
        this.honorific.text = honorific;
    }

    public void Reselect()
    {
        if (currentInput < inputFields.Length)
        {
            inputFields[currentInput].ActivateInputField();
            inputFields[currentInput].Select();
        }
    }

    public void SetAddPlayer(AddPlayer addPlayer)
    {
        this.addPlayer = addPlayer;
    }

    public void NextLetter()
    {
        if (!enabled || currentInput >= inputFields.Length) return;
        var inputFieldText = inputFields[currentInput].text;
        if (inputFieldText == "") return;
        if (!addPlayer.CheckChosenControls(inputFieldText))
        {
            inputFields[currentInput].text = "";
            inputFields[currentInput].ActivateInputField();
            inputFields[currentInput].Select();
            return;
        }
        addPlayer.AddChosenControl(inputFieldText);

        recordedControls[currentInput] = inputFieldText;
        chosenControls[currentInput].text = inputFieldText.ToUpper();
        currentInput++;
        inputFields[currentInput - 1].text = "";
        if (currentInput == inputFields.Length)
        {
            LockInControls();
            inputFields[currentInput-1].DeactivateInputField();
            return;
        }
        EventSystem.current.SetSelectedGameObject(inputFields[currentInput].gameObject);
        inputFields[currentInput].ActivateInputField();
        
    }

    public void LockInControls()
    {
        addPlayer.SetCurrentPlayersControls(recordedControls);
    }
}
