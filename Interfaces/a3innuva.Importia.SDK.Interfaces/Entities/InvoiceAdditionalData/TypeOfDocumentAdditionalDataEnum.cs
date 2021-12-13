namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public enum TypeOfDocumentAdditionalDataEnum
    {
        None = 0,

        IssuedNotCorrectiveInvoice = 10,
        IssuedNotCorrectiveSimplifiedInvoice = 11,
        IssuedNotCorrectiveInvoiceWithoutObligation = 12,
        IssuedCorrectiveErrorFundado = 20,
        IssuedCorrectiveConcurso = 21,
        IssuedCorrectiveDeudaIncobrable = 22,
        IssuedCorrectiveResto = 23,
        IssuedCorrectiveSimplificadas = 24,



        InputNotCorrectiveInvoice = 30,
        InputNotCorrectiveSimplifiedInvoice = 0x1F,
        InputNotCorrectiveJustificantesContables = 0x20,
        InputCorrectiveErrorFundado = 40,
        InputCorrectiveConcurso = 41,
        InputCorrectiveDeudaIncobrable = 42,
        InputCorrectiveResto = 43,
        InputCorrectiveSimplificadas = 44
    }
}
