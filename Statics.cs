using System.Numerics;

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
}
