namespace MovieAppTest.Components;

public partial class NavigationBar : ContentView
{
    public static readonly BindableProperty TextTitleProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(NavigationBar), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextTitleProperty);
        set => SetValue(TextTitleProperty, value);
    }

    public NavigationBar()
	{
		InitializeComponent();
	}

    private async void nav_bar_tapped(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PopAsync().ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}