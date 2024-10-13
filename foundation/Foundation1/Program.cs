using System;
using System.Collections.Generic;

// Class to represent a comment
class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// Class to represent a video
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public List<Comment> GetComments()
    {
        return Comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating a list of videos
        List<Video> videos = new List<Video>();

        // Creating videos and adding comments
        Video video1 = new Video("Learning C#", "CodeMaster", 600);
        video1.AddComment(new Comment("John", "Great tutorial!"));
        video1.AddComment(new Comment("Mary", "Very helpful, thank you!"));
        video1.AddComment(new Comment("Peter", "Could you explain more about classes?"));
        videos.Add(video1);

        Video video2 = new Video("Chocolate Cake Recipe", "ChefExpert", 480);
        video2.AddComment(new Comment("Anna", "It turned out delicious!"));
        video2.AddComment(new Comment("Charles", "I'll try to make it this weekend"));
        video2.AddComment(new Comment("Lucy", "What brand of chocolate do you use?"));
        videos.Add(video2);

        Video video3 = new Video("Productivity Tips", "LifeHacker", 720);
        video3.AddComment(new Comment("Frank", "These tips changed my routine!"));
        video3.AddComment(new Comment("Beatrice", "Very good, but you missed talking about organization apps"));
        video3.AddComment(new Comment("Ralph", "Loved it, I want to see more videos like this"));
        videos.Add(video3);

        // Displaying video information
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Duration: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}