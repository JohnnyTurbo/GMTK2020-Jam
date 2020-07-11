using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMG.GMTK2020
{
    public class DialogueController : MonoBehaviour
    {

        private static DialogueController _instance;
        public static DialogueController instance;

        public delegate void EnterInput(string inputString);
        public delegate void BasicLineCall();

        public delegate void EndDialogue();
        EndDialogue endDialogue;

        List<DialogueObject> currentConversation;
        List<DialogueObject> exampleDialogue, example2Dialogue;

        enum DialogueType { BasicLine, TextInput, Choice }

        DialogueObject curDialogueObject;

        GameObject dialogueCanvas;
        Text speakerNameText, dialogueText;
        InputField dialogueInputField;
        Button advanceDialogueButton;

        public string playerName { get; private set; }
        int dialogueIndex;

        /// <summary>
        /// Awake function called as soon as the GameObject is set active and before the start function
        /// </summary>
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                _instance = this;
            }
            instance = _instance;

            endDialogue = null;

            dialogueCanvas = transform.Find("DialogueCanvas").gameObject;
            Transform dialogueBox = dialogueCanvas.transform.Find("DialogueBox");
            speakerNameText = dialogueBox.Find("SpeakerNameText").GetComponent<Text>();
            dialogueText = dialogueBox.Find("DialogueText").GetComponent<Text>();
            dialogueInputField = dialogueBox.Find("DialogueInputField").GetComponent<InputField>();
            advanceDialogueButton = dialogueBox.Find("AdvanceDialogueButton").GetComponent<Button>();

            dialogueCanvas.SetActive(false);
        }

        /// <summary>
        /// Start funtion called before the first frame of gameplay
        /// </summary>
        private void Start()
        {
            currentConversation = new List<DialogueObject>();
            //exampleDialogue = new List<DialogueObject>();
            //example2Dialogue = new List<DialogueObject>();

            //SetupExampleDialogue();
            //StartExampleDialogue();
        }

        /// <summary>
        /// Sets up the first part of the example dialogue
        /// </summary>
        void SetupExampleDialogue()
        {
            DialogueObject line1 = new DialogueObject("You", "Hey, it's Joe right?");
            DialogueObject line2 = new DialogueObject("Joe", "Yes..?");
            DialogueObject line3 = new DialogueObject("You", "That's right! We met a few years back at the sales convention in Atlanta. How have you been?");
            DialogueObject line4 = new DialogueObject("Joe", "Oh I remember that! Remind me what is your name again?", InputField.ContentType.Standard, "Your name...", 32, EnterPlayerName);

            exampleDialogue.Add(line1);
            exampleDialogue.Add(line2);
            exampleDialogue.Add(line3);
            exampleDialogue.Add(line4);
        }

        /// <summary>
        /// Begins the example dialogue
        /// </summary>
        public void StartExampleDialogue()
        {
            ChangeCurConversation(exampleDialogue, StartExample2Dialogue);
            ShowDialogue(curDialogueObject);
        }

        /// <summary>
        /// Sets up the second part of the example dialogue
        /// </summary>
        void SetupExample2Dialogue()
        {
            DialogueObject line1 = new DialogueObject(playerName, "I'm " + playerName);
            DialogueObject line2 = new DialogueObject("Joe", "Oh, " + playerName + " of course how could I forget!");
            DialogueObject line3 = new DialogueObject("Joe", "Hey I've got to run to another meeting but we should catch up later");
            DialogueObject line4 = new DialogueObject(playerName, "Sounds good, talk to you then!");
            DialogueObject line5 = new DialogueObject("Joe", "Later!");

            example2Dialogue.Add(line1);
            example2Dialogue.Add(line2);
            example2Dialogue.Add(line3);
            example2Dialogue.Add(line4);
            example2Dialogue.Add(line5);
        }

        /// <summary>
        /// Begins the second part of the example dialogue
        /// </summary>
        void StartExample2Dialogue()
        {
            ChangeCurConversation(example2Dialogue, EndExampleDialogue);
            ShowDialogue(curDialogueObject);
        }

        /// <summary>
        /// Called when the example dialogue has ended. Debugs a message to the console so you know...
        /// </summary>
        void EndExampleDialogue()
        {
            Debug.Log("Example dialogue conversation has ended.");
        }

        /// <summary>
        /// Sets the name of the main character. Sets up the 2nd Part of the example conversation, as that uses the player's name
        /// </summary>
        /// <param name="enteredName"></param>
        void EnterPlayerName(string enteredName)
        {
            playerName = enteredName;
            SetupExample2Dialogue();
        }

        public void OneLiner(string speakerName, string line, EndDialogue _endDialogue)
		{
            endDialogue = _endDialogue;
            curDialogueObject = new DialogueObject(speakerName, line);
            ShowDialogue(curDialogueObject);
		}

        /// <summary>
        /// Displays a single line of dialogue spoken by the player.
        /// </summary>
        /// <param name="line">Line of dialogue for the player.</param>
        public void MainCharOneLiner(string line)
        {
            endDialogue = null;
            curDialogueObject = new DialogueObject(playerName, line);
            ShowDialogue(curDialogueObject);
        }

        /// <summary>
        /// Displays a single line of dialogue with the input field. The player is the speaker.
        /// </summary>
        /// <param name="line">Dialogue line player speaks.</param>
        /// <param name="placeholderText">Text displayed in input field before input is entered</param>
        /// <param name="enterFunction">Function to be called when input is entered. Function must be of type void and take in a single string.</param>
        public void MainCharInputOneLiner(string line, string placeholderText, EnterInput enterFunction)
        {
            endDialogue = null;
            curDialogueObject = new DialogueObject(playerName, line, InputField.ContentType.Alphanumeric, placeholderText, 16, enterFunction);
            ShowDialogue(curDialogueObject);
        }

        /// <summary>
        /// Changes the current conversation.
        /// </summary>
        /// <param name="newConversation">New conversation to be active</param>
        /// <param name="_endDialogue">Function to be called once dialogue ends. Function must be type void with no parameters.</param>
        void ChangeCurConversation(List<DialogueObject> newConversation, EndDialogue _endDialogue)
        {
            endDialogue = _endDialogue;
            currentConversation.RemoveRange(0, currentConversation.Count);
            currentConversation.AddRange(newConversation);

            dialogueIndex = 0;
            curDialogueObject = currentConversation[dialogueIndex];
        }

        /// <summary>
        /// Shows the dialogue UI with the current dialogue line
        /// </summary>
        /// <param name="line">Curremt line that shoudl be displayed</param>
        void ShowDialogue(DialogueObject line)
        {
            dialogueCanvas.SetActive(true);
            dialogueInputField.gameObject.SetActive(false);
            speakerNameText.text = line.speakerName;
            dialogueText.text = line.dialogueLine;

            switch (line.dialogueType)
            {
                case (DialogueType.BasicLine):
                    if (line.basicLineCall != null)
                    {
                        line.basicLineCall();
                    }
                    break;

                case DialogueType.TextInput:
                    dialogueInputField.gameObject.SetActive(true);
                    dialogueInputField.contentType = line.inputContentType;
                    dialogueInputField.characterLimit = line.maxInputLength;
                    dialogueInputField.text = "";
                    dialogueInputField.placeholder.GetComponent<Text>().text = line.inputPlaceholderText;
                    dialogueInputField.Select();
                    break;

                default:
                    Debug.LogError("Error unknown DialogueType " + line.dialogueType);
                    break;

            }
        }

        /// <summary>
        /// Attempts to advance the dialogue to the next line.
        /// </summary>
        public void AdvanceDialogue()
        {
            if (curDialogueObject.dialogueType == DialogueType.TextInput && !CheckForInput())
            {
                return;
            }
            dialogueIndex++;
            if (dialogueIndex >= currentConversation.Count)
            {
                HideDialogue();
                if (endDialogue != null)
                {
                    endDialogue();
                }
                return;
            }
            curDialogueObject = currentConversation[dialogueIndex];
            ShowDialogue(curDialogueObject);
        }

        /// <summary>
        /// Checks if the player has entered anything in the input field.
        /// </summary>
        /// <returns>True if there is input, false if input field is empty.</returns>
        bool CheckForInput()
        {
            if (dialogueInputField.text.Length > 0)
            {
                //TODO Probably not the smartest idea to have this here
                curDialogueObject.dialogueInputCallback(dialogueInputField.text);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Hides the dialogue UI
        /// </summary>
        public void HideDialogue()
        {
            Debug.Log("HideDialogue()");
            speakerNameText.text = "";
            dialogueText.text = "";
            dialogueCanvas.SetActive(false);
        }

        [System.Serializable]
        class DialogueObject
        {
            public DialogueType dialogueType { get; private set; }

            public InputField.ContentType inputContentType;

            public string speakerName;
            public string dialogueLine;
            public string inputPlaceholderText;
            public int maxInputLength;
            public EnterInput dialogueInputCallback;
            public BasicLineCall basicLineCall;

            /// <summary>
            /// Creates a basic DialogueObject. Just shows the person speaking and what they are saying.
            /// </summary>
            /// <param name="name">Name of the person speaking</param>
            /// <param name="line">Dialogue the person says</param>
            public DialogueObject(string name, string line)
            {
                dialogueType = DialogueType.BasicLine;

                speakerName = name;
                dialogueLine = line;
                basicLineCall = null;
            }

            /// <summary>
            /// Creates a basic DialogueObject. lineCall delegate will be called when this dialogue box is shown.
            /// </summary>
            /// <param name="name">Name of the person speaking</param>
            /// <param name="line">Dialogue the person says</param>
            /// <param name="lineCall">Fucntion to be called when dialogue line is shown. Function must be of type void and take in no parameters.</param>
            public DialogueObject(string name, string line, BasicLineCall lineCall)
            {
                dialogueType = DialogueType.BasicLine;

                speakerName = name;
                dialogueLine = line;
                basicLineCall = lineCall;
            }

            /// <summary>
            /// Creates a DialogueObject of type TextInput. Allows the player to input text into the conversation.
            /// </summary>
            /// <param name="name">Name of the person speaking</param>
            /// <param name="line">Dialogue the person says</param>
            /// <param name="contentType">Type of input to be entered into the text input field.</param>
            /// <param name="placeholderText">Text to be displayed in the input field before the player enters text</param>
            /// <param name="maxLength">Max length of the string to be entered. 0 = no max length.</param>
            /// <param name="enterInput">Function to be called when player advances dialogue. Function must be of type void and take in one string variable.</param>
            public DialogueObject(string name, string line, InputField.ContentType contentType, string placeholderText, int maxLength,
                                  EnterInput enterInput)
            {
                dialogueType = DialogueType.TextInput;

                speakerName = name;
                dialogueLine = line;
                inputContentType = contentType;
                inputPlaceholderText = placeholderText;
                maxInputLength = maxLength;
                dialogueInputCallback = enterInput;
            }
        }
    }
}