// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Interpolates between 2 Int values over a specified Time.")]
	public class IntInterpolate : FsmStateAction
	{
        [Tooltip("Interpolation mode: Linear or EaseInOut.")]
		public InterpolationType mode;
		
        [RequiredField]
        [Tooltip("Interpolate from this value.")]
		public FsmInt fromInt;

		[RequiredField]
		[Tooltip("Interpolate to this value.")]
        public FsmInt toInt;

        [RequiredField]
        [Tooltip("Interpolate over this amount of time in seconds.")]
		public FsmFloat time;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the current value in an Int variable.")]
        public FsmInt storeResult;

        [Tooltip("Event to send when the interpolation is finished.")]
		public FsmEvent finishEvent;

		[Tooltip("Ignore TimeScale. Useful if the game is paused (Time scaled to 0).")]
		public bool realTime;

		private float startTime;
		private float currentTime;
		
		public override void Reset()
		{
			mode = InterpolationType.Linear;
			fromInt = null;
			toInt = null;
			time = 1.0f;
			storeResult = null;
			finishEvent = null;
			realTime = false;
		}

		public override void OnEnter()
		{
			startTime = FsmTime.RealtimeSinceStartup;
			currentTime = 0f;
			
			if (storeResult == null)
			{
			    Finish();
			}
			else
			{
				storeResult.Value = fromInt.Value;
			}
		}
		
		public override void OnUpdate()
		{
			// update time
			
			if (realTime)
			{
				currentTime = FsmTime.RealtimeSinceStartup - startTime;
			}
			else
			{
				currentTime += Time.deltaTime;
			}
			
			var lerpTime = currentTime/time.Value;

			switch (mode) {
			
			case InterpolationType.Linear:
				
				storeResult.Value = (int)Mathf.Lerp(fromInt.Value, toInt.Value, lerpTime);
				
				break;
				
			case InterpolationType.EaseInOut:
				
				storeResult.Value = (int)Mathf.SmoothStep(fromInt.Value, toInt.Value, lerpTime);
				
				break;
			}
			
			if (lerpTime > 1)
			{
				if (finishEvent != null)
				{
				    Fsm.Event(finishEvent);
				}

				Finish();
			}
				
		}
	}
}
