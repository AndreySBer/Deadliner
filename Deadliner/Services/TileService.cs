using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Deadliner.Models;
using System.Xml.Linq;
namespace Deadliner.Services
{
    public class TileService
    {
        static public void SetBadgeCountOnTile(int count)
        {
            // Update the badge on the real tile
            XmlDocument badgeXml =
           BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            XmlElement badgeElement =
           (XmlElement)badgeXml.SelectSingleNode("/badge");
            badgeElement.SetAttribute("value", count.ToString());
            BadgeNotification badge = new BadgeNotification(badgeXml);

            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
        }
        private static Windows.Data.Xml.Dom.XmlDocument CreateTiles(PrimaryTile primaryTile)
        {
            XDocument xDoc = new XDocument(new XElement("tile", new XAttribute("version", 3),
                    new XElement("visual",
                        // Wide Tile
                        new XElement("binding", /*new XAttribute("branding", primaryTile.branding),*/ /*new XAttribute("displayName", primaryTile.appName),*/ new XAttribute("template", "TileWide"),
                            new XElement("group",
                                new XElement("subgroup",
                                    new XElement("text", primaryTile.time, new XAttribute("hint-style", "caption")),
                                    new XElement("text", primaryTile.message, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 3))
                                    
                                    ),
                                new XElement("subgroup", new XAttribute("hint-weight", 15),
                                    new XElement("image", new XAttribute("placement", "inline"), new XAttribute("src", "Assets/StoreLogo.png"))
                                )
                            )
                        ))));
            Windows.Data.Xml.Dom.XmlDocument xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            return xmlDoc;
        }
        public static void UpdatePrimaryTile(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var xmlDoc = TileService.CreateTiles(new PrimaryTile());
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            TileNotification notification = new TileNotification(xmlDoc);
            updater.Update(notification);
        }
    }
}
