using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AIShared
{
	public class App : Application
	{
		public App ()
		{
			//MainPage = new ActivityPage();
			MainPage = new HomePage();

		}

		public static Page GetMainPage()
		{
			//return new ActivityPage();
			return new HomePage();
		}

	}
		
}

