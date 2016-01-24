namespace BaoVietCore.Models
{
    public class MenuItem
    {
        public string MenuTitle { get; set; }
        public string Glyph { get; set; }
        public MenuItemType Type { get; set; }
    }

    public enum MenuItemType
    {
        Home,
        Saved,
        Setting
    }
}