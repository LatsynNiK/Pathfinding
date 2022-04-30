using Abstractions.Enums;

namespace Services.Models
{
    public class TypeItemSm : BaseSmModel
    {
        public ItemType Type { get; set; }

        public string Name { get; set; }
    }
}