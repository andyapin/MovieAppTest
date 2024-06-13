using MovieAppTest.Controllers;
using MovieAppTest.Models;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace MovieAppTest.Views;

public partial class Details : ContentPage
{
	int _id = 0;
    MovieController controller = new MovieController();
    bool IsLoading = false;
    ObservableCollection<UserReviewResult> datasources = new ObservableCollection<UserReviewResult>();
    int page = 1;
    int current = 1;
    string homepage_url = "";

    public Details(int id)
	{
		InitializeComponent();
		_id = id;
        LoadData();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (homepage_url == "") return;
        Navigation.PushModalAsync(new Web(homepage_url));
    }

    async void LoadData()
    {
        controller = new MovieController();
        var result = await controller.Detail(_id).ConfigureAwait(true);
        if (result != null)
        {
            img.Source = $"https://image.tmdb.org/t/p/original{result.poster_path}";
            txt_adult.Text = "All Age";
            if (result.adult)
            {
                txt_adult.Text = "Adult +18";
            }
            txt_vote_average.Text = result.vote_average.ToString("0.0");
            txt_vote_count.Text = $"{result.vote_count} users";
            txt_status.Text = result.status;
            txt_title.Text = result.title;
            txt_overview.Text = result.overview;
            txt_release_date.Text = result.release_date;
            homepage_url = result.homepage;

            var tmp = new ObservableCollection<MovieGenreList>();
            foreach (var item in result.genres)
            {
                var toAdd = GlobalController.Genres.FirstOrDefault(x => x.id == item.id);
                if (toAdd != null)
                {
                    tmp.Add(toAdd);
                }
            }
            GenreList.ItemsSource = tmp;
            LoadReview();
        }
    }

    async void LoadReview()
    {
        IsLoading = true;
        activity_indicator.IsRunning = IsLoading;
        controller = new MovieController();
        var result = await controller.ReviewList(_id, current).ConfigureAwait(true);
        if (result != null && result.results.Count > 0)
        {
            page = result.total_pages;
            foreach (var item in result.results)
            {
                datasources.Add(item);
            }
            ReviewsList.ItemsSource = datasources;
        }
        IsLoading = false;
        activity_indicator.IsRunning = IsLoading;
    }

    void Reviews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (e.CurrentSelection.FirstOrDefault() != null)
            {
                var item = new UserReviewResult();
                item = e.CurrentSelection.FirstOrDefault() as UserReviewResult;
                Navigation.PushAsync(new Web(item.url));
            }
            ReviewsList.SelectedItem = null;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }

    }
    void Reviews_RemainingItemsThresholdReached(object sender, EventArgs e)
    {
        if (IsLoading || datasources.Count == 0)
            return;
        if (current != page)
        {
            current = current + 1;
            LoadReview();
        }
    }
}