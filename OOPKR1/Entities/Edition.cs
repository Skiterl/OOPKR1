using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPKR1.Entities
{
    public class Edition
    {
        protected string _title;
        protected DateTime _releaseDate;
        protected int _count;

        public Edition(string title, DateTime releaseDate, int count)
        {
            Title = title;
            ReleaseDate = releaseDate;
            Count = count;
        }

        public Edition()
        {
            Title = "";
            ReleaseDate = DateTime.Now;
            Count = 0;
        }

        public string Title { get => _title; set => _title = value; }
        public DateTime ReleaseDate { get => _releaseDate; set => _releaseDate = value; }
        public int Count
        {
            get => _count;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, "Недопустимое значение");
                //if (value < 0) throw new Exception("Недопустимое значение");
                _count = value;
            }
        }

        public virtual object DeepCopy() => new Edition { Title = Title, ReleaseDate = ReleaseDate, Count = Count };

        public override bool Equals(object? obj)
        {
            if(obj is Edition e) return e.ReleaseDate == ReleaseDate && e.Count == Count && e.Title == Title;
            return false;
        }

        public override int GetHashCode()
        {
            return ReleaseDate.GetHashCode() + Count.GetHashCode() + Title.GetHashCode();
        }

        public override string? ToString()
        {
            return Title + " " + ReleaseDate + " " + Count;
        }

        public static bool operator ==(Edition e1, Edition e2) {  return e1.Equals(e2); }
        public static bool operator !=(Edition e1, Edition e2) { return  !e1.Equals(e2); }
    }
}
