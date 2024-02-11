
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// Global game state
/// </summary>
public partial class State : Node
{

    public List<Activity> Activities = new List<Activity>();

    public Weather CurrentWeather;

    public MonthDate Date;

    public WeekDays CurrentDay;

    public int Score;

    public Client CurrentClient;

    public Vector2 CurrentLocationGuess;

    public List<DayRecap> Archive = new List<DayRecap>();
}