using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Business;
using TourPlanner_Model;

namespace TourPlanner_UnitTests
{
    [TestFixture]
    class UnitTests
    {
        [Test]
        public void getTours()
        {
            Data data = new Data();
            List<Tour> tours = data.getTours();

            Assert.IsNotNull(tours);
        }
    }
}
