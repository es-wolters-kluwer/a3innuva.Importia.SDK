namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public class Transactions
    {
        private readonly List<string> outputTransactions;
        private readonly List<string> inputTransactions;

        public Transactions()
        {
            this.outputTransactions = new List<string>()
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
                "NOSUJ_SDED",
                "EXE_SIND",
                "OTR_EXE",
                "CAN_CYM",
                "SUPLIDOS",
                "EXPORTASIM",
                "PEN_CYM",
                "NOSUJ_CDED",
                "NOSUJ_SDED",
                "EXE_SIND25",
            };

            this.inputTransactions = new List<string>()
            {
                "OP_INT",
                "ARR_LOCN",
                "ARR_LOCN_ISP",
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
                "ARR_ISP_LOCN",
                "IMPORT_BI25",
                "IGIC_NODED",
                "MINORISTAS",
                "EXENTASPEQ",
            };
        }

        public bool ItExistForOutput(string code)
        {
            return this.outputTransactions.Contains(code);
        }

        public bool ItExistForInput(string code)
        {
            return this.inputTransactions.Contains(code);
        }
    }
}
