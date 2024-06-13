using Java.Net;

namespace MovieAppTest.Views;

public partial class Web : ContentPage
{
	public Web(string url)
	{
		InitializeComponent();
        src.Source = url;
    }
}