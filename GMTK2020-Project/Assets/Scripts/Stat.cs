using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
    public class Stat
    {
        public int max;
        public int cur;
        
        public Stat() { }

        public Stat(int _max)
		{
            max = _max;
            cur = _max;
		}

        public Stat(int _max, int _cur)
		{
            max = _max;
            cur = _cur;
		}

        public void ModifyCurStat(int amount)
	    {
            cur = Math.Min(max, cur + amount);
            cur = Math.Max(cur, 0);
	    }
    }

}