using UnityEngine;
using ParadoxNotion;
using ParadoxNotion.Design;
using System.Collections;
using System.Reflection;
using System;

namespace FlowCanvas.Nodes{

	[DoNotList]
	[Description("*IMPORTANT* Please note that accessing fields per-frame in FlowCanvas, may impact performance and should best be avoided. Please use properties whenever possible instead.")]
	public class ReflectedFieldNodeWrapper : FlowNode {

		public enum AccessMode{
			GetField,
			SetField
		}

		[SerializeField]
		private string fieldName;
		[SerializeField]
		private Type targetType;
		[SerializeField]
		private AccessMode accessMode;

		private FieldInfo _field;

		private FieldInfo field{
			get
			{
				if (_field != null){ return _field; }
				return _field = targetType != null? targetType.GetField(fieldName) : null;
			}
		}

		public override string name {
			get
			{
				if (field != null){ return string.Format("{0}.{1}", field.DeclaringType.Name, field.Name); }
				return string.Format("* Missing '{0}.{1}' *", targetType != null? targetType.Name : "null", fieldName);
			}
		}

		public void SetField(FieldInfo newField, AccessMode mode, object instance = null){
			
			if (newField == null){
				return;
			}

			fieldName = newField.Name;
			targetType = newField.DeclaringType;
			accessMode = mode;
			GatherPorts();

			if (instance != null && !newField.IsStatic){
				var port = (ValueInput)GetFirstInputOfType(instance.GetType());
				if (port != null){
					port.serializedValue = instance;
				}			
			}
		}

		protected override void RegisterPorts(){

			if (field == null){
				return;
			}

			if ( field.IsReadOnly() && field.IsStatic ){
				var constantValue = field.GetValue(null);
				AddValueOutput("Value", field.FieldType, ()=>{ return constantValue; });
				return;
			}

			if (accessMode == AccessMode.GetField){
				var instanceInput = AddValueInput(targetType.FriendlyName(), targetType);
				AddValueOutput("Value", field.FieldType, ()=>{ return field.GetValue(instanceInput.value); });			

			} else {

				object instance = null;
				var instanceInput = AddValueInput(targetType.FriendlyName(), targetType);
				var valueInput = AddValueInput("Value", field.FieldType);
				var flowOut = AddFlowOutput(" ");
				AddValueOutput(targetType.FriendlyName(), targetType, ()=>{ return instance; });
				AddFlowInput(" ", (f)=> { instance = instanceInput.value; field.SetValue(instance, valueInput.value); flowOut.Call(f); });
			}
		}
	}
}