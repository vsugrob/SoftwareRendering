using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace Common3d {
	public class Camera {
		#region Properties
		double3 right = double3.UnitX,
				up    = double3.UnitY,				
				view  = double3.UnitZ;
		double4 pos   = double4.UnitW;
		double fovX = 100, fovY = 100;
		double4x4 viewMatrix = double4x4.Identity;
		double4x4 viewInvMatrix = double4x4.Identity;

		public double3 Right {
			get { return	right; }
			set {
				if ( right == value )
					return;

				right = value.Normalized;
				view = right * up;
				up = view * right;
				BuildMatrices ();
			}
		}

		public double3 Up {
			get { return	up; }
			set {
				if ( up == value )
					return;

				up = value.Normalized;
				view = right * up;
				right = up * view;
				BuildMatrices ();
			}
		}

		public double3 View {
			get { return	view; }
			set {
				if ( view == value )
					return;

				view = value.Normalized;
				right = up * view;
				up = view * right;
				BuildMatrices ();
			}
		}

		public double4 Pos {
			get { return	pos; }
			set {
				double4 newPos = value;
				newPos.w = 1;

				if ( pos == newPos )
					return;

				pos = newPos;
				BuildMatrices ();
			}
		}

		public double FovX {
			get { return	fovX; }
			set { fovX = value; }
		}

		public double FovY {
			get { return	fovY; }
			set { fovY = value; }
		}

		public double4x4 ViewMatrix {
			get { return	viewMatrix; }
		}

		public double4x4 ViewInvMatrix {
			get { return	viewInvMatrix; }
		}
		#endregion Properties

		#region Constructors
		public Camera () {}
		public Camera ( double3 pos ) {
			this.Pos = new double4 ( pos, 1 );
		}

		public Camera ( double3 pos, double3 view, double3 up ) {
			this.view  = view;
			this.up    = up;
			this.right = up * view;
			this.pos   = new double4 ( pos, 1 );
			BuildMatrices ();
		}
		#endregion Constructors

		#region Methods
		void BuildMatrices () {
			viewMatrix = double4x4.Frame ( right, up, view, pos );
			viewInvMatrix = double4x4.FrameInv ( right, up, view, pos );
		}

		public void Transform ( double4x4 m ) {
			right = m.Transform ( right );
			up    = m.Transform ( up );
			view  = m.Transform ( view );
			pos   = m.Transform ( pos );
			BuildMatrices ();
		}
		#endregion Methods

		#region Overrides
		public override string ToString () {
			return	string.Format ( @"Fov: ({0}, {1}), View: {{{2}}}, Up: {{{3}}}, Right: {{{4}}}",
				fovX, fovY, View, Up, Right );
		}
		#endregion Overrides
	}
}
