using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public abstract class Traceable {
		#region Properties
		public Material Material { get; set; }
		public virtual double4x4 ModelMatrix { get; set; }
		public string Name { get; set; }
		public static readonly Dictionary <Type, int> NumCreatedObjects = new Dictionary <Type, int> ();
		#endregion Properties

		#region Constructors
		protected Traceable () {
			this.Material = Material.Default;
			this.ModelMatrix = double4x4.Identity;

			Type thisType = this.GetType ();

			if ( NumCreatedObjects.ContainsKey ( thisType ) ) {
				NumCreatedObjects [thisType]++;
				this.Name = string.Format ( "{0}#{1}", this.GetType ().Name, NumCreatedObjects [thisType] );
			} else {
				NumCreatedObjects [thisType] = 0;
				this.Name = string.Format ( "{0}#0", this.GetType ().Name );
			}
		}
		#endregion Constructors

		#region Methods
		public abstract bool MayIntersect ( Ray r );
		public abstract List <IntersectData> Intersect ( Ray r );
		public abstract double3 GetNormal ( IntersectData data );
		public abstract double2 GetTexCoord ( IntersectData data );
		public abstract double3 GetTangent ( IntersectData data );
		public abstract double3 GetBinormal ( IntersectData data );
		public virtual double3 Advance ( double3 p, double3 l, out double d ) {
			d = Math3.DIFF_THR;

			return	p + l * Math3.DIFF_THR;
		}

		public double3 Advance ( double3 p, double3 l ) {
			double d;

			return	Advance ( p, l, out d );
		}
		#endregion Methods

		#region Overrides
		public override string ToString () {
			return	this.Name;
		}
		#endregion Overrides
	}
}
