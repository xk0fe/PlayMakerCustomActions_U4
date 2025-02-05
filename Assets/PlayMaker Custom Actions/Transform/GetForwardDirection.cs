﻿// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Transforms a Direction from a Game Object's local space to world space.")]
    public class GetForwardDirection : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmVector3 forwardDirection;
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
         
            forwardDirection = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoTransformDirection();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoTransformDirection();
        }

        void DoTransformDirection()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            forwardDirection.Value = go.transform.forward;
        }
    }
}

