namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class Transactions
    {
        private static readonly List<string> outputTransactions;
        private static readonly List<string> inputTransactions;

        static Transactions()
        {
            outputTransactions = new List<string>()
            {
                "OP_INT",
                "ARR_LOCN",
                "OP_INTBI",
                "OP_INTIN",
                "INTRA_BIE",
                "INTRA_SER",
                "INTRA_TRI",
                "EXPORT",
                "ISP_TRANS",
                "NOSUJ_CDED",
                "NOSUJ_VEN",
                "NOSUJ_SDED",
                "EXE_SIND",
                "OTR_EXE",
                "CAN_CYM",
                "SUPLIDOS",
                "EXPORT",
                "EXPORTASIM",
                "PEN_CYM",
                "ISP_TRANS",
                "EXE_SIND",
                "EXE_SIND_ART25",
                "EXE_CDED",
                "EXE_CDED_ART47",
                "EXE_CDED_ART13",
                "EXE_CDED_ART23",
                "EXE_CDED_ART24"
            };

            inputTransactions = new List<string>()
            {
                "OP_INT",
                "ARR_LOCN",
                "ARR_ISP_LOCN",
                "OP_INTBI",
                "COMP_AGR",
                "INTRA_BIE",
                "INTRA_SER",
                "INTRA_BI",
                "INV_SUJ_PAS",
                "ISP_BI",
                "IMPORT",
                "IMPORT_BI",
                "IVA_NODED",
                "NOSUJ_SDED",
                "SUPLIDOS",
                "IMPORT_BI25",
                "IGIC_NODED",
                "MINORISTAS",
                "EXENTASPEQ"
            };
        }

        public static bool ItExistForOutput(string code)
        {
            return outputTransactions.Contains(code);
        }

        public static bool ItExistForInput(string code)
        {
            return inputTransactions.Contains(code);
        }
    }
}
