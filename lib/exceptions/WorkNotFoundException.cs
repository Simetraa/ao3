namespace ao3.lib.exceptions
{
    public class WorkNotFoundException : Exception
    {
        public WorkNotFoundException() : base("Could not find work") { }
    }
}
