using Syslib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
	public interface ICardGamesMenuUI
	{

		/// <summary>
		/// 
		///		Welcome
		///		Check ui settings and inta anything
		///		needed to be setup in the ui environment
		///		
		/// </summary>
		public bool Welcome();

		/// <summary>
		/// 
		///		AskMainMenu
		///		Show avaiable choices
		///		and return selected option
		///		
		/// </summary>
		public int AskMainMenu(IForEach<Syslib.ISelectItem> list);


		public void ShowMsg(string msg);
		public void ShowErrMsg(string msg);

		/// <summary>
		/// 
		///		Finish
		///		
		/// </summary>
		void Finish();

	}
}
