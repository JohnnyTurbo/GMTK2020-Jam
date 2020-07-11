using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMG.GMTK2020;

namespace Tests
{
    public class StatTests
    {
        [Test]
        public void Increase_To_Max()
        {
            Stat testStat = new Stat(50, 25);

            testStat.ModifyCurStat(25);

            Assert.AreEqual(50, testStat.cur);
        }

        [Test]
        public void Increase_Above_Max()
        {
            Stat testStat = new Stat(50, 25);

            testStat.ModifyCurStat(50);

            Assert.AreEqual(50, testStat.cur);
        }

        [Test]
        public void Decrease_To_Zero()
        {
            Stat testStat = new Stat(50, 25);

            testStat.ModifyCurStat(-25);

            Assert.AreEqual(0, testStat.cur);
        }

        [Test]
        public void Decrease_Below_Zero()
        {
            Stat testStat = new Stat(50, 25);

            testStat.ModifyCurStat(-50);

            Assert.AreEqual(0, testStat.cur);
        }
    }
}
