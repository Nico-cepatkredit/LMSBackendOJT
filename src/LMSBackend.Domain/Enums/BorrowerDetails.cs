using System.ComponentModel.DataAnnotations;

namespace LMSBackend.Domain.Enums
{
    public enum BorrowersType
    {
        [Display(Name = "OFW")]
        OFW = 1,

        [Display(Name = "BENE")]
        BENE = 2,

        [Display(Name = "ACB")]
        ACB = 3
    }

    public enum Suffix
    {
        [Display(Name = "N/A")]
        NotApplicable = 1,

        [Display(Name = "Jr.")]
        Jr = 2,

        [Display(Name = "Sr.")]
        Sr = 3,

        [Display(Name = "I")]
        I = 4,

        [Display(Name = "II")]
        II = 5,

        [Display(Name = "III")]
        III = 6,

        [Display(Name = "IV")]
        IV = 7,

        [Display(Name = "V")]
        V = 8
    }

    public enum Gender
    {
        [Display(Name = "MALE")]
        Male = 1,

        [Display(Name = "FEMALE")]
        Female = 2
    }

    public enum MaritalStatus
    {
        [Display(Name = "SINGLE")]
        Single = 1,

        [Display(Name = "MARRIED")]
        Married = 2,

        [Display(Name = "WIDOWED")]
        Widowed = 3,

        [Display(Name = "SEPARATED")]
        Separated = 4,

        [Display(Name = "COMMON LAW")]
        CommonLaw = 5,

        [Display(Name = "LIVE IN PARTNER")]
        LiveInPartner = 6
    }

    public enum TypeOfResidence
    {
        [Display(Name = "OWNED HOUSE")]
        OwnedHouse = 1,
        [Display(Name = "MORTGAGED")]
        Mortgaged = 2,
        [Display(Name = "RENTING")]
        Renting = 3,
        [Display(Name = "USED FREE")]
        UsedFree = 4
    }

    public enum Religion
    {
        [Display(Name = "CATHOLIC")]
        Catholic = 1,

        [Display(Name = "IGLESIA NI CRISTO")]
        IglesiaNiCristo = 2,

        [Display(Name = "MUSLIM")]
        Muslim = 3,

        [Display(Name = "CHRISTIAN / BORN-AGAIN CHRISTIAN")]
        BornAgainChristian = 4,

        [Display(Name = "EL SHADDAI")]
        ElShaddai = 5,

        [Display(Name = "JEHOVAH’S WITNESSES")]
        JehovahsWitnesses = 6,

        [Display(Name = "CHURCH OF LATTER DAY SAINTS / MORMON")]
        Mormon = 7
    }

    public enum EducationStatus
    {
        [Display(Name = "N/A")]
        NotApplicable = 1,

        [Display(Name = "ELEMENTARY SCHOOL")]
        Elementary = 2,

        [Display(Name = "HIGH SCHOOL")]
        HighSchool = 3,

        [Display(Name = "COLLEGE")]
        College = 4,

        [Display(Name = "ASSOCIATE / VOCATIONAL DEGREE")]
        AssociateVocational = 5,

        [Display(Name = "BACHELOR / MASTER / DOCTORATE DEGREE")]
        BachelorMasterDoctorate = 6,

        [Display(Name = "TECHNICAL OR TRADE SCHOOL CERTIFICATE")]
        TechnicalTrade = 7,

        [Display(Name = "POSTGRADUATE STUDIES")]
        PostGraduate = 8,

        [Display(Name = "PROFESSIONAL CERTIFICATION")]
        ProfessionalCertification = 9
    }

    public enum Relationships
    {
        [Display(Name = "AUNT / UNCLE")]
        AuntUncle = 1,

        [Display(Name = "BROTHER-IN-LAW / SISTER-IN-LAW")]
        BrotherSisterInLaw = 2,

        [Display(Name = "BUSINESS PARTNER")]
        BusinessPartner = 3,

        [Display(Name = "CHILDREN")]
        Children = 4,

        [Display(Name = "COLLEAGUE / OFFICEMATE")]
        ColleagueOfficemate = 5,

        [Display(Name = "COMMON-LAW SPOUSE")]
        CommonLawSpouse = 6,

        [Display(Name = "COUSIN")]
        Cousin = 7,

        [Display(Name = "FATHER-IN-LAW / MOTHER-IN-LAW")]
        FatherMotherInLaw = 8,

        [Display(Name = "FIANCÉ / LIVE-IN PARTNER")]
        FianceLiveInPartner = 9,

        [Display(Name = "FRIEND")]
        Friend = 10,

        [Display(Name = "GODPARENT")]
        Godparent = 11,

        [Display(Name = "GRANDPARENT")]
        Grandparent = 12,

        [Display(Name = "NEIGHBOR")]
        Neighbor = 13,

        [Display(Name = "NIECE / NEPHEW")]
        NieceNephew = 14,

        [Display(Name = "PARENT")]
        Parent = 15,

        [Display(Name = "SIBLING")]
        Sibling = 16,

        [Display(Name = "SON-IN-LAW / DAUGHTER-IN-LAW")]
        SonDaughterInLaw = 17,

        [Display(Name = "SPOUSE")]
        Spouse = 18,

        [Display(Name = "OTHERS")]
        Others = 19
    }

    public enum SpouseSourceOfIncome
    {
        [Display(Name = "EMPLOYED")]
        AuntUncle = 1,

        [Display(Name = "UNEMPLOYED")]
        BrotherSisterInLaw = 2,

        [Display(Name = "BUSINESS")]
        BusinessPartner = 3,
    }

    public enum AddressType
    {
        [Display(Name = "PRESENT")]
        AuntUncle = 1,

        [Display(Name = "PERMANENT")]
        BrotherSisterInLaw = 2,

        [Display(Name = "PROVINCE")]
        BusinessPartner = 3,
    }
}
