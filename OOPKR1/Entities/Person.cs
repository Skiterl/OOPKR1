namespace OOPKR1.Entities
{
    public class Person
    {
        public string Surname { get; set; }

        public Person(string surname) => Surname = surname;

        public override string ToString() => Surname;
    }
}
