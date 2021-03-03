namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    /// <summary>
    /// Available sources, select extern for 3rd software
    /// </summary>
    public enum MigrationOrigin
    {
        Excel = 0,
        Eco = 10,
        Con = 11,
        Cwn = 20,
        Fgo = 30,
        A3ASESORnom = 40,
        A3InnuvaNomina = 50,
        A3InnuvaFactura = 60,
        Extern = 90,
        None = 99
    }
}
