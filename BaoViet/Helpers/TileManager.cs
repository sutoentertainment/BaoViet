using BaoVietCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace BaoViet.Helpers
{
    public class TileManager
    {
        public void SetBadgeNumber(int number)
        {
            XmlDocument badgeData = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            XmlNodeList badgeXML = badgeData.GetElementsByTagName("badge");


            ((XmlElement)badgeXML[0]).SetAttribute("value", (number).ToString());

            BadgeNotification badge = new BadgeNotification(badgeData);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
        }

        public async Task CreateSecondaryTileAsync(string id, string imageUri, string title, string args, string wideLogo = null)
        {
            SecondaryTile tile = new  SecondaryTile(id, title, args, new Uri(imageUri), TileSize.Square150x150);
            tile.VisualElements.BackgroundColor = Colors.Transparent;
            if (wideLogo != null)
                tile.VisualElements.Wide310x150Logo = new Uri(wideLogo);

            tile.RoamingEnabled = true;
            await tile.RequestCreateAsync();

        }

        public void UpdateTile(bool Transparent)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Image);

            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            //tileTextAttributes[0].InnerText = "Hello World! My very own tile notification";

            XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", (Transparent ? "ms-appx:///Assets/Wide310x150LogoTrans.png" : "ms-appx:///Assets/Wide310x150Logo.png"));
            ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "");

            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
            XmlNodeList tileImageAttributesSquare = squareTileXml.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributesSquare[0]).SetAttribute("src", (Transparent ? "ms-appx:///Assets/Square150x150LogoTrans.png" : "ms-appx:///Assets/Square150x150Logo.png"));

            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            //squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode("Hello World! My very own tile notification"));


            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);

        }

        internal async Task DeleteTileAsync(string id)
        {
            var tiles = await SecondaryTile.FindAllAsync();
            var tile = tiles.Where(x => x.TileId == id).FirstOrDefault();
            if(tile != null)
            {
                await tile.RequestDeleteAsync();
            }
        }
    }
}
