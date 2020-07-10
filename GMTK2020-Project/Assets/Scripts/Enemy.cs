using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMG.GMTK2020
{
	public class Enemy : MonoBehaviour
	{
		public int maxHealth;
		public int maxControl;

		public Dictionary<string, Stat> enemyStats = new Dictionary<string, Stat>();

		private void Awake()
		{
			enemyStats.Add("Health", new Stat() { max = maxHealth, cur = maxHealth });
			enemyStats.Add("Control", new Stat() { max = maxControl, cur = maxControl });
		}
	}
}