using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HeneGames.DialogueSystem
{
    public class DialogueUI : MonoBehaviour, IUIPanel
    {
        private bool _isOpen;
        #region Singleton

        public static DialogueUI instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            //Hide dialogue and interaction UI at awake
            dialogueWindow.SetActive(false);
            interactionUI.SetActive(false);
        }

        #endregion

        private DialogueManager currentDialogueManager;
        private bool typing;
        private string currentMessage;
        private float startDialogueDelayTimer;

        [Header("References")]
        [SerializeField] private Image portrait;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private GameObject dialogueWindow;
        [SerializeField] private GameObject interactionUI;

        [Header("Settings")]
        [SerializeField] private bool animateText = true;

        [Range(0.1f, 1f)]
        [SerializeField] private float textAnimationSpeed = 0.5f;

        [Header("Next sentence input")]
        public KeyCode actionInput = KeyCode.Space;

        private void Update()
        {
            //Delay timer
            if(startDialogueDelayTimer > 0f)
            {
                startDialogueDelayTimer -= Time.deltaTime;
            }

            InputUpdate();
        }

        public virtual void InputUpdate()
        {
            //Next dialogue input
            if (Input.GetKeyDown(actionInput))
            {
                NextSentenceSoft();
            }
        }

        public void NextSentenceSoft()
        {
            if (startDialogueDelayTimer <= 0f)
            {
                if (!typing)
                {
                    NextSentenceHard();
                }
                else
                {
                    StopAllCoroutines();
                    typing = false;
                    messageText.text = currentMessage;
                }
            }
        }

        public void NextSentenceHard()
        {
            if (currentDialogueManager == null)
                return;

            currentDialogueManager.NextSentence(out bool lastSentence);

            if (lastSentence)
            {
                currentDialogueManager = null;
            }
        }

        public void Open() { }

        public void Close()
        {
            if (!_isOpen) return;
            currentDialogueManager?.StopDialogue();
        }

        public void StartDialogue(DialogueManager _dialogueManager)
        {
            startDialogueDelayTimer = 0.1f;
            currentDialogueManager = _dialogueManager;
            currentDialogueManager.StartDialogue();

            if (!_isOpen)
            {
                _isOpen = true;
                IUIPanel.Notify(this, true, true);
            }
        }

        public void ShowSentence(DialogueCharacter _dialogueCharacter, string _message)
        {
            StopAllCoroutines();

            dialogueWindow.SetActive(true);

            portrait.sprite = _dialogueCharacter.characterPhoto;
            nameText.text = _dialogueCharacter.characterName;
            currentMessage = _message;

            if (animateText)
            {
                StartCoroutine(WriteTextToTextmesh(_message, messageText));
            }
            else
            {
                messageText.text = _message;
            }
        }

        public void ClearText()
        {
            dialogueWindow.SetActive(false);
            if (_isOpen)
            {
                _isOpen = false;
                IUIPanel.Notify(this, false, false);
            }
        }

        public void ShowInteractionUI(bool _value)
        {
            interactionUI.SetActive(_value);
        }

        public bool IsProcessingDialogue()
        {
            if(currentDialogueManager != null)
            {
                return true;
            }

            return false;
        }

        public bool IsTyping()
        {
            return typing;
        }

        public int CurrentDialogueSentenceLenght()
        {
            if (currentDialogueManager == null)
                return 0;

            return currentDialogueManager.CurrentSentenceLenght();
        }

        IEnumerator WriteTextToTextmesh(string _text, TextMeshProUGUI _textMeshObject)
        {
            typing = true;

            _textMeshObject.text = "";
            char[] _letters = _text.ToCharArray();

            float _speed = 1f - textAnimationSpeed;

            foreach(char _letter in _letters)
            {
                _textMeshObject.text += _letter;

                if(_textMeshObject.text.Length == _letters.Length)
                {
                    typing = false;
                }

                yield return new WaitForSeconds(0.1f * _speed);
            }
        }
    }
}