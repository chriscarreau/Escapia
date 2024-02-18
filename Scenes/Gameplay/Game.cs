using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public partial class Game : Node2D
{
	public Events GlobalEvents;
	public State GameState;

	private Label MonthLabel;
	private Label DateLabel;
	private Label WeekdayLabel;
	
	private AnimatedSprite2D SunnyWindow;
	private AnimatedSprite2D RainyWindow;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalEvents = GetNode<Events>("/root/Events");
		GameState = GetNode<State>("/root/State");
		MonthLabel = GetNode<Label>("MonthLbl");
		DateLabel = GetNode<Label>("DateLbl");
		WeekdayLabel = GetNode<Label>("DayLbl");
		
		SunnyWindow = GetNode<AnimatedSprite2D>("Sunny");
		RainyWindow = GetNode<AnimatedSprite2D>("Rainy");

		InitGame();
		UpdateDisplay();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnResetDayClicked() {
		IncrementDay();
		UpdateDisplay();
	}

	public void UpdateDisplay() {

		// Set Calendar labels
		var monthName = new DateTime(2000, GameState.Date.Month, 1)
			.ToString("MMM", CultureInfo.InvariantCulture);
		MonthLabel.Text = monthName;
		DateLabel.Text = GameState.Date.Day.ToString();
		WeekdayLabel.Text = Enum.GetName(GameState.CurrentDay);

		// Set Window
		SunnyWindow.Visible = GameState.CurrentWeather == Weather.Sunny;
		RainyWindow.Visible = GameState.CurrentWeather == Weather.Rainy;
	}
	/// <summary>
	/// Initializes a new GameState
	/// </summary>
	public void InitGame() {
		var rand = new Random();
		var client = GenerateRandomClient();
		GameState = new State{
			// Randomise date for any month, and any date below 22, so that we never during a 7 days game have to switch months
			Date = new MonthDate(rand.Next(1,12), 1),
			CurrentDay = (WeekDays)rand.Next(0,6),
			CurrentWeather = rand.Next(0,2) == 0 ? Weather.Sunny : Weather.Rainy,
			CurrentClient = client,
			CurrentLocationGuess = Vector2.Zero,
		};
	}

	/// <summary>
	/// Increment the dates and set a new client
	/// </summary>
	public void IncrementDay() {
		var rand = new Random();
		var weather = rand.Next(0,2) == 0 ? Weather.Sunny : Weather.Rainy;
		// Increment day with loop
		GameState.CurrentDay = (WeekDays)((int)(GameState.CurrentDay + 1) % 7);
		GameState.Date = new MonthDate(GameState.Date.Month, GameState.Date.Day + 1);
		GameState.CurrentWeather = weather;
	}

	/// <summary>
	/// 
	/// </summary>
	public void EndOfDay() {
		var selectedActivity = GetClosestActivity();
		var score = CalculateScore(selectedActivity);
		var recap = new DayRecap(GameState.CurrentDay, selectedActivity, score, GameState.CurrentClient, GameState.CurrentWeather);
		GameState.Archive.Add(recap);
	}

	/// <summary>
	/// Calculates score based on how many categories matched the selected activitiy
	/// </summary>
	/// <returns>Score</returns>
	public int CalculateScore(Activity selectedActivity) {
		if (GameState.CurrentLocationGuess == Vector2.Zero) {
			return 0;
		}

		// find out how many match between activity and clients likes
		var categoriesMatched = 0;
		foreach (var like in GameState.CurrentClient.Likes){
			if (selectedActivity.Categories.Contains(like)) {
				categoriesMatched += 1;
			}
		}

        int score;
		// Give score based on how many matches
        switch (categoriesMatched){
			case 1:
				score = 1;
				break;
			case 2:
				score = 4;
				break;
			case 3:
				score = 10;
				break;
			default:
				score = 0;
				break;

		}
		return score;
	}

	/// <summary>
	/// Returns the activity in the list of all activities that's located closest to location guess
	/// </summary>
	/// <returns></returns>
	public Activity GetClosestActivity() {
		var activitiesCopy = new List<Activity>(GameState.Activities);
		activitiesCopy.Sort((a, b) => a.Location.DistanceTo(GameState.CurrentLocationGuess) < b.Location.DistanceTo(GameState.CurrentLocationGuess) ? -1 : 1);
		return activitiesCopy.First();
	}

	public Client GenerateRandomClient() {
		var rand = new Random();
		var nbOfCategories = Enum.GetValues(typeof(Category)).Length;
		var likes = new List<Category>();
		for (int i = 0; i < 3; i++)
		{
			likes.Add((Category)rand.Next(0, nbOfCategories));
		}
		return new Client{
			Likes = likes,
		};
	}
}
