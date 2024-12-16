namespace ao3.lib
{
    public enum DownloadType
    {
        AZW3,
        EPUB,
        MOBI,
        PDF,
        HTML,
    }
    public enum CompletionStatus
    {
        All,
        Complete,
        InProgress,
    }

    public enum Crossovers
    {
        Include,
        Exclude,
        Only,
    }

    public enum SortColumn
    {
        BestMatch,
        Author,
        Title,
        DatePosted,
        DateUpdated,
        WordCount,
        Hits,
        Kudos,
        Comments,
        Bookmarks,
    }

    public enum SortDirection
    {
        Descending,
        Ascending,
    }

    public enum Rating
    {
        All = 0,
        NotRated = 9,
        GeneralAudiences = 10,
        TeenAndUpAudiences = 11,
        Mature = 12,
        Explicit = 13,
    }

    public enum Warning
    {
        CreatorChoseNotToUseArchiveWarnings = 14,
        GraphicDepictionsOfViolence = 17,
        NoArchiveWarningsApply = 16,
        MajorCharacterDeath = 18,
    }

    public enum Category
    {
        FF = 116,
        FM = 22,
        Gen = 21,
        MM = 23,
        Multi = 2246,
        Other = 24,
    }
}
