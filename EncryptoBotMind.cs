using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EncryptoBot.Moves;
using System.Linq;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;

namespace EncryptoBot
{
	public class EncryptoBotMind
	{
		public List<RLBotDotNet.Bot> BotList = new List<RLBotDotNet.Bot>();
		private List<IMoves> MovesDict = new List<IMoves>();

		public EncryptoBotMind()
		{
			FindAllMoves();
		}
		public void UpdatePacket(EncryptoAgent encAgent)
		{
			encAgent.moves[0].Update(encAgent);
			if (encAgent.moves[0].Done)
			{
				encAgent.moves = MovesDict;
				encAgent.moves.ForEach(m => m.Update(encAgent));
				encAgent.moves = encAgent.moves.Where( m => m.Available).ToList();
				encAgent.moves = encAgent.moves.OrderByDescending(m => m.Priority).ToList();
				encAgent.moves[0].Run(encAgent);
			}
		}
		public void FindAllMoves()
		{
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(IMoves)) && !t.IsAbstract)
				.ToList()
				.ForEach(t => {
					IMoves move = (IMoves)Activator.CreateInstance(t);
					move.Initialize(t.Name,MovesDict.Count);
					MovesDict.Add(move);
				});
		}
		public void AddBot(EncryptoAgent encAgent)
		{
			BotList.Add(encAgent);
			encAgent.moves = new List<IMoves>();
			encAgent.moves.Add(new StandStil());
		}
		internal Controller GetMove(int index)
		{
			return new Controller();
		}
	}
}