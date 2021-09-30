using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot
{
	public class EncryptoAgent : RLBotDotNet.Bot
	{
		/// Customization<summary>
		/// Body:	Breakout Type-S
		/// Livery:	Encryption
		/// Hat:	Top-Hat
		/// Wheels:	Stella inverted
		/// Trail:	Binary
		/// 
		/// </summary>
		int fieldLocInt = 0;
		float carHeigt = 0;
		//readonly LookAheadDist = 
		Packet packet;
		EncryptoBotMind BotMind;

		public EncryptoAgent(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
		{
			EncryptoBotGui.AddBot(this);
		}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			packet = new Packet(gameTickPacket);
			if (packet.GameInfo.IsMatchEnded)
			{
				return new Controller();
			}
			BotMind.UpdatePacket(packet);
			return BotMind.GetMove(Index);
			
		}

	}
}
