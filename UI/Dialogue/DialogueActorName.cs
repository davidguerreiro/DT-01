using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActorName : MonoBehaviour {
    public TextComponent displayedName;                         // Actor name text component.
    public FadeElement actorFade;                           // Actor fade component.

    /// <summary>
    /// Display actor name.
    /// </summary>
    /// <param name="name">string - actorName. Empty to keep previous value</param>
    public void Display( string name = "" ) {
        if ( name != "" ) {
            displayedName.UpdateContent(name);
        }

        actorFade.FadeIn();
    }

    /// <summary>
    /// Clear actor name.
    /// </summary>
    /// <param name="removeName">bool - if true, removes the current actor name text value. Default to true.</param>
    public void Clear( bool removeName = true ) {
        StartCoroutine(ClearRoutine(removeName));
    }

    /// <summary>
    /// Clear actor name coroutine.
    /// </summary>
    /// <param name="removeName">bool - if true, removes the current actor name text value.</param>
    /// <return>IEnumerator</return>
    public IEnumerator ClearRoutine( bool removeName ) {
        actorFade.FadeOut();
        yield return new WaitForSeconds(1f);

        if (removeName) {
            displayedName.UpdateContent("");
        }
    }
}
