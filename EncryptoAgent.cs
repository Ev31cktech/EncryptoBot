using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using EncryptoBot.Moves;
using EncryptoBot.States;
using RLBotDotNet;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot
{
	public class EncryptoAgent : RLBotDotNet.Bot
	{
		public int BoostAmount { get { return carObject.Boost; } }
		public Player carObject { get { return packet.Players[Index]; } }
		public Vector3 Location { get { return carObject.Location; } }
		public IState state {get {return botState[0]; } set {botState[0] = value; } }
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
		public Packet packet;

		private EncryptoBotMind BotMind;
		public Vector3 targetLoc = new Vector3();
		public List<IState> botState = new List<IState>(1);
		public EncryptoAgent(string botName, int botTeam, int botIndex) : base("EncryptoBot", botTeam, botIndex)
		{
			botState = new List<IState>();
			botState.Add(null);
			BotMind = EncryptoBotGui.AddBot(this);
		}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			if (packet == null)
				packet = new Packet(gameTickPacket, new FieldInfo(base.GetFieldInfo()));
			packet.updatePacket(gameTickPacket);
			Vector3 carLocation = packet.Players[Index].Location;

			if (packet.GameInfo.IsMatchEnded)
			{
				return new Controller();
			}
			BotMind.UpdatePacket(this);
			Renderer.DrawLine3D(Color.Lime, carLocation, targetLoc);
			return state.moves[0].GetController(this);
			
		}
	}
}
