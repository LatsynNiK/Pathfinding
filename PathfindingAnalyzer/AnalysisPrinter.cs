using System.IO;
using Common;
using MazeGenerator;
using Pathfinding;

namespace PathfindingAnalyzer
{
    public class AnalysisPrinter
    {
        private TextWriter _textWriter;

        public TextWriter Print(AnalysisParameter parameter, AnalysisResult result)
        {
            _textWriter = new StringWriter();
            PrintParameter(parameter);
            PrintExample(parameter);
            PrintResult(result);
            
            _textWriter.Close();
            return _textWriter;
        }

        private void PrintParameter(AnalysisParameter parameter)
        {
            _textWriter.WriteLine($"\t\t\t*** {parameter.Name} Report ***\t\t\t");
            _textWriter.WriteLine("Parameters:");
            _textWriter.WriteLine($"\tNumber of mazes for each experiment: {parameter.NumberOfMazes}");
            _textWriter.WriteLine($"\tPerfect mazes: {parameter.IsPerfectMaze}");
            if (!parameter.IsPerfectMaze)
            {
                _textWriter.WriteLine($"\tPercent of walls: {parameter.PercentOfWalls}");
            }
            _textWriter.WriteLine($"\tStart position: {parameter.StartPosition}");
            _textWriter.WriteLine();
        }

        private void PrintResult(AnalysisResult result)
        {
            _textWriter.WriteLine("Result table");
            foreach (var pathFinder in result.PathfindingStatistics.Keys)
            {
                _textWriter.WriteLine($"Pathfinder: {pathFinder.GetType()}");
                _textWriter.WriteLine("\tMaze size\t\tAverage Time [ms]\t\tAverage Time [ticks = 100*ns]");
                foreach (var line in result.PathfindingStatistics[pathFinder])
                {
                    _textWriter.WriteLine($"\t{line.Key}\t\t\t{line.Value.Milliseconds}\t\t\t{line.Value.Ticks}");
                }
            }
            _textWriter.WriteLine();
        }

        private void PrintExample(AnalysisParameter parameter)
        {
            _textWriter.WriteLine("Example maze:");
            _textWriter.WriteLine();
            var mazeGenerator = new EllerMazeGenerator();
            var maze = mazeGenerator.Generate(new MazeGeneratorOptions()
            {
                Height = 100,
                Width = 100,
                PercentOfWalls = parameter.PercentOfWalls,
                IsPerfectMaze = parameter.IsPerfectMaze,
                StartPosition = parameter.StartPosition
            });

            var leePathFinder = new Pathfinding.LeePathFinder();
            var leePath = leePathFinder.FindPath(maze);

            var aStarPathFinder = new AStarPathFinder();
            var aStarPath = aStarPathFinder.FindPath(maze);

            var mazePrinter = new MazePrinter();
            mazePrinter.AddMazeLayer(maze)
                .AddPathLayer(leePath, "o ")
                .AddPathLayer(aStarPath, "x ")
                .AddStartAndFinish(maze.Start, maze.Finish)
                .Print(_textWriter);
            _textWriter.WriteLine();
            _textWriter.WriteLine();
        }
    }
}
