﻿using ao3.lib;
using Spectre.Console;

namespace ao3.client
{
    internal class Symbols
    {
        public static readonly Dictionary<Rating, Text> RatingSymbols = new()
        {
            { Rating.GeneralAudiences, new Text("G") },
            { Rating.TeenAndUpAudiences,new Text("T") },
            { Rating.Mature, new Text("M") },
            { Rating.Explicit, new Text("E") },
            { Rating.NotRated, new Text(" ") },
        };

        public static readonly Dictionary<Category, Text> CategorySymbols = new()
        {
            { Category.FF, new ("♀️") },
            { Category.FM, new Text("⚥") },
            { Category.Gen, new Text("⊙") },
            { Category.MM, new Text("♂️") },
            { Category.Multi, new Text("M") },
            { Category.Other, new Text("♅") },
            { Category.None, new Text(" ") }
        };


        public static readonly Dictionary<CompletionStatus, Text> CompletionDictionary = new()
        {
            { CompletionStatus.All, new Text(" ") },
            { CompletionStatus.Complete, new Text("✓") },
            { CompletionStatus.InProgress, new Text("🛇") }
        };

    }
}
