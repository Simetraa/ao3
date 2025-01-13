namespace ao3.lib.exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException() : base("Could not find author") { }
    }
}
