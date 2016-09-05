using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math3d;

namespace RayTrace {
	public class TraceData {
		#region Settings
		public static double3 ReflectionLimitExceedColor = double3.UnitX;
		public static double3 RefractionLimitExceedColor = double3.UnitZ;
		public static int ReflectionLimitExceedCount = 0;
		public static int RefractionLimitExceedCount = 0;
		#endregion Settings

		#region Properties
		public int Reflections;
		public int Refractions;
		public Dictionary <object, object> Properties;
		#endregion Properties

		#region Constructors
		public TraceData ( int reflections, int refractions, Dictionary <object, object> properties ) {
		    this.Reflections = reflections;
		    this.Refractions = refractions;
		    this.Properties = new Dictionary <object, object> ( properties );
		}

		public TraceData ( TraceData traceData, Dictionary <object, object> properties ) {
			this.Reflections = traceData.Reflections;
			this.Refractions = traceData.Refractions;
			this.Properties = traceData.Properties != null && traceData.Properties.Count > 0 ?
				new Dictionary <object, object> ( traceData.Properties ) : new Dictionary <object, object> ();

			foreach ( KeyValuePair <object, object> keyVal in properties )
				this.Properties [keyVal.Key] = keyVal.Value;
		}

		public TraceData ( int reflections, int refractions, params object [] properties ) {
			this.Reflections = reflections;
			this.Refractions = refractions;

			int num = properties.Length / 2;

			for ( int i = 0 ; i < num ; i += 2 )
				Properties [properties [i]] = properties [i + 1];
		}

		public TraceData ( TraceData traceData, params object [] properties ) {
			this.Reflections = traceData.Reflections;
			this.Refractions = traceData.Refractions;
			this.Properties = traceData.Properties != null && traceData.Properties.Count > 0 ?
				new Dictionary <object, object> ( traceData.Properties ) : new Dictionary <object, object> ();

			int num = properties.Length / 2;

			for ( int i = 0 ; i < num ; i += 2 )
				Properties [properties [i]] = properties [i + 1];
		}
		#endregion Constructors

		#region Methods
		public TraceData GetReflected () {
		    return	new TraceData ( this.Reflections - 1, this.Refractions, this.Properties );
		}

		public TraceData GetRefracted () {
		    return	new TraceData ( this.Reflections, this.Refractions - 1, this.Properties );
		}
		#endregion Methods
	}
}
