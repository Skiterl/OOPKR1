namespace OOPKR1.Entities
{
    public class Article: IComparable<Article>, IComparer<Article>
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }

        public Article()
        {
            Author = new Person("");
            Title = "";
            Rating = 0;
        }

        public override string ToString()
        {
            return Author.Surname + " " + Title + " " + Rating;
        }

        public int CompareTo(Article? other)
        {
            return Title.CompareTo(other.Title);
        }

        public int Compare(Article? x, Article? y)
        {
            return x.Author.Surname.CompareTo(y.Author.Surname);
        }
    }

    public class ArticleComparer : IComparer<Article>
    {
        public int Compare(Article? x, Article? y)
        {
            return x.Rating.CompareTo(y.Rating);
        }
    }
}
