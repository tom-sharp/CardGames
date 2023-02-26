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
		///		ShowProgress
		///		Progressindicator
		///		
		/// </summary>
		public void ShowProgress(double progress, double complete);


		/// <summary>
		/// 
		///		AskMainMenu
		///		Show avaiable choices
		///		and return selected option
		///		
		/// </summary>
		public int AskMainMenu(IForEach<Syslib.ISelectItem> list);

		/// <summary>
		/// 
		///		ShowMsg
		///		Display message
		///		
		/// </summary>
		public void ShowMsg(string msg);

		/// <summary>
		/// 
		///		ShowErrMsg
		///		Display an error mesasge
		///		
		/// </summary>
		public void ShowErrMsg(string msg);

		/// <summary>
		/// 
		///		Finish
		///		
		/// </summary>
		void Finish();

	}
}
