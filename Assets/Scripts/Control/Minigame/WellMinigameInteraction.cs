using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellMinigameInteraction : Interaction {

    public MiniGameSceneSwitchController mgssc;


    public override void interact() {
        if (!hasInteracted) {
            //There's no prereq, so I'm ignoring that bit.

            indicator.enabled = false;
            feedbackController.showFeedback(successText, gameObject.name, this);
            actionComplete = true;

            if(!delayAction) {
                triggerAction();
            }
        }

    }

    public override void triggerAction() {
        hasInteracted = true;
        mgssc.save = true;
    }
}
