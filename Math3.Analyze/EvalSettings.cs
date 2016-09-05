using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math3.Analyze {
	[Flags]
	public enum EvalFlags {
		EvalFuncs = 1,
		ReduceZeroMultiplier = 2,
		ReduceUnitMultiplier = 4,
		ReduceUnitDivider = 8,
		GroupByCommonFactor = 16,
		SimplifyTrigonometricFunctions = 32,
		GroupMultipliersToFractions = 64,
		SumFractions = 128,
		SubstituteVariables = 256,
		All = EvalFuncs |
			ReduceZeroMultiplier | ReduceUnitMultiplier | ReduceUnitDivider |
			GroupByCommonFactor | SimplifyTrigonometricFunctions | 
			GroupMultipliersToFractions | SumFractions | SubstituteVariables
	}

	public class EvalSettings {
		public const int INFINITE_LOOP_CYCLE_NUM = 1000;
		public EvalFlags Flags = EvalFlags.All & ( ~EvalFlags.SubstituteVariables );

		public bool EvalFuncs {
			get { return	Flags.HasFlag ( EvalFlags.EvalFuncs ); }
			set { Flags = Flags.SetFlag ( EvalFlags.EvalFuncs, value ); }
		}

		public bool ReduceZeroMultiplier {
			get { return	Flags.HasFlag ( EvalFlags.ReduceZeroMultiplier ); }
			set { Flags = Flags.SetFlag ( EvalFlags.ReduceZeroMultiplier, value ); }
		}

		public bool ReduceUnitMultiplier {
			get { return	Flags.HasFlag ( EvalFlags.ReduceUnitMultiplier ); }
			set { Flags = Flags.SetFlag ( EvalFlags.ReduceUnitMultiplier, value ); }
		}

		public bool ReduceUnitDivider {
			get { return	Flags.HasFlag ( EvalFlags.ReduceUnitDivider ); }
			set { Flags = Flags.SetFlag ( EvalFlags.ReduceUnitDivider, value ); }
		}

		public bool GroupByCommonFactor {
			get { return	Flags.HasFlag ( EvalFlags.GroupByCommonFactor ); }
			set { Flags = Flags.SetFlag ( EvalFlags.GroupByCommonFactor, value ); }
		}

		public bool SimplifyTrigonometricFunctions {
			get { return	Flags.HasFlag ( EvalFlags.SimplifyTrigonometricFunctions ); }
			set { Flags = Flags.SetFlag ( EvalFlags.SimplifyTrigonometricFunctions, value ); }
		}

		public bool GroupMultipliersToFractions {
			get { return	Flags.HasFlag ( EvalFlags.GroupMultipliersToFractions ); }
			set { Flags = Flags.SetFlag ( EvalFlags.GroupMultipliersToFractions, value ); }
		}

		public bool SumFractions {
			get { return	Flags.HasFlag ( EvalFlags.SumFractions ); }
			set { Flags = Flags.SetFlag ( EvalFlags.SumFractions, value ); }
		}

		public bool SubstituteVariables {
			get { return	Flags.HasFlag ( EvalFlags.SubstituteVariables ); }
			set { Flags = Flags.SetFlag ( EvalFlags.SubstituteVariables, value ); }
		}

		public bool MaximumSimplicity = true;
		public Dictionary <string, object> Values = new Dictionary <string, object> ();

		public EvalSettings ( EvalFlags flags ) {
			this.Flags = flags;
		}

		public EvalSettings ( EvalFlags flags, params object [] varValues ) : this ( varValues ) {
			this.Flags = flags;
		}

		public EvalSettings ( bool substituteVariables = false ) {
			SubstituteVariables = substituteVariables;
		}

		public EvalSettings ( params object [] varValues ) : this ( true ) {
			int numPairs = varValues.Length - varValues.Length % 2;

			for ( int i = 0 ; i < numPairs ; i += 2 )
				Values.Add ( ( string ) varValues [i], varValues [i + 1] );
		}
	}
}
