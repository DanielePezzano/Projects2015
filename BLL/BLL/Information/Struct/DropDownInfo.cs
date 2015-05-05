namespace BLL.Information.Struct
{
    public struct DropDownInfo
    {
        public int ItemId;
        public string ItemName;

        public DropDownInfo(int id, string name)
        {
            ItemName = name;
            ItemId = id;
        }
    }
}