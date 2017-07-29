using System.Collections;
using ParadoxNotion.Design;


namespace FlowCanvas.Nodes{

	[Name("On Disable")]
	[Category("Events/Graph")]
	[Description("Called when the Graph is Disabled")]
	public class DisableEvent : EventNode {

		private FlowOutput disable;

		public override void OnGraphStoped(){
			disable.Call( new Flow(1) );
		}

		protected override void RegisterPorts(){
			disable = AddFlowOutput("Out");
		}
	}
}