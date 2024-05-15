namespace ToLearnApi.Models.General;

public class Error
{
    public string Title { get; set; }
    public string Message { get; set; }

    public Error(string title, string message)
    {
        Title = title;
        Message = message;
    }
}
