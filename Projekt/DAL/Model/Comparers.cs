using DAL.Model.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public static class CustomComparer
    {
        public static Comparer<T> Create<T>(Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException("comparison");
            return new ComparisonComparer<T>(comparison);
        }
        private sealed class ComparisonComparer<T> : Comparer<T>
        {
            private readonly Comparison<T> comparison;
            public ComparisonComparer(Comparison<T> comparison)
            {
                this.comparison = comparison;
            }
            public override int Compare(T x, T y)
            {
                return comparison(x, y);
            }
        }


        //player comparers
        public static Comparer<Player> ComparePlayerByShirtNumberAsc = CustomComparer.Create<Player>((a, b) => a.ShirtNumber.CompareTo(b.ShirtNumber));

        public static Comparer<Player> ComparePlayerByShirtNumberDesc = CustomComparer.Create<Player>((a, b) => -a.ShirtNumber.CompareTo(b.ShirtNumber));

        public static Comparer<Player> ComparePlayerByNameAsc = CustomComparer.Create<Player>((a, b) => a.Name.CompareTo(b.Name));

        public static Comparer<Player> ComparePlayerByNameDesc = CustomComparer.Create<Player>((a, b) => -a.Name.CompareTo(b.Name));

        public static Comparer<Player> ComparePlayerByGoalsAsc = CustomComparer.Create<Player>((a, b) => a.Goals.CompareTo(b.Goals));

        public static Comparer<Player> ComparePlayerByGoalsDesc = CustomComparer.Create<Player>((a, b) => -a.Goals.CompareTo(b.Goals));

        public static Comparer<Player> ComparePlayerByYellowCardsAsc = CustomComparer.Create<Player>((a, b) => a.Goals.CompareTo(b.Goals));

        public static Comparer<Player> ComparePlayerByYellowCardsDesc = CustomComparer.Create<Player>((a, b) => -a.YellowCards.CompareTo(b.YellowCards));

        //match comparers
        public static Comparer<Match> CompareMatchByAttendanceAsc = CustomComparer.Create<Match>((a, b) => a.Attendance.CompareTo(b.Attendance));

        public static Comparer<Match> CompareMatchByAttendanceDesc = CustomComparer.Create<Match>((a, b) => -a.Attendance.CompareTo(b.Attendance));

    }
}
