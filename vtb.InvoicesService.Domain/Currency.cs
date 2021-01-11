using System.ComponentModel.DataAnnotations;

namespace vtb.InvoicesService.Domain
{
    public enum Currency
    {
        Unknown = 0,

        [Display(Name = "United Arab Emirates Dirham")]
        AED = 1,

        [Display(Name = "Afghan Afghani")] AFN = 2,

        [Display(Name = "Albanian Lek")] ALL = 3,

        [Display(Name = "Armenian Dram")] AMD = 4,

        [Display(Name = "Netherlands Antillean Guilder")]
        ANG = 5,

        [Display(Name = "Angolan Kwanza")] AOA = 6,

        [Display(Name = "Argentine Peso")] ARS = 7,

        [Display(Name = "Australian Dollar")] AUD = 8,

        [Display(Name = "Aruban Florin")] AWG = 9,

        [Display(Name = "Azerbaijani Manat")] AZN = 10,

        [Display(Name = "Bosnia-Herzegovina Convertible Mark")]
        BAM = 11,

        [Display(Name = "Barbadian Dollar")] BBD = 12,

        [Display(Name = "Bangladeshi Taka")] BDT = 13,

        [Display(Name = "Bulgarian Lev")] BGN = 14,

        [Display(Name = "Bahraini Dinar")] BHD = 15,

        [Display(Name = "Burundian Franc")] BIF = 16,

        [Display(Name = "Bermudan Dollar")] BMD = 17,

        [Display(Name = "Brunei Dollar")] BND = 18,

        [Display(Name = "Bolivian Boliviano")] BOB = 19,

        [Display(Name = "Brazilian Real")] BRL = 20,

        [Display(Name = "Bahamian Dollar")] BSD = 21,

        [Display(Name = "Bitcoin")] BTC = 22,

        [Display(Name = "Bhutanese Ngultrum")] BTN = 23,

        [Display(Name = "Botswanan Pula")] BWP = 24,

        [Display(Name = "Belarusian Ruble")] BYN = 25,

        [Display(Name = "Belize Dollar")] BZD = 26,

        [Display(Name = "Canadian Dollar")] CAD = 27,

        [Display(Name = "Congolese Franc")] CDF = 28,

        [Display(Name = "Swiss Franc")] CHF = 29,

        [Display(Name = "Chilean Unit of Account (UF)")]
        CLF = 30,

        [Display(Name = "Chilean Peso")] CLP = 31,

        [Display(Name = "Chinese Yuan (Offshore)")]
        CNH = 32,

        [Display(Name = "Chinese Yuan")] CNY = 33,

        [Display(Name = "Colombian Peso")] COP = 34,

        [Display(Name = "Costa Rican Colón")] CRC = 35,

        [Display(Name = "Cuban Convertible Peso")]
        CUC = 36,

        [Display(Name = "Cuban Peso")] CUP = 37,

        [Display(Name = "Cape Verdean Escudo")]
        CVE = 38,

        [Display(Name = "Czech Republic Koruna")]
        CZK = 39,

        [Display(Name = "Djiboutian Franc")] DJF = 40,

        [Display(Name = "Danish Krone")] DKK = 41,

        [Display(Name = "Dominican Peso")] DOP = 42,

        [Display(Name = "Algerian Dinar")] DZD = 43,

        [Display(Name = "Egyptian Pound")] EGP = 44,

        [Display(Name = "Eritrean Nakfa")] ERN = 45,

        [Display(Name = "Ethiopian Birr")] ETB = 46,

        [Display(Name = "Euro")] EUR = 47,

        [Display(Name = "Fijian Dollar")] FJD = 48,

        [Display(Name = "Falkland Islands Pound")]
        FKP = 49,

        [Display(Name = "British Pound Sterling")]
        GBP = 50,

        [Display(Name = "Georgian Lari")] GEL = 51,

        [Display(Name = "Guernsey Pound")] GGP = 52,

        [Display(Name = "Ghanaian Cedi")] GHS = 53,

        [Display(Name = "Gibraltar Pound")] GIP = 54,

        [Display(Name = "Gambian Dalasi")] GMD = 55,

        [Display(Name = "Guinean Franc")] GNF = 56,

        [Display(Name = "Guatemalan Quetzal")] GTQ = 57,

        [Display(Name = "Guyanaese Dollar")] GYD = 58,

        [Display(Name = "Hong Kong Dollar")] HKD = 59,

        [Display(Name = "Honduran Lempira")] HNL = 60,

        [Display(Name = "Croatian Kuna")] HRK = 61,

        [Display(Name = "Haitian Gourde")] HTG = 62,

        [Display(Name = "Hungarian Forint")] HUF = 63,

        [Display(Name = "Indonesian Rupiah")] IDR = 64,

        [Display(Name = "Israeli New Sheqel")] ILS = 65,

        [Display(Name = "Manx pound")] IMP = 66,

        [Display(Name = "Indian Rupee")] INR = 67,

        [Display(Name = "Iraqi Dinar")] IQD = 68,

        [Display(Name = "Iranian Rial")] IRR = 69,

        [Display(Name = "Icelandic Króna")] ISK = 70,

        [Display(Name = "Jersey Pound")] JEP = 71,

        [Display(Name = "Jamaican Dollar")] JMD = 72,

        [Display(Name = "Jordanian Dinar")] JOD = 73,

        [Display(Name = "Japanese Yen")] JPY = 74,

        [Display(Name = "Kenyan Shilling")] KES = 75,

        [Display(Name = "Kyrgystani Som")] KGS = 76,

        [Display(Name = "Cambodian Riel")] KHR = 77,

        [Display(Name = "Comorian Franc")] KMF = 78,

        [Display(Name = "North Korean Won")] KPW = 79,

        [Display(Name = "South Korean Won")] KRW = 80,

        [Display(Name = "Kuwaiti Dinar")] KWD = 81,

        [Display(Name = "Cayman Islands Dollar")]
        KYD = 82,

        [Display(Name = "Kazakhstani Tenge")] KZT = 83,

        [Display(Name = "Laotian Kip")] LAK = 84,

        [Display(Name = "Lebanese Pound")] LBP = 85,

        [Display(Name = "Sri Lankan Rupee")] LKR = 86,

        [Display(Name = "Liberian Dollar")] LRD = 87,

        [Display(Name = "Lesotho Loti")] LSL = 88,

        [Display(Name = "Libyan Dinar")] LYD = 89,

        [Display(Name = "Moroccan Dirham")] MAD = 90,

        [Display(Name = "Moldovan Leu")] MDL = 91,

        [Display(Name = "Malagasy Ariary")] MGA = 92,

        [Display(Name = "Macedonian Denar")] MKD = 93,

        [Display(Name = "Myanma Kyat")] MMK = 94,

        [Display(Name = "Mongolian Tugrik")] MNT = 95,

        [Display(Name = "Macanese Pataca")] MOP = 96,

        [Display(Name = "Mauritanian Ouguiya (pre-2018)")]
        MRO = 97,

        [Display(Name = "Mauritanian Ouguiya")]
        MRU = 98,

        [Display(Name = "Mauritian Rupee")] MUR = 99,

        [Display(Name = "Maldivian Rufiyaa")] MVR = 100,

        [Display(Name = "Malawian Kwacha")] MWK = 101,

        [Display(Name = "Mexican Peso")] MXN = 102,

        [Display(Name = "Malaysian Ringgit")] MYR = 103,

        [Display(Name = "Mozambican Metical")] MZN = 104,

        [Display(Name = "Namibian Dollar")] NAD = 105,

        [Display(Name = "Nigerian Naira")] NGN = 106,

        [Display(Name = "Nicaraguan Córdoba")] NIO = 107,

        [Display(Name = "Norwegian Krone")] NOK = 108,

        [Display(Name = "Nepalese Rupee")] NPR = 109,

        [Display(Name = "New Zealand Dollar")] NZD = 110,

        [Display(Name = "Omani Rial")] OMR = 111,

        [Display(Name = "Panamanian Balboa")] PAB = 112,

        [Display(Name = "Peruvian Nuevo Sol")] PEN = 113,

        [Display(Name = "Papua New Guinean Kina")]
        PGK = 114,

        [Display(Name = "Philippine Peso")] PHP = 115,

        [Display(Name = "Pakistani Rupee")] PKR = 116,

        [Display(Name = "Polish Zloty")] PLN = 117,

        [Display(Name = "Paraguayan Guarani")] PYG = 118,

        [Display(Name = "Qatari Rial")] QAR = 119,

        [Display(Name = "Romanian Leu")] RON = 120,

        [Display(Name = "Serbian Dinar")] RSD = 121,

        [Display(Name = "Russian Ruble")] RUB = 122,

        [Display(Name = "Rwandan Franc")] RWF = 123,

        [Display(Name = "Saudi Riyal")] SAR = 124,

        [Display(Name = "Solomon Islands Dollar")]
        SBD = 125,

        [Display(Name = "Seychellois Rupee")] SCR = 126,

        [Display(Name = "Sudanese Pound")] SDG = 127,

        [Display(Name = "Swedish Krona")] SEK = 128,

        [Display(Name = "Singapore Dollar")] SGD = 129,

        [Display(Name = "Saint Helena Pound")] SHP = 130,

        [Display(Name = "Sierra Leonean Leone")]
        SLL = 131,

        [Display(Name = "Somali Shilling")] SOS = 132,

        [Display(Name = "Surinamese Dollar")] SRD = 133,

        [Display(Name = "South Sudanese Pound")]
        SSP = 134,

        [Display(Name = "São Tomé and Príncipe Dobra (pre-2018)")]
        STD = 135,

        [Display(Name = "São Tomé and Príncipe Dobra")]
        STN = 136,

        [Display(Name = "Salvadoran Colón")] SVC = 137,

        [Display(Name = "Syrian Pound")] SYP = 138,

        [Display(Name = "Swazi Lilangeni")] SZL = 139,

        [Display(Name = "Thai Baht")] THB = 140,

        [Display(Name = "Tajikistani Somoni")] TJS = 141,

        [Display(Name = "Turkmenistani Manat")]
        TMT = 142,

        [Display(Name = "Tunisian Dinar")] TND = 143,

        [Display(Name = "Tongan Pa'anga")] TOP = 144,

        [Display(Name = "Turkish Lira")] TRY = 145,

        [Display(Name = "Trinidad and Tobago Dollar")]
        TTD = 146,

        [Display(Name = "New Taiwan Dollar")] TWD = 147,

        [Display(Name = "Tanzanian Shilling")] TZS = 148,

        [Display(Name = "Ukrainian Hryvnia")] UAH = 149,

        [Display(Name = "Ugandan Shilling")] UGX = 150,

        [Display(Name = "United States Dollar")]
        USD = 151,

        [Display(Name = "Uruguayan Peso")] UYU = 152,

        [Display(Name = "Uzbekistan Som")] UZS = 153,

        [Display(Name = "Venezuelan Bolívar Fuerte")]
        VEF = 154,

        [Display(Name = "Vietnamese Dong")] VND = 155,

        [Display(Name = "Vanuatu Vatu")] VUV = 156,

        [Display(Name = "Samoan Tala")] WST = 157,

        [Display(Name = "CFA Franc BEAC")] XAF = 158,

        [Display(Name = "Silver Ounce")] XAG = 159,

        [Display(Name = "Gold Ounce")] XAU = 160,

        [Display(Name = "East Caribbean Dollar")]
        XCD = 161,

        [Display(Name = "Special Drawing Rights")]
        XDR = 162,

        [Display(Name = "CFA Franc BCEAO")] XOF = 163,

        [Display(Name = "Palladium Ounce")] XPD = 164,

        [Display(Name = "CFP Franc")] XPF = 165,

        [Display(Name = "Platinum Ounce")] XPT = 166,

        [Display(Name = "Yemeni Rial")] YER = 167,

        [Display(Name = "South African Rand")] ZAR = 168,

        [Display(Name = "Zambian Kwacha")] ZMW = 169,

        [Display(Name = "Zimbabwean Dollar")] ZWL = 170
    }


}
