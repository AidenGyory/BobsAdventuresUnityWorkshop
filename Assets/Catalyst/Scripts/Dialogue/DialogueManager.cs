using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI nameText, text;
    public RectTransform textBox;
    public CanvasGroup cg;
    public static DialogueManager instance;
    DialogueContainer dialogue;
    int lineIndex = -1;
    bool rowFinished = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ui.SetActive(false);
    }

    private void Update()
    {
        if (dialogue != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (rowFinished)
                {
                    NextLine();
                }
                else
                {
                    rowFinished = true;
                }
            }
        }
    }

    public void StartDialogue(DialogueContainer dialogue)
    {
        if (this.dialogue != null)
        {
            return;
        }

        InteractManager.instance.HidePrompt();

        ui.SetActive(true);

        DOTween.Kill(cg);
        DOTween.Kill(textBox);

        cg.alpha = 0;
        textBox.sizeDelta = new Vector2();
        cg.DOFade(1, 0.5f);
        textBox.DOSizeDelta(new Vector2(700, 150), 0.5f).SetEase(Ease.OutBack);

        this.dialogue = dialogue;

        PlayerController.instance.Lock();
        CameraController.instance.Lock(dialogue.cameraPoint.position, dialogue.cameraDistance);
        lineIndex = -1;

        NextLine();
    }

    void NextLine()
    {
        lineIndex++;
        rowFinished = false;

        if (lineIndex >= dialogue.lines.Count)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        text.text = dialogue.lines[lineIndex].line;
        nameText.text = dialogue.lines[lineIndex].name;

        int count = text.GetTextInfo(dialogue.lines[lineIndex].line).characterCount;
        float delay = 0.075f;

        for (int c = 0; c < count; c++)
        {
            text.maxVisibleCharacters = c + 1;
            yield return new WaitForSeconds(delay);

            if (rowFinished) { break; }
        }

        text.maxVisibleCharacters = count;

        rowFinished = true;
    }

    void EndDialogue()
    {
        dialogue = null;
        InteractManager.instance.ShowPrompt("Talk");

        cg.DOFade(0, 0.5f).OnComplete(DisableUI);
        textBox.DOSizeDelta(new Vector2(), 0.5f).SetEase(Ease.InBack);

        PlayerController.instance.Unlock();
        CameraController.instance.Unlock();
    }

    void DisableUI()
    {
        ui.SetActive(false);
    }
}
