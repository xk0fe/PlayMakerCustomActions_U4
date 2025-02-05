﻿// (c) copyright Hutong Games, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Inverts the sign of an FSM int variable. If the given int is positive, it gets flipped and becomes negative and vice versa.")]
    public class FsmIntFlip : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmInt)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        [UIHint(UIHint.Variable)]
        public FsmInt StoreResult;

        [Tooltip("Flip FSM value,if true, that will change the FSM value")]
        public FsmBool FlipFsmValue;

        public bool everyFrame;

        int storeFsmValue;

        GameObject goLastFrame;
        PlayMakerFSM fsm;

        public override void Reset()
        {
            FlipFsmValue = false;
            StoreResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            Ggop();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            Ggop();
        }


        void Ggop()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;
            if (go != goLastFrame)
            {
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
                goLastFrame = go;
            }

            if (fsm == null) return;

            FsmInt fsmInt = fsm.FsmVariables.GetFsmInt(variableName.Value);

            if (fsmInt == null) return;

            storeFsmValue = fsmInt.Value;

            if (FlipFsmValue.Value)
            {
                fsmInt.Value = storeFsmValue * (-1);
                StoreResult.Value = fsmInt.Value;
            }

            else
            {
                storeFsmValue = storeFsmValue * (-1);
                StoreResult.Value = storeFsmValue;
            }
        }
    }
}