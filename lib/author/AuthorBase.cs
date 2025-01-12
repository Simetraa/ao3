namespace ao3.lib.author
{
    public class AuthorBase(string name)
    {
        public string Name { get; private set; } = name;


        public async Task<Author> ToAuthor()
        {
            return await Author.ParseFromName(Name);
        }
    }



}