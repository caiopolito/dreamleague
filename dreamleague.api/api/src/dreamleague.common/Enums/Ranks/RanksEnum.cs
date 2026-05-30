using System.Runtime.Serialization;

namespace dreamleague.common.Enums.Ranks
{
    public enum RanksEnum
    {
        [EnumMember(Value = "Unranked")]
        Unranked = 0,
        [EnumMember(Value = "Silver 1")]
        SilverOne = 1,
        [EnumMember(Value = "Silver 2")]
        SilverTwo = 2,
        [EnumMember(Value = "Silver 3")]
        SilverThree = 3,
        [EnumMember(Value = "Silver 4")]
        SilverFour = 4,
        [EnumMember(Value = "Gold 1")]
        GoldOne = 5,
        [EnumMember(Value = "Gold 2")]
        GoldTwo = 6,
        [EnumMember(Value = "Gold 3")]
        GoldThree = 7,
        [EnumMember(Value = "Gold 4")]
        GoldFour = 8,
        [EnumMember(Value = "Diamond 1")]
        DiamondOne = 9,
        [EnumMember(Value = "Diamond 2")]
        DiamondTwo = 10,
        [EnumMember(Value = "Diamond 3")]
        DiamondThree = 11,
        [EnumMember(Value = "Diamond 4")]
        DiamondFour = 12
    }
}
