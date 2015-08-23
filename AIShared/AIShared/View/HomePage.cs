using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AIShared
{
	public class HomePage : ContentPage
	{
		//for the button click counter
		private int _clickCount;

		public HomePage ()
		{
			// setup your ViewModel
			ViewModel = new HomePageViewModel
			{
				ButtonText = "Click Me!"
			};
			// Set the binding context to the newly created ViewModel
			BindingContext = ViewModel;

			// the button is what we're going to use to trigger a long running Async task
			// we're also going to bind the button text so that we can see the binding in action
			var actionButton = new Button();
			actionButton.SetBinding(Button.TextProperty, "ButtonText");
			actionButton.Clicked += async (sender, args) => await SomeLongRunningTaskAsync();

			// here's your activity indicator, it's bound to the IsBusy property of the BaseViewModel
			// those bindings are on both the visibility property as well as the IsRunning property
			var activityIndicator = new ActivityIndicator
			{
				Color = Color.Black,
			};
			activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
			activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

			// return the layout that includes all the above elements
			Content = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.White,
				Children = {actionButton, activityIndicator}
			};
		}

		// you need a view model to bind to
		private HomePageViewModel ViewModel { get; set; }

		private async Task SomeLongRunningTaskAsync()
		{
			// here's your long running task.
			// set to busy at the start
			ViewModel.IsBusy = true;

			// run your task... and MAKE SURE it's a Task, and NOT on the UI Thread
			await Task.Delay(2500)
				.ContinueWith(task => { _clickCount ++; });

			// finish updating if necessary
			ViewModel.ButtonText = string.Format("I've been clicked {0} times!", _clickCount);

			// make sure you switch the busy indicator back.
			ViewModel.IsBusy = false;
		}

	}
		
}

