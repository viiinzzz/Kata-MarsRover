﻿using MarsRover.Controller.Parser;
using MarsRover.Models;

namespace MarsRover.Controller;

public record class Fleet(IPositionMaster PositionMaster) : IRoverIdentifier
{
    private readonly List<Rover.RoverUnit> Rovers = new();

    private Rover.RoverUnit Add(Rover.RoverUnit rover)
    {
        Rovers.Add(rover);
        return rover;
    }

    private int RoverId = 0;

    public int GetRoverId() => RoverId++;

    public override string ToString() => Rovers.Count == 0 ? ""
        : Rovers.Select(rover => $"{rover.Status}")
            .Aggregate((x, y) => x + '\n' + y);

    public Rover.RoverUnit AddRover(int PositionX, int PositionY, DirectionEnum Direction)
        => Add(new Rover.RoverUnit(PositionX, PositionY, Direction, PositionMaster, GetRoverId()));

    public Rover.RoverUnit AddRover(string status)
        => Add(new Rover.RoverUnit(status, PositionMaster, GetRoverId()));

    public Rover.RoverUnit TryAddRover(Discardable status, Discardable moves)
    {
        Rover.RoverUnit rover = null;

        status.Try(status =>
        {
            rover = new Rover.RoverUnit(status, PositionMaster, GetRoverId());
            if (rover == null) throw new Exception();
        });

        moves.Try(moves =>
        {
            Add(rover.Run(moves));
        });

        return rover;
    }
}