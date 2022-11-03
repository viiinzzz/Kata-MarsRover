﻿using System.Text.RegularExpressions;

namespace MarsRover;

public enum Direction { N, W, S, E }
public enum Move { L, R,  M }

public record class Rover(Plateau Plateau)
{
    public int PositionX { get; private set; } = 0;
    public int PositionY { get; private set; } = 0;
    public Direction Direction { get; private set; } = Direction.N;

    public Rover(int positionX, int positionY, Direction direction, Plateau plateau)
        : this(plateau) {
        PositionX = positionX;
        PositionY = positionY;
        Direction = direction;
    }

    private static readonly string AllDirections = Enum.GetValues(typeof(Direction))
        .Cast<Direction>().Select(x => $"{x}")
        .Aggregate((x, y) => x + y);

    private static readonly Regex StatusRx = new Regex(@$"(?<PositionX>\d+) (?<PositionY>\d+) (?<Direction>[{AllDirections}])");

    public Rover(string status, Plateau plateau)
        : this(0, 0, Direction.N, plateau)
    {
        var rx = StatusRx.Match(status);
        var parseInt = (string name) => int.Parse(rx.Groups[name].Value);
        var parseDirection = (string name) => Enum.Parse<Direction>(rx.Groups[name].Value);
        (PositionX, PositionY, Direction) = (
            parseInt("PositionX"), parseInt("PositionY"),
            parseDirection("Direction")
        );
    }

    public string Status => $"{PositionX} {PositionY} {Direction}";

    public void Do(string moves)
        => moves.AsEnumerable().ToList().ForEach(
            move => Do(Enum.Parse<Move>($"{move}")));

    public void Do(Move move) => (PositionX, PositionY, Direction) = move switch {
        Move.R => Turn(true),
        Move.L => Turn(false),
        Move.M => Direction switch {
            Direction.N => MoveY(true),
            Direction.S => MoveY(false),
            Direction.E => MoveX(true),
            Direction.W => MoveX(false),
            _ => throw new NotImplementedException()
        },
        _ => throw new NotImplementedException()
    };

    private (int PositionX, int PositionY, Direction Direction) MoveX(bool right) => (
        PositionX + (right ? 1 : -1),
        PositionY,
        Direction
        );

    private (int PositionX, int PositionY, Direction Direction) MoveY(bool up) => (
        PositionX,
        PositionY + (up ? 1 : -1),
        Direction
        );

    private (int PositionX, int PositionY, Direction Direction) Turn(bool clockwise) => (
        PositionX,
        PositionY,
        (Direction)(((int)Direction + (clockwise ? - 1 : 1) + 4) % 4)
        );
}
