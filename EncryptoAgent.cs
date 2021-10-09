using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using EncryptoBot.Moves;
using RLBotDotNet;
using System;
using System.Collections.Generic;
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
		Packet packet;

		private EncryptoBotMind BotMind;
		public Vector3 targetLoc = new Vector3();
		public List<IMoves> moves;


		public EncryptoAgent(string botName, int botTeam, int botIndex) : base("EncryptoBot", botTeam, botIndex)
		{
			BotMind = EncryptoBotGui.AddBot(this);
		}
		public IMoves getActiveMove()
		{
			return moves[0];
		}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			if(packet == null)
				packet = new Packet(gameTickPacket, new FieldInfo(base.GetFieldInfo()));
			packet.updatePacket(gameTickPacket);
			targetLoc = new Vector3(Field.DiamondPath[fieldLocInt], carHeigt);
			Vector3 carLocation = packet.Players[Index].Location;

			if (Vector3.Distance(carLocation, targetLoc) < 100)
			{
				fieldLocInt = ++fieldLocInt % 4;
			}

			if (packet.GameInfo.IsMatchEnded)
			{
				return new Controller();
			}
			Renderer.DrawLine3D(Color.Lime,carLocation,targetLoc);
			BotMind.UpdatePacket(packet, this);
			return moves[0].GetController(this);
		}
	}
}
