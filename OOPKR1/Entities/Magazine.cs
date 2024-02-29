using OOPKR1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OOPKR1.Entities
{
    public delegate TKey KeySelector<TKey>(Magazine mg);
    public class Magazine
    {

        private string _title;
        private Frequency _frequency;
        private DateTime _releaseDate;
        private int _count;
        private Article[] _articles;

        public List<Person> Editors { get; set; }

        public Magazine(string title, Frequency frequency, DateTime releaseDate, int count, Article[] articles, List<Person> editors)
        {
            _title = title;
            _frequency = frequency;
            _releaseDate = releaseDate;
            _count = count;
            _articles = articles;
            Editors = editors;
        }

        public Magazine()
        {
            _title = "";
            _frequency = new Frequency();
            _releaseDate = DateTime.Now;
            _count = 0;
            _articles = new Article[0];
            Editors = new List<Person>();
        }

        public string Title { get => _title; set => _title = value; }
        public Frequency Frequency { get => _frequency; set => _frequency = value; }
        public DateTime ReleaseDate { get => _releaseDate; set => _releaseDate = value; }
        public int Count { get => _count; set => _count = value; }
        public List<Article> Articles { get => _articles.ToList(); set => _articles = value.ToArray(); }

        public double AverageRating => Articles.Average(x => x.Rating);

        public bool this[Frequency frequency] => frequency == Frequency;

        public void AddArticles(params Article[] articles)
        {
            Articles = (List<Article>)Articles.Concat(articles);
        }

        public override string ToString()
        {
            string articles = string.Join(", ", _articles.Select(a => $"{a.Title} {a.Rating}"));
            return Title + "\n" + Frequency + "\n" + ReleaseDate + "\n" + Count + "\n" + articles + "\n";
        }

        public virtual string ToShortString()
        {
            return Title + "\n" + Frequency + "\n" + ReleaseDate + "\n" + Count + "\n" + AverageRating + "\n";
        }

        public void SortByTitle()
        {
            //Articles.Sort((a1, a2) => string.Compare(a1.Title, a2.Title, StringComparison.Ordinal));
            //_articles = Articles.ToArray();
            Array.Sort(_articles, (a1, a2) => string.Compare(a1.Title, a2.Title, StringComparison.Ordinal));
        }

        public void SortByAuthor()
        {
            //Articles.Sort((a1, a2) => string.Compare(a1.Author.Surname, a2.Author.Surname, StringComparison.Ordinal));
            //_articles = Articles.ToArray();
            Array.Sort(_articles, (a1, a2) => string.Compare(a1.Author.Surname, a2.Author.Surname, StringComparison.Ordinal));
        }

        public void SortByRating()
        {
            //Articles.Sort((a1, a2) => a1.Rating.CompareTo(a2.Rating));
            //_articles = [.. Articles];
            Array.Sort(_articles, (a1, a2) => a1.Rating.CompareTo(a2.Rating));
        }
    }

    public class MagazineCollection<TKey>
    {
        public Dictionary<TKey, Magazine> Magazines { get; set; }
        private KeySelector<TKey> keySelector;

        public MagazineCollection(KeySelector<TKey> keySelector)
        {
            this.keySelector = keySelector;
            Magazines = new Dictionary<TKey, Magazine>();
        }

        public void AddDefaults()
        {
            Person p1 = new Person("Alex");
            Person p2 = new Person("Bob");
            AddMagazines(
                new Magazine("Sport", Frequency.Monthly, DateTime.Now, 700, [new Article(p1, "Chess", 5), new Article(p2, "Diving", 3.7)], new List<Person>() { new Person("tk")}), 
                new Magazine("Business", Frequency.Weekly, DateTime.Now, 400, [new Article(p1, "News", 3.9), new Article(p2, "gh", 4.9)], new List<Person>() { new Person("gh") }),
                new Magazine("Science", Frequency.Monthly, DateTime.Now, 100, [new Article(p1, "Phisics", 3.9), new Article(p2, "Math", 4.9)], new List<Person>() { new Person("bcv") }));
        }

        public void AddMagazines(params Magazine[] newMagazines)
        {
            foreach (var magazine in newMagazines)
            {
                TKey key = keySelector(magazine);
                Magazines.Add(key, magazine);
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Magazines.Values.Select(m =>
            {
                string editors = string.Join(", ", m.Editors);
                string articles = string.Join(", ", m.Articles.Select(a => $"{a.Title} {a.Rating}"));
                return $"Title: {m.Title}, Editors: {editors}, Articles: {articles}";
            }));
        }

        public string ToShortString()
        {
            return "";
        }

        public double MaxAvarageRating => Magazines.Values.Any() ? Magazines.Values.Max
            (m => m.Articles.Any() ? m.Articles.Average(a => a.Rating) : 0) : 0;

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency f) => 
            Magazines.Where(p => p.Value.Articles.Any() && p.Value.Frequency == f);

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> GroupByFrequency => Magazines.GroupBy(p => p.Value.Frequency);
    }
}
