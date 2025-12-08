using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("UI")]
    public GameObject dialogBox;
    public Text dialogText;
    public float lettersPerSecond = 30f;

    [Header("Talking SFX")]
    public AudioSource audioSource;       // Assign an AudioSource to DialogManager
    public AudioClip defaultTalkSound;    // Default talking sound
    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    private QuestNPC currentNPC;
    private bool showChoicePanel = false;

    private void Awake() => Instance = this;

    public void StartDialog(Dialog dialog, QuestNPC npc, bool showChoice = false)
    {
        currentNPC = npc;
        showChoicePanel = showChoice;
        StartCoroutine(RunDialog(dialog));
    }

    private IEnumerator RunDialog(Dialog dialog)
    {
        dialogBox.SetActive(true);

        // Choose NPC voice if available
        AudioClip talkSound = defaultTalkSound;
        if (currentNPC != null && currentNPC.talkSound != null)
            talkSound = currentNPC.talkSound;

        foreach (string line in dialog.Lines)
        {
            dialogText.text = "";

            // Start looping audio for this line
            if (audioSource != null && talkSound != null)
            {
                audioSource.clip = talkSound;
                audioSource.pitch = Random.Range(pitchMin, pitchMax);
                audioSource.loop = true;
                audioSource.Play();
            }

            foreach (char c in line)
            {
                dialogText.text += c;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }

            // Stop the audio after the line
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }

            // Wait for player to press Space
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        dialogBox.SetActive(false);

        // Quest choice panel
        if (showChoicePanel && currentNPC != null)
        {
            ChoicePanel.Instance.ShowChoices(
                $"Start quest for {currentNPC.questName}?",
                onOk: () => QuestManager.Instance.StartQuest(currentNPC)
            );
        }
    }
}
