using OOPKR1;
using OOPKR1.Entities;
using OOPKR1.Enums;


Person p1 = new Person("A");
Person p2 = new Person("C");
Person p3 = new Person("B");
Magazine magazine = new Magazine("News", Frequency.Daily, DateTime.Now, 5000, [new Article(p1, "abcs", 3.5), new Article(p2, "clskdjf", 2.9), new Article(p3, "bxclkj", 3.1)], new List<Person>() { new Person("nlkj") });
Console.WriteLine(magazine);
magazine.SortByAuthor();
Console.WriteLine(magazine);
magazine.SortByRating();
Console.WriteLine(magazine);
magazine.SortByTitle();
Console.WriteLine(magazine);

MagazineCollection<string> col = new MagazineCollection<string>(m => m.Title);
col.AddDefaults();
col.AddMagazines(magazine);

Console.WriteLine(col);
Console.WriteLine(col.MaxAvarageRating);
foreach (var item in col.FrequencyGroup(Frequency.Weekly))
{
    Console.WriteLine(item.Value);
}
foreach (var item in col.GroupByFrequency)
{
    Console.WriteLine(item.ToString());
    foreach(var j in item)
    {
        Console.WriteLine(j.Value);
    }
}

TestCollections<Edition, Magazine> testCollections;
int length;

do
{
    Console.WriteLine("Введите число элементов в коллекциях: ");
}while(!int.TryParse(Console.ReadLine(), out length));

testCollections = new TestCollections<Edition, Magazine>(length, GenerateKeyValuePair);

testCollections.MeasureSearchTimes();

static KeyValuePair<Edition, Magazine> GenerateKeyValuePair(int j)
{
    var edition = new Edition(j.ToString(), new DateTime(), j);
    var articles = new List<Article>();
    articles.AddRange(Enumerable.Select(Enumerable.Range(1, j % 10), j => new Article(new Person(j.ToString()), j.ToString(), j)));
    var editors = new List<Person>();
    editors.AddRange(Enumerable.Select(Enumerable.Range(1, j % 10), j => new Person(j.ToString() + j.ToString())));
    var magazine = new Magazine(j.ToString(), (Frequency)(j % 3), new DateTime(), j, articles.ToArray(), editors);
    return new KeyValuePair<Edition, Magazine>(edition, magazine);
}