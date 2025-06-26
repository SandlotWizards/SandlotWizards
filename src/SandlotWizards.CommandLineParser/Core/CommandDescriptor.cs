﻿using SandlotWizards.CommandLineParser.Core;

public class CommandDescriptor
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required ICommand Handler { get; init; }

    public List<string> Aliases { get; init; } = new();
    public List<string> Examples { get; init; } = new();
    public string WhyExplanation { get; init; } = string.Empty;
    public string HelpText { get; init; } = string.Empty;
    public string Group { get; init; } = "General"; // ⬅ NEW

    public bool Matches(string input)
    {
        return string.Equals(Name, input, StringComparison.OrdinalIgnoreCase)
            || Aliases.Any(a => string.Equals(a, input, StringComparison.OrdinalIgnoreCase));
    }
}
