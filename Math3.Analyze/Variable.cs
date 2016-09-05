using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Math3.Analyze {
	public enum VariableType {
		Numeric, Vector
	}

	public class Variable : Literal {
		public string Name;
		public VariableType Type;
		public object Value;

		public override string AsHtml {
			get {
				return	Greek.ReplaceGreekWithHtmlEntities ( Name );
			}
		}

		public Variable ( string name, VariableType type ) {
			this.Name = name;
			this.Type = type;
		}

		public Variable ( string name, VariableType type, object value ) {
			this.Name = name;
			this.Type = type;
			this.Value = value;
		}

		public override bool IsValueNode {
			get {
				return	Type == VariableType.Vector && Value is double4;
			}
		}

		public override ExpressionType InferredType {
			get {
				ExpressionType et;

				switch ( Type ) {
				case VariableType.Numeric : et = ExpressionType.Numeric; break;
				case VariableType.Vector  : et = ExpressionType.Vector;  break;
				default: et = ExpressionType.Undefined; break;
				}

				return	et;
			}
		}

		public override E Evaluate ( EvalSettings evalSettings = null, bool isRootNode = true ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;

			if ( evalSettings.SubstituteVariables ) {
				if ( Type == VariableType.Numeric ) {
					object val = null;

					if ( Value != null )
						val = Value;

					object settingsVal;

					if ( evalSettings.Values.TryGetValue ( Name, out settingsVal ) )
						val = settingsVal;
					
					if ( val != null && val.IsNumeric () )
						return	SimplifyIfRoot ( E.NumConst ( Convert.ToDouble ( val ) ), evalSettings, isRootNode );
				}
			}

			return	SimplifyIfRoot ( this, evalSettings, isRootNode );
		}

		public override string ToString () {
			return	Name;
		}

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	Name.GetHashCode () ^ Type.GetHashCode () ^ ( Value != null ? Value.GetHashCode () : 0 );
		}

		public override bool Equals ( object obj ) {
			Variable varObj;

			if ( object.ReferenceEquals ( null, varObj = obj as Variable ) )
				return	false;

		    return	this.Name == varObj.Name && this.Type == varObj.Type && this.Value == varObj.Value;
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( Variable e1, Variable e2 ) {
			return	object.ReferenceEquals ( e1, e2 ) || ( !object.ReferenceEquals ( e1, null ) && e1.Equals ( e2 ) );
		}

		public static bool operator != ( Variable e1, Variable e2 ) {
			return	!object.ReferenceEquals ( e1, e2 ) && ( !object.ReferenceEquals ( e1, null ) && !e1.Equals ( e2 ) );
		}
		#endregion Operators
	}
}
