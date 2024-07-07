using System.Text.RegularExpressions;
using System.Text;

namespace DiceRollerApp;

internal partial class Program
{
	public static async Task Main(string[] args)
	{
		var _streamWriter = new StreamWriter(Console.OpenStandardOutput(), Encoding.UTF8, 
			sizeof(byte) * 1024 * 1024 * 4);
		await _streamWriter.WriteLineAsync(RollDice("100000000d2"));
	}

	private static string RollDice(string _input)
	{
		var _match = CheckExpression().Match(_input);
		if (!_match.Success)
		{
			return string.Empty;
		}

		var _rollCount = int.Parse(_match.Groups[1].ValueSpan);
		var _dieFaces = int.Parse(_match.Groups[2].ValueSpan);

		var _randomRange = new Random();

		var _nextRoll = _randomRange.Next(1, _dieFaces + 1);

		var _rand = new Random();

		var _stringBuilder = new StringBuilder();

		_stringBuilder.Append(_nextRoll);

		for(int i = 1; i < _rollCount - 1; i++)
		{
			_nextRoll = _rand.Next(1, _dieFaces + 1);
			_stringBuilder.Append($", {_nextRoll}");
		}

		_nextRoll = _randomRange.Next(1, _dieFaces + 1);
		_stringBuilder.Append($" and, {_nextRoll}.");

		return _stringBuilder.ToString();
	}

	[GeneratedRegex("(^[0-9]*)(?:d)([0-9]*$)")]
	private static partial Regex CheckExpression();
}