using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveUI : MonoBehaviour
{
    [Header("Tablet")]
    public TextMeshProUGUI tabletText;
    public Image tabletCheck;

    [Header("Fossil")]
    public TextMeshProUGUI fossilText;
    public Image fossilCheck;

    [Header("Meteor")]
    public TextMeshProUGUI meteorText;
    public Image meteorCheck;

    public Color completedColor = Color.gray;

    public void CompleteObjectiveUI(string id)
    {
        switch (id)
        {
            case "Tablet":
                StartCoroutine(
                    AnimateObjective(
                        tabletText,
                        tabletCheck
                    )
                );
                break;

            case "Fossil":
                StartCoroutine(
                    AnimateObjective(
                        fossilText,
                        fossilCheck
                    )
                );
                break;

            case "Meteor":
                StartCoroutine(
                    AnimateObjective(
                        meteorText,
                        meteorCheck
                    )
                );
                break;
        }
    }

    IEnumerator AnimateObjective(
        TextMeshProUGUI text,
        Image check
    )
    {
        float duration = 0.5f;

        Color startColor = text.color;

        float time = 0;

        check.gameObject.SetActive(true);

        while (time < duration)
        {
            time += Time.deltaTime;

            text.color = Color.Lerp(
                startColor,
                completedColor,
                time / duration
            );

            yield return null;
        }

        text.color = completedColor;
    }
}