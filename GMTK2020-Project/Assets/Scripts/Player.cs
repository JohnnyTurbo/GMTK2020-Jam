using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020 {
    public class Player : MonoBehaviour
    {
        public int maxHealth;
        public int maxControl;

        public Dictionary<string, Stat> playerStats = new Dictionary<string, Stat>();

		private void Awake()
		{
			playerStats.Add("Health", new Stat() { max = maxHealth, cur = maxHealth });
			playerStats.Add("Control", new Stat() { max = maxControl, cur = maxControl });
		}
	}
}