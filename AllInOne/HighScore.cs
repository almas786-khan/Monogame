using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne
{
    public class HighScore : IComparable<HighScore>
    {
     
            private int points;
            public int Points
            {
                get { return points; }
            }
            private string name;
            public string Name
            {
                get { return name; }
            }

            public HighScore(int points, string name)
            {
                this.points = points;
                this.name = name;
            }


        public int CompareTo(HighScore other)
        {
            return (other.points - this.points);
        }
    }
    }


