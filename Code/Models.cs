
using Godot;

public record DayRecap(WeekDays Day, Activity Activity, int Score, Client Client, Weather Weather);


public record MonthDate(int Month, int Day);
