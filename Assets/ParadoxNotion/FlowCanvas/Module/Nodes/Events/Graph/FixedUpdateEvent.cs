﻿using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion.Services;

namespace FlowCanvas.Nodes{

	[Name("On Fixed Update")]
	[Category("Events/Graph")]
	[Description("Called every fixed framerate frame, which should be used when dealing with Physics")]
	public class FixedUpdateEvent : EventNode {

		private FlowOutput fixedUpdate;

		protected override void RegisterPorts(){
			fixedUpdate = AddFlowOutput("Out");
		}

		public override void OnGraphStarted(){
			MonoManager.current.onFixedUpdate += this.FixedUpdate;
		}

		public override void OnGraphStoped(){
			MonoManager.current.onFixedUpdate -= this.FixedUpdate;
		}

		void FixedUpdate(){
			fixedUpdate.Call(new Flow(1));
		}
	}
}