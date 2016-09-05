using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Math3.Analyze {
	public class PropertyAccess : E {
		public E Object;
		public string Property;

		public override string AsHtml {
			get {
				string html = @"<table cellspacing=0 style=""display : inline;""><tr>";

				if ( !( Object is Literal ) )
					html += string.Format ( "<td>(</td><td>{0}</td><td>)</td><td>.</td><td>{1}</td>", Object.AsHtml, Property );
				else
					html += string.Format ( "<td>{0}</td><td>.</td><td>{1}</td>", Object.AsHtml, Property );

				html += "</tr></table>";

				return	html;
			}
		}

		public PropertyAccess ( E obj, string prop ) {
			this.Object = obj;
			this.Property = prop;
		}

		public override ExpressionType InferredType {
			get {
				if ( Object.InferredType == ExpressionType.Vector &&
					( Property == "x" || Property == "y" || Property == "z" || Property == "w" ) )
				{
					return	ExpressionType.Numeric;
				} else
					return	ExpressionType.Undefined;
			}
		}

		public override E Evaluate ( EvalSettings evalSettings = null, bool isRootNode = true ) {
			evalSettings = evalSettings ?? E.DefaultEvalSettings;
			E evaluatedObject = Object.Evaluate ( evalSettings, false );

			if ( evaluatedObject is Variable ) {
				Variable var = evaluatedObject as Variable;
				
				if ( var.IsValueNode ) {
					double4 v = ( double4 ) var.Value;
					
					if ( Property == "x" )
						return	SimplifyIfRoot ( E.NumConst ( v.x ), evalSettings, isRootNode );
					else if ( Property == "y" )
						return	SimplifyIfRoot ( E.NumConst ( v.y ), evalSettings, isRootNode );
					else if ( Property == "z" )
						return	SimplifyIfRoot ( E.NumConst ( v.z ), evalSettings, isRootNode );
					else if ( Property == "w" )
						return	SimplifyIfRoot ( E.NumConst ( v.w ), evalSettings, isRootNode );
				}
			}

			return	SimplifyIfRoot ( evaluatedObject.Prop ( Property ), evalSettings, isRootNode );
		}

		public override string ToString () {
			if ( !( Object is Literal ) )
				return	string.Format ( "( {0} ).{1}", Object.ToString (), Property );
			else
				return	string.Format ( "{0}.{1}", Object.ToString (), Property );
		}

		#region Mandatory Overrides
		public override int GetHashCode () {
			return	Object.GetHashCode () ^ Property.GetHashCode ();
		}

		public override bool Equals ( object obj ) {
			PropertyAccess propAccessObj;

			if ( object.ReferenceEquals ( null, propAccessObj = obj as PropertyAccess ) )
				return	false;

		    return	this.Object == propAccessObj.Object && this.Property == propAccessObj.Property;
		}
		#endregion Mandatory Overrides

		#region Operators
		public static bool operator == ( PropertyAccess e1, PropertyAccess e2 ) {
			return	object.ReferenceEquals ( e1, e2 ) || ( !object.ReferenceEquals ( e1, null ) && e1.Equals ( e2 ) );
		}

		public static bool operator != ( PropertyAccess e1, PropertyAccess e2 ) {
			return	!object.ReferenceEquals ( e1, e2 ) && ( !object.ReferenceEquals ( e1, null ) && !e1.Equals ( e2 ) );
		}
		#endregion Operators
	}
}
