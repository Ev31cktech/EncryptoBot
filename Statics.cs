using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EncryptoBot
{
	static class Field
	{
		private const int inset = 300;
		public static readonly Vector2[] DiamondPath = {
			new Vector2( 4096 - inset,  5120 - (1152 + inset)),
			new Vector2(-4096 + (1152 + inset),  5120 - inset),
			new Vector2(-4096 + inset, -5120 + (1152 + inset)),
			new Vector2( 4096 - (1152 + inset), -5120 + inset),
			};
	}
	static class Vectors
	{
		public static Vector2 Vector2Max = new Vector2(1,1);
		public static Vector2 Vector2Min = new Vector2(-1,-1);
		public static Vector3 Vector3Max = new Vector3(1,1,1);
		public static Vector3 Vector3Min = new Vector3(-1,-1,-1);
	}
}
