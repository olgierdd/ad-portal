using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdPortal.AdManagers;

namespace AdPortal.Tests
{
    [TestClass]
    public class Repository
    {
        [TestMethod]
        public void LoadAds()
        {
            var mgr = new AdWCFManager();
            mgr.LoadAds("01/01/2011", "04/01/2011");
            using (StreamWriter sw = new StreamWriter("AllAds.csv"))
            {
                foreach (var ad in mgr.Ads)
                {
                    sw.WriteLine($"{ad.AdId};{ad.BrandId};{ad.BrandName};{ad.NumPages};{ad.Position}");
                }
                sw.Close();
            }

        }
    }
}
