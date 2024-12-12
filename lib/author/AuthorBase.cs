namespace ao3.lib.author
{
    public class AuthorBase(string name)
    {
        public string Name { get; } = name;


        public async Task<Author> ToAuthor()
        {
            return await Author.ParseAsync(Name);
        }
    }



}