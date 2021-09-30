using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EncryptoBot.Moves;
using System.Linq;

namespace EncryptoBot
{
	public class EncryptoBotMind
	{
		public List<RLBotDotNet.Bot> BotList = new List<RLBotDotNet.Bot>();
		public List<IMoves> MovesDict = new List<IMoves>();
		public EncryptoBotMind()
		{
			FindAllMoves();
		}
		public void FindAllMoves()
		{
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(IMoves)) && !t.IsAbstract)
				.ToList()
				.ForEach(t => MovesDict.Add((IMoves)Activator.CreateInstance(t)));
		}
	}
}