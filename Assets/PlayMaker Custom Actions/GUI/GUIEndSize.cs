// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":["Assets/PlayMaker Custom Actions/GUI/Internal/GUISizer.cs"]
}
EcoMetaEnd
---*/

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI End Size.")]
	public class GUIEndSize : FsmStateAction
	{
		public override void OnGUI()
		{
			base.OnGUI();

            GUISizer.EndGUI();
		}
	}
}
