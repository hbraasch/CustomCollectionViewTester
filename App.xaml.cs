using static CustomCollectionViewTester.StartupPageModel;

namespace CustomCollectionViewTester;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new StartupPage<DisplayItem>(new StartupPageModel()));
    }
}
