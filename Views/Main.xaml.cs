using CommunityToolkit.Maui.Views;
using MovieAppTest.Controllers;
using MovieAppTest.Models;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace MovieAppTest.Views;

public partial class Main : ContentPage
{
    MovieController controller = new MovieController();
    bool IsLoading = false;
    ObservableCollection<MovieListResult> datasources = new ObservableCollection<MovieListResult>();
    int page = 1;
    int current = 1;
    int? genre_id = null;
    string genre_name = string.Empty;

    public Main()
	{
		InitializeComponent();
		LoadGenre();
        LoadMovie();
    }

    async void LoadGenre()
	{
        controller = new MovieController();
        var result = await controller.Genre().ConfigureAwait(true);
		if (result != null && result.Count > 0)
        {
            GenreList.ItemsSource = result;
            GlobalController.Genres = result;
        }
	}

    async void LoadMovie()
    {
        IsLoading = true;
        activity_indicator.IsRunning = IsLoading;
        controller = new MovieController();
        var result = await controller.List(current, genre_id).ConfigureAwait(true);
        if (result != null && result.results.Count > 0)
        {
            if (genre_id != null)
            {
                txt_genre.Text = $"Genre: {genre_name}";
            }
            else
            {
                txt_genre.IsVisible = true;
            }
            txt_result.Text = $"Result: {result.total_results}\nPage: {result.page} of {result.total_pages}";
            page = result.total_pages;
            foreach (var item in result.results)
            {
                datasources.Add(item);
            }
            MoviesList.ItemsSource = datasources;
        }
        IsLoading = false;
        activity_indicator.IsRunning = IsLoading;
    }
    void Genre_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (e.CurrentSelection.FirstOrDefault() != null)
            {
                var item = new MovieGenreList();
                item = e.CurrentSelection.FirstOrDefault() as MovieGenreList;
                genre_id = item.id;
                genre_name = item.name;
                datasources.Clear();
                page = 1;
                current = 1;
                MoviesList.ItemsSource = null;
                LoadMovie();
            }
            MoviesList.SelectedItem = null;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }
    void Movie_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (e.CurrentSelection.FirstOrDefault() != null)
            {
                var item = new MovieListResult();
                item = e.CurrentSelection.FirstOrDefault() as MovieListResult;
                if (item != null)
                {
                    Navigation.PushAsync(new Details(item.id));
                }
            }
            MoviesList.SelectedItem = null;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }
    void Movie_RemainingItemsThresholdReached(object sender, EventArgs e)
    {
        if (IsLoading || datasources.Count == 0)
            return;
        if (current != page)
        {
            current = current + 1;
            LoadMovie();
        }
    }
}