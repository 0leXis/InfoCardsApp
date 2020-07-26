using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoCardsAppServer;
using System.Linq;
using System.IO;

namespace ServerUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void JSONSerializationTest()
        {
            foreach (var file in Directory.GetFiles("test/"))
                File.Delete(file);

            var card = new InfoCard(-1, "Холодильник", new byte[] { 1, 2, 3, 4 });

            card.SaveToFile("test/", new JSONSaveLoader(), true);
            var loadedCard = InfoCard.LoadFromFile("test/", "0", new JSONSaveLoader());

            Assert.AreEqual(card.CardId, loadedCard.CardId);
            Assert.AreEqual(card.CardName, loadedCard.CardName);
            Assert.IsTrue(card.Image.SequenceEqual(loadedCard.Image));
        }

        [TestMethod]
        public void XMLSerializationTest()
        {
            foreach (var file in Directory.GetFiles("test/"))
                File.Delete(file);

            var card = new InfoCard(-1, "Холодильник", new byte[] { 1, 2, 3, 4 });

            card.SaveToFile("test/", new XMLSaveLoader(), true);
            var loadedCard = InfoCard.LoadFromFile("test/", "0", new XMLSaveLoader());

            Assert.AreEqual(card.CardId, loadedCard.CardId);
            Assert.AreEqual(card.CardName, loadedCard.CardName);
            Assert.IsTrue(card.Image.SequenceEqual(loadedCard.Image));
        }
    }
}
