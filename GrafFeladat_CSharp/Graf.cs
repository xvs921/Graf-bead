using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        public void szelessegiBejaras(int kezdop)
        {
            List<int> bejartLista = new List<int>();
            List<int> kovetkezokLista= new List<int>();

            kovetkezokLista.Add(kezdop);
            bejartLista.Add(kezdop);

            while (kovetkezokLista.Count!=0)
            {
                int i = kovetkezokLista[0];
                kovetkezokLista.RemoveAt(0);
                Console.Write(this.csucsok[i] +";");
                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == i && !bejartLista.Contains(el.Csucs2))
                    {
                        kovetkezokLista.Add(el.Csucs2);
                        bejartLista.Add(el.Csucs2);
                    }
                }
            }
        }

        public void melysegiBejaras(int kezdop)
        {
            List<int> bejartLista = new List<int>();
            List<int> kovetkezokLista = new List<int>();

            kovetkezokLista.Add(kezdop);
            bejartLista.Add(kezdop);

            while (kovetkezokLista.Count!=0)
            {
                int i = kovetkezokLista[0];
                kovetkezokLista.RemoveAt(0);
                Console.Write(this.csucsok[i]+ ";");
                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == i && !bejartLista.Contains(el.Csucs2))
                    {
                        kovetkezokLista.Add(el.Csucs2);
                        bejartLista.Add(el.Csucs2);
                    }
                }
            }
        }

        public bool osszefuggo()
        {
            bool osszefuggoE = false;

            List<int> bejartLista = new List<int>();
            List<int> kovetkezokLista = new List<int>();

            kovetkezokLista.Add(0);
            bejartLista.Add(0);

            while (kovetkezokLista.Count!=0)
            {
                int k = kovetkezokLista[0];
                kovetkezokLista.RemoveAt(0);
                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == k && !bejartLista.Contains(el.Csucs2))
                    {
                        kovetkezokLista.Add(el.Csucs2);
                        bejartLista.Add(el.Csucs2);
                    }
                }
            }
            if (bejartLista.Count == this.csucsokSzama)
            {
                osszefuggoE=true;
            }
            return osszefuggoE;
        }

        public Graf feszitofa()
        {
            Graf ffa = new Graf(this.csucsokSzama);

            List<int> bejartLista = new List<int>();
            List<int> kovetkezokLista = new List<int>();

            kovetkezokLista.Add(0);
            bejartLista.Add(0);

            while (kovetkezokLista.Count!= 0)
            {
                int i = kovetkezokLista[0];
                kovetkezokLista.RemoveAt(0);
                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == i)
                    {
                        if (!(bejartLista.Contains(el.Csucs2)))
                        {
                            bejartLista.Add(el.Csucs2);
                            kovetkezokLista.Add(el.Csucs1);
                            ffa.Hozzaad(el.Csucs1, el.Csucs2);
                        }
                    }
                }
            }
            return ffa;
        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}