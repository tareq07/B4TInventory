using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using System.Web;

namespace EMS.Core.Utility
{
    #region Enumeration : Databases
    public enum Database
    {
        Default = 1,// main DB 
        NitgenDatabase = 2
    }
    #endregion

    #region Enummeration : NumericOrder
    public enum EnumNumericOrder
    {
        Start = 0,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Sixth = 6,
        Seventh = 7,
        Eighth = 8,
        Ninth = 9,
        Tenth = 10,
        Eleventh = 11,
        Twelveth = 12,
        Thirteenth,
        Fourteenth,
        Fifteenth,
        Sixteenth,
        Seventeenth,
        Eightennth,
        Nineteenth,
        Twentyth,
        TwentyFirst,
        TwentySecond,
        TwentyThird,
        TwentyFourth,
        TwentyFifth,
        TwentySixth,
        TwentySeventh,
        TwentyEighth,
        TwentyNineth,
        Thirty,
        ThirtyFirst

    }
    #endregion

    #region Enummeration : UserType
    public enum EnumUserType
    {
        None = 0,
        AdminGlobal = 1,
        StaffLocal = 2
    }
    #endregion

    #region Enummeration : Group
    public enum EnumGroup
    {        
        Science = 1,
        Commerce = 2,
        Arts = 3        
    }
    #endregion

    #region Enummeration : Group2
    public enum EnumGroup2
    {
        None = 0,
        Science = 1,
        Commerce = 2,
        Arts = 3
    }
    #endregion

    #region Enummeration : CompareOperator
    public enum EnumCompareOperator
    {
        None = 0,
        EqualTo = 1,
        NotEqualTo = 2,
        GreaterThen = 3,
        SmallerThen = 4,
        Between = 5,
        NotBetween = 6
    }
    #endregion

    #region Enummeration : Gender
    public enum EnumGender
    {
        None = 0,
        Male = 1,
        Female = 2
    }
    #endregion

    #region Enummeration : UserStatus
    public enum EnumUserStatus
    {
        Active = 1,
        Suspend = 0
    }
    #endregion

    #region Enummeration : Religions
    public enum EnumReligions
    {
        None = 0,
        Islam = 1,
        Hinduism = 2,
        Christian = 3,
        Buddhism = 4

    }
    #endregion

    #region Enummeration : Languages
    public enum EnumLanguages
    {
        None = 0,
        Bangla = 1,
        English = 2
    }
    #endregion

    #region Enummeration : Class
    public enum EnumClass
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Eleven=11,
        Twelve=12

    }
    #endregion

    #region Enummeration : Semester
    public enum EnumSemester
    {
        None = 0,
        Spring = 1,
        Summer = 2,
        Fall = 3

    }
    #endregion

    #region Enummeration : BrnchType
    public enum EnumBrnchType
    {
        None = 0,
        Combined = 1,
        Boys = 2,
        Girls = 3

    }
    #endregion

    #region Enummeration : Medium
    public enum EnumMedium
    {
        None = 0,
        Bangla = 1,
        English = 2
    }
    #endregion

    #region Enummeration : Shift
    public enum EnumShift
    {
        None = 0,
        Morning = 1,
        Evening = 2
    }
    #endregion

    #region Enummeration : Cash Tran Type
    public enum EnumCashTrnType
    {
        NormalSale = 0,
        Return = 1,
        Expence = 2,
        Widthdrawal = 3,
        DeleteItem = 4
    }
    #endregion

    #region Enummeration : Installment Terms
    public enum EnumInstTerms
    {
        
        OneMonth = 1,
        TwoMonth = 2,
        ThreeMonth = 3,
        FourMonth = 4,
        FiveMonth = 5,
        SixMonth = 6,
        SevenMonth = 7,
        EightMonth = 8,
        NineMonth = 9,
        TenMonth = 10,
        ElevenMonth = 11,
        TwelveMonth = 12

    }
    #endregion

    #region Enummeration : EnumUnitType
    public enum EnumUnitType
    {
        None = 0,
        Kg = 1,
        Litter = 2,
        Pice = 3
    }
    #endregion
    #region Enummeration : EnumOrderStatus
    public enum EnumOrderStatus
    {
        None = 0,
        Initial = 1,
        AprovedFull = 2,
        AprovedPartial = 3,
        Hold = 4,
        Cancel = 5
        
    }
    #endregion
    #region Enummeration : EnumParty
    public enum EnumParty
    {
        
        Customer = 1,
        Suplier = 2       

    }
    #region Enummeration : AccountType
    public enum EnumAccountType
    {
        None = 0,
        CD = 1,
        SB = 2
    }
    #endregion
    #endregion
}