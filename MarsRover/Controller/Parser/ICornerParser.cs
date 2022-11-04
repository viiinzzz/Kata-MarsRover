using MarsRover.Controller.Data;

namespace MarsRover.Controller.Parser;

public interface ICornerParser {
    Plateau Parse(string corner);
}